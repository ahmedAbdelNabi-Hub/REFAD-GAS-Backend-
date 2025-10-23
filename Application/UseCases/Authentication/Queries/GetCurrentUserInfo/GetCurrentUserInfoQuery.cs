using Application.DTOs.Auth;
using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Authentication.Queries.GetCurrentUserInfo
{
    public class GetCurrentUserInfoQuery : IRequest<BaseApiResponse<UserInfoDto>>
    {
        public string Email { get; set; } = string.Empty;

        public GetCurrentUserInfoQuery(string email)
        {
            Email = email.Trim().ToLower();
        }
    }
}
