using API.Entities;

namespace API.Service
{
    public interface IRoleRepository
    {
        Task<Role> Create(Role role);
        Task<Role> Update(Role role);
        Task<Role> Delete(Role role);
        Task<Role?> GetByID(Guid ID);
        Task<Role?> GetByName(String? name);
        Task<ICollection<Role>> GetRoleList();
    }
}
