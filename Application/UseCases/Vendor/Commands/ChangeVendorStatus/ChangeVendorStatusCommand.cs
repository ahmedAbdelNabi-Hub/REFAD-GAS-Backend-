using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Vendor.Commands.ChangeVendorStatus
{
    public class ChangeVendorStatusCommand : IRequest<BaseApiResponse>
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }

        public ChangeVendorStatusCommand(Guid id, bool isActive)
        {
            Id = id;
            IsActive = isActive;
        }
    }
}
