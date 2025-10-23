using Application.DTOs.Admins;
using Application.DTOs;
using Domain.Specification.Params;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Admin.Queries.GetAllAdmins
{
    public class GetAllAdminsQuery : IRequest<PaginationDTO<AdminDto>>
    {
        public PaginationParams paginationParams { get; }
        public AdminParams adminParams { get; }

        public GetAllAdminsQuery(PaginationParams paginationParams, AdminParams adminParams)
        {
            this.paginationParams = paginationParams;
            this.adminParams = adminParams;
        }
    }
}
