using API.Entities;
using Model.Dto;

namespace API.Service
{
    public interface IUserRepository
    {
        List<User> GetUserList(Pagination query);
    }
}
