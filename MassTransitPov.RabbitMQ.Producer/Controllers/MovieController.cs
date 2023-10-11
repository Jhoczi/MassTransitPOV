using MassTransit;
using MassTransitPov.Common.Messages;
using MassTransitPov.RabbitMQ.Producer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitPov.RabbitMQ.Producer.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MovieController  : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public MovieController(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
    {
        _publishEndpoint = publishEndpoint;
        _sendEndpointProvider = sendEndpointProvider;
    }

    [HttpPost]
    public async Task<ActionResult> AddMovie(AddMovieRequest movieModel)
    {
        if (movieModel.Title == null || movieModel.Creator == null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new {
                message = "Invalid request data value."
            });
        }
        
        movieModel.Id = Guid.NewGuid();
        await _publishEndpoint.Publish(new NewMovieMessage
        {
            Id = movieModel.Id,
            Title = movieModel.Title,
            Creator = movieModel.Creator,
            Description = movieModel.Description,
            TimeStamp = movieModel.TimeStamp,
            YearOfCreation = movieModel.YearOfCreation
        });
        
        // var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:new-movie-queue"));
        // await endpoint.Send(new NewMovieMessage
        // {
        //     Id = movieModel.Id,
        //     Title = movieModel.Title,
        //     Creator = movieModel.Creator,
        //     Description = movieModel.Description,
        //     TimeStamp = movieModel.TimeStamp,
        //     YearOfCreation = movieModel.YearOfCreation
        // });
        
        return StatusCode(StatusCodes.Status201Created, new {
            message = "Operation finished with success.",
            movieModel.Id
        });
    }
}