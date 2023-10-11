using MassTransitPov.AWS.SNS.Consumers.Models;

namespace MassTransitPov.AWS.SNS.Consumers.Repositories;

public interface IReservationRepository
{
    Task<ReservationEntity> Save(ReservationEntity model);
}