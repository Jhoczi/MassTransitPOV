using DbProvider.Abstract;
using MassTransitPov.AWS.SNS.Consumers.Models;

namespace MassTransitPov.AWS.SNS.Consumers.Repositories;

public class ErrorRepository : IErrorRepository
{
    private readonly IDataProvider<ErrorEntity> _dataProvider;

    public ErrorRepository(IDataProvider<ErrorEntity> dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task Save(ErrorEntity model)
    {
        await _dataProvider.Create(model);
    }
}