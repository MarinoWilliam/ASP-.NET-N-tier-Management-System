using Microsoft.EntityFrameworkCore;
using NTier.ManagementSystem.Data;
using NTier.ManagementSystem.Domain.Entities;
using NTier.ManagementSystem.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Service.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly ManagementDbContext _managementDbContext;

        public TeamService(ManagementDbContext managementDbContext)
        {
            _managementDbContext = managementDbContext;
        }

        public async Task<Team> AddTeamAsync(Team team)
        {
            _managementDbContext.Teams.Add(team);
            await _managementDbContext.SaveChangesAsync();
            return team;
        }

        public async Task<bool> DeleteTeamAsync(int id)
        {
            var team = await _managementDbContext.Teams.FindAsync(id);
            if (team == null)
                return false;

            _managementDbContext.Teams.Remove(team);
            await _managementDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _managementDbContext.Teams
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Team?> GetTeamByIdAsync(int id)
        {
            return await _managementDbContext.Teams.FindAsync(id);
        }

        public async Task<Team?> UpdateTeamAsync(Team team)
        {
            var existing = await _managementDbContext.Teams.FindAsync(team.Id);
            if (existing == null)
                return null;

            existing.Name = team.Name;
            existing.Description = team.Description;
            existing.DepartmentId = team.DepartmentId;

            await _managementDbContext.SaveChangesAsync();
            return existing;
        }
    }
}
