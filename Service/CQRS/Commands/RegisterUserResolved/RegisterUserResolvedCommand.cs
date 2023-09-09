using Core.Dto;
using Core.Enums;
using MediatR;
using Shared.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CQRS.Commands.RegisterUserResolved
{
    public class RegisterUserResolvedCommand : IRequest<Response<Unit>>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public AccountType AccountType { get; set; }

        public List<RoleDto> Roles { get; set; }
    }
}
