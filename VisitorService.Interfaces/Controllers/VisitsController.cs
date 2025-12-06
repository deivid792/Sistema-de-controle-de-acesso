using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VisitorService.Application.DTOS;
using VisitorService.Application.Interfaces;
using VisitorService.Interfaces.Extensions;

namespace VisitorService.Interfaces.Controllers
{
    [ApiController]
    [Route("api/visits")]
    public class VisitorController : ControllerBase
    {
        private readonly IUpdateVisitStatusHandler _updateVisitStatusHandler;
        private readonly IGetTodayApprovedVisitsHandler _getTodayApprovedVisitsHandler;
        private readonly IVisitCheckInHandler _visitCheckInHandler;
        private readonly IVisitCheckOutHandler _visitCheckOutHandler;
        private readonly IGetAllVisitsHandler _getAllVisitsHandler;
        private readonly IcreateVisitHandler _createVisitHandler;

        public VisitorController(
            IUpdateVisitStatusHandler updateVisitStatusHandler,
            IGetTodayApprovedVisitsHandler getTodayApprovedVisitsHandler,
            IVisitCheckInHandler visitCheckInHandler,
            IVisitCheckOutHandler visitCheckOutHandler,
            IGetAllVisitsHandler getAllVisitsHandler,
            IcreateVisitHandler createVisitHandler)
        {
            _updateVisitStatusHandler = updateVisitStatusHandler;
            _getTodayApprovedVisitsHandler = getTodayApprovedVisitsHandler;
            _visitCheckInHandler = visitCheckInHandler;
            _visitCheckOutHandler = visitCheckOutHandler;
            _getAllVisitsHandler = getAllVisitsHandler;
            _createVisitHandler = createVisitHandler;
        }

        [Authorize(Roles = "Gestor")]
        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateVisitStatusDto dto)
        {
            var gestorId = User.GetUserId();
            var result = await _updateVisitStatusHandler.Handle(dto, gestorId);

            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [Authorize(Roles = "Porteiro")]
        [HttpGet("today/approved")]
        public async Task<IActionResult> GetTodayApproved()
        {
            var result = await _getTodayApprovedVisitsHandler.Handle();

            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [Authorize(Roles = "Porteiro")]
        [HttpPost("checkin")]
        public async Task<IActionResult> CheckIn([FromBody] VisitCheckDto dto)
        {
            var result = await _visitCheckInHandler.Handle(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [Authorize(Roles = "Porteiro")]
        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut([FromBody] VisitCheckDto dto)
        {
            var result = await _visitCheckOutHandler.Handle(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [Authorize(Roles = "Gestor")]
        [HttpGet("gestor")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllVisitsHandler.Handle();

            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateVisit([FromBody] CreateVisitDto dto)
        {
            var result = await _createVisitHandler.Handler(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
    }
}
