using DbProvider.Abstract;
using MassTransitPov.AWS.SNS.Consumers.Models;

namespace MassTransitPov.AWS.SNS.Consumers.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly IDataProvider<ReservationEntity> _dataProvider;

    public ReservationRepository(IDataProvider<ReservationEntity> dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task<ReservationEntity> Save(ReservationEntity model)
    {
        if (model.ProcessId == null)
            throw new ArgumentNullException(nameof(model.ProcessId), "The value is required.");

        var result = await _dataProvider.Create(model);

        return result;
    }
}