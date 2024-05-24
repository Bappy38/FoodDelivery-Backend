using FoodDelivery.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamsController : ControllerBase
{
    private readonly ITeamMemberRepository _teamMemberRepository;

    public TeamsController(ITeamMemberRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var members = _teamMemberRepository.GetAll();
        return Ok(members);
    }
}
