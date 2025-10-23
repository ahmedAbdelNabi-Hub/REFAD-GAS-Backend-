using Application.DTOs.Auth;
using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Authentication.Queries.Login
{
    public class LoginQuery : IRequest<BaseApiResponse<AuthResponseDto>>
    {
        public LoginDto LoginDto { get; }

        public LoginQuery(LoginDto loginDto)
        {
            LoginDto = loginDto;
        }
    }
}
