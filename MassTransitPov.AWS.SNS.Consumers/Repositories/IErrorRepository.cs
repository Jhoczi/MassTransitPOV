using MassTransitPov.AWS.SNS.Consumers.Models;

namespace MassTransitPov.AWS.SNS.Consumers.Repositories;

public interface IErrorRepository
{
    Task Save(ErrorEntity model);
}