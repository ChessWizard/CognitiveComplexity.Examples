using Core.Entities;
using Core.Repositories;
using Core.UnitofWork;
using MediatR;
using Service.Common;
using Shared.ResponseObjects;
using System.ComponentModel.DataAnnotations;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Service.CQRS.Commands.RegisterUser
{
    public class RegisterUserComplexCommandHandler : IRequestHandler<RegisterUserComplexCommand, Response<Unit>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitofWork _unitofWork;
        private readonly IUserRoleRepository _userRoleRepository;

        public RegisterUserComplexCommandHandler(IUserRepository userRepository, IUnitofWork unitofWork, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _unitofWork = unitofWork;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<Response<Unit>> Handle(RegisterUserComplexCommand command, CancellationToken cancellationToken)
        {
            User user = new();

            if (!string.IsNullOrWhiteSpace(command.Name))
            {
                if(command.Name.Length <= 20)
                {
                    if(command.Name.Length > 2)
                    {
                        user.Name = command.Name;
                    }

                    else
                        return Response<Unit>.Error("Name must be longer than 2 characters!", (int)HttpStatusCode.BadRequest);
                }
                else
                    return Response<Unit>.Error("Name is not valid!", (int)HttpStatusCode.BadRequest);
            }
            else
                return Response<Unit>.Error("Name is cannot be empty!", (int)HttpStatusCode.BadRequest);

            if (!string.IsNullOrWhiteSpace(command.Surname))
            {
                if (command.Surname.Length <= 20)
                {
                    if (command.Surname.Length > 2)
                        user.Surname = command.Surname;

                    else
                        return Response<Unit>.Error("Surname must be longer than 2 characters!", (int)HttpStatusCode.BadRequest);
                }
                else
                    return Response<Unit>.Error("Surname is not valid!", (int)HttpStatusCode.BadRequest);
            }
            else
                return Response<Unit>.Error("Surname is cannot be empty!", (int)HttpStatusCode.BadRequest);

            if (!string.IsNullOrWhiteSpace(command.Email))
            {
                var emailService = new EmailAddressAttribute();
                var isValidEmail = emailService.IsValid(command.Email);

                if (!isValidEmail)
                    return Response<Unit>.Error("Email is not valid!", (int)HttpStatusCode.BadRequest);
                else
                    user.Email = command.Email;
            }
            else
                return Response<Unit>.Error("Email is cannot be empty!", (int)HttpStatusCode.BadRequest);

            if (!string.IsNullOrWhiteSpace(command.Password))
            {
                if(command.Password.Length > 6)
                {
                    if(command.Password.Any(x => char.IsUpper(x)) && command.Password.Any(x => char.IsLower(x)) 
                        && command.Password.Any(x => char.IsNumber(x)))
                    user.Password = PasswordGenerator.Hash(command.Password);

                    else
                        return Response<Unit>.Error("Password is not valid!", (int)HttpStatusCode.BadRequest);
                }
                else
                    return Response<Unit>.Error("Password must be longer than 6 characters!", (int)HttpStatusCode.BadRequest);
            }
            else
                return Response<Unit>.Error("Password is cannot be empty!", (int)HttpStatusCode.BadRequest);

            if ((int)command.AccountType < 0 || (int)command.AccountType > 3)
                return Response<Unit>.Error("Enum is not valid!", (int)HttpStatusCode.BadRequest);
            else
                user.AccountType = command.AccountType;

            if(command.Roles is not null)
            {
                if(command.Roles.Any())
                     await _userRoleRepository.AddRoleToUserAsync(command.Roles, user);
            }

            await _userRepository.AddUserAsync(user);
            await _unitofWork.SaveChangesAsync();

            return Response<Unit>.Success(Unit.Value, (int)HttpStatusCode.OK);
        }
    }
}
