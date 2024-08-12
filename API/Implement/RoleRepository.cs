using API.Data;
using API.Entities;
using API.Service;
using Microsoft.EntityFrameworkCore;

namespace API.Implement
{
    public class RoleRepository : IRoleRepository
    {
        private readonly CinemaDbContext _context;

        public RoleRepository(CinemaDbContext context) 
        {
            _context = context;
        }

        public async Task<Role> Create(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> Delete(Role role)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<ICollection<Role>> GetRoleList()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetByID(Guid ID)
        {
            return await _context.Roles.FindAsync(ID);
        }

        public async Task<Role?> GetByName(String? Name)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(x => x.Name == Name);
            return role;
        }

        public async Task<Role> Update(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return role;
        }
    }
}
