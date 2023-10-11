using MassTransit;
using MassTransitPov.AWS.SNS.Producer.DTOs;
using MassTransitPov.Common.Messages;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitPov.AWS.SNS.Producer.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ReservationController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<ActionResult> CreateTicket(CreateReservationRequest request)
    {
        await _publishEndpoint.Publish(new NewReservationMessage
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Email = request.Email,
            ScreeningDate = request.ScreeningDate
        });
        
        return StatusCode(StatusCodes.Status201Created, new {
            message = "Ticket created.",
        });
    }
}