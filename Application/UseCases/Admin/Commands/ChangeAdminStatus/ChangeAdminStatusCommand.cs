using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Admin.Commands.ChangeAdminStatus
{
    public class ChangeAdminStatusCommand : IRequest<BaseApiResponse>
    {
        public Guid AdminId { get; set; }
        public bool IsActive { get; set; }

        public ChangeAdminStatusCommand(Guid adminId, bool isActive)
        {
            AdminId = adminId;
            IsActive = isActive;
        }
    }
}
