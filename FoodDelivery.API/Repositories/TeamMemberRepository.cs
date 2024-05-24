using FoodDelivery.API.Constants;
using FoodDelivery.API.Models;
using Newtonsoft.Json;

namespace FoodDelivery.API.Repositories;

public interface ITeamMemberRepository
{
    List<TeamMember> GetAll();
}

public class TeamMemberRepository : ITeamMemberRepository
{
    private static readonly List<TeamMember> teamMembers;

    static TeamMemberRepository()
    {
        var teamMembersJson = File.ReadAllText(ResourcePaths.TeamMembers);
        teamMembers = JsonConvert.DeserializeObject<List<TeamMember>>(teamMembersJson);
    }

    public List<TeamMember> GetAll()
    {
        return teamMembers;
    }
}
