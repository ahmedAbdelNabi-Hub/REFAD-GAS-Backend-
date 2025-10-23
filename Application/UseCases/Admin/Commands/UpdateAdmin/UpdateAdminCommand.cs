using Application.DTOs.Admins;
using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Admin.Commands.UpdateAdmin
{
    public class UpdateAdminCommand : IRequest<BaseApiResponse<int>>
    {
        public Guid Id { get; set; }
        public AdminDto AdminDto { get; set; }

        public UpdateAdminCommand(Guid id, AdminDto adminDto)
        {
            Id = id;
            AdminDto = adminDto;
        }
    }
}
