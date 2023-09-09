using Core.Entities;
using Core.Enums;
using Core.Repositories;
using Core.UnitofWork;
using Data.Repositories;
using MediatR;
using Service.Common;
using Shared.ResponseObjects;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Service.CQRS.Commands.RegisterUserResolved
{
    public class RegisterUserResolvedCommandHandler : IRequestHandler<RegisterUserResolvedCommand, Response<Unit>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUnitofWork _unitofWork;

        public RegisterUserResolvedCommandHandler(IUserRepository userRepository, IUnitofWork unitofWork, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _unitofWork = unitofWork;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<Response<Unit>> Handle(RegisterUserResolvedCommand command, CancellationToken cancellationToken)
        {
            User user = new();

            var userNameResult = UserNameProcess(user, command);
            if (userNameResult.HttpStatusCode is not (int)HttpStatusCode.OK)
                return userNameResult;

            var userSurnameResult = UserSurnameProcess(user, command);
            if(userSurnameResult.HttpStatusCode is not (int)HttpStatusCode.OK)
                return userSurnameResult;

            var userEmailResult = UserEmailProcess(user, command);
            if(userEmailResult.HttpStatusCode is not (int)HttpStatusCode.OK)
                return userEmailResult;

            var userAccountTypeResult = UserAccountTypeProcess(user, command);
            if(userAccountTypeResult.HttpStatusCode is not (int)HttpStatusCode.OK)
                return userAccountTypeResult;

            var userPasswordResult = UserPasswordProcess(user, command);
            if (userPasswordResult.HttpStatusCode is not (int)HttpStatusCode.OK)
                return userPasswordResult;

            await UserRolesProcessAsync(user, command);

            await _userRepository.AddUserAsync(user);
            await _unitofWork.SaveChangesAsync();

            return Response<Unit>.Success(Unit.Value, (int)HttpStatusCode.OK);
        }

        private Response<Unit> UserNameProcess(User user, RegisterUserResolvedCommand command)
        {
            if(string.IsNullOrWhiteSpace(command.Name) || command.Name.Length > 20
                || command.Name.Length < 2)
            {
                return Response<Unit>.Error("Name is not valid!", (int)HttpStatusCode.BadRequest);
            }

            user.Name = command.Name;
            return Response<Unit>.Success((int)HttpStatusCode.OK);
        }

        private Response<Unit> UserSurnameProcess(User user, RegisterUserResolvedCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Surname) || command.Surname.Length > 20
                || command.Surname.Length < 2)
            {
                return Response<Unit>.Error("Surname is not valid!", (int)HttpStatusCode.BadRequest);
            }

            user.Surname = command.Surname;
            return Response<Unit>.Success((int)HttpStatusCode.OK);
        }

        private Response<Unit> UserEmailProcess(User user, RegisterUserResolvedCommand command)
        {
            if(string.IsNullOrWhiteSpace(command.Email))
                return Response<Unit>.Error("Email is cannot be empty!", (int)HttpStatusCode.BadRequest);

            var emailService = new EmailAddressAttribute();
            var isValidEmail = emailService.IsValid(command.Email);

            if(!isValidEmail)
                return Response<Unit>.Error("Email is not valid!", (int)HttpStatusCode.BadRequest);

            user.Email = command.Email;
            return Response<Unit>.Success((int)HttpStatusCode.OK);
        }

        private Response<Unit> UserAccountTypeProcess(User user, RegisterUserResolvedCommand command)
        {
            if(!Enum.IsDefined(typeof(AccountType), command.AccountType))
                return Response<Unit>.Error("AccountType is not valid!", (int)HttpStatusCode.BadRequest);

            user.AccountType = command.AccountType;
            return Response<Unit>.Success((int)HttpStatusCode.OK);
        }

        private Response<Unit> UserPasswordProcess(User user, RegisterUserResolvedCommand command)
        {
            if(string.IsNullOrWhiteSpace(command.Password))
                return Response<Unit>.Error("Password is cannot be empty!", (int)HttpStatusCode.BadRequest);

            if(command.Password.Length < 6 || !command.Password.Any(x => char.IsUpper(x))
                || !command.Password.Any(x => char.IsLower(x)) || !command.Password.Any(x => char.IsNumber(x)))
                return Response<Unit>.Error("Password is not valid!", (int)HttpStatusCode.BadRequest);

            user.Password = PasswordGenerator.Hash(command.Password);
            return Response<Unit>.Success((int)HttpStatusCode.OK);
        }

        private async Task UserRolesProcessAsync(User user, RegisterUserResolvedCommand command)
        {
            if(command.Roles is not null && command.Roles.Any())
                await _userRoleRepository.AddRoleToUserAsync(command.Roles, user);    
        }
    }
}
