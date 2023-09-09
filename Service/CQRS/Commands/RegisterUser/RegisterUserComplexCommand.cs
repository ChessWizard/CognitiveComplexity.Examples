using Core.Dto;
using Core.Enums;
using MediatR;
using Shared.ResponseObjects;

namespace Service.CQRS.Commands.RegisterUser
{
    public class RegisterUserComplexCommand : IRequest<Response<Unit>>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public AccountType AccountType { get; set; }

        public List<RoleDto>? Roles { get; set; }
    }
}
