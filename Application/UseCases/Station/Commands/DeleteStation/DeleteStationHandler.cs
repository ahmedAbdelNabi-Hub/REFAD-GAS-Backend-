using Application.DTOs.Response;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Station.Commands.DeleteStation
{
    public class DeleteStationHandler : IRequestHandler<DeleteStationCommand, BaseApiResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseApiResponse<int>> Handle(DeleteStationCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Domain.Entities.Station>();
            var station = await repo.GetByIdAsync(request.Id);

            if (station == null)
                return new BaseApiResponse<int>(404, "Station not found.");

            await repo.DeleteAsync(station);
            await _unitOfWork.SaveChangeAsync();

            return new BaseApiResponse<int>(200, "Station deleted successfully.");
        }
    }
}
