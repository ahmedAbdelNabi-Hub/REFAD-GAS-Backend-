using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Admin.Commands.DeleteAdmin
{
    public class DeleteAdminCommand : IRequest<BaseApiResponse>
    {
        public Guid Id { get; set; }

        public DeleteAdminCommand(Guid id)
        {
            Id = id;
        }
    }
}
