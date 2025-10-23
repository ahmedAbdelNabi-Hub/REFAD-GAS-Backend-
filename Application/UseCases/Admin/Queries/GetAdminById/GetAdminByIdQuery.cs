using Application.DTOs.Admins;
using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Admin.Queries.GetAdminById
{
    public class GetAdminByIdQuery : IRequest<BaseApiResponse<AdminDto>>
    {
        public Guid Id { get; set; }

        public GetAdminByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
