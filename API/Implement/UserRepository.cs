using API.Data;
using API.Entities;
using API.Service;
using Microsoft.EntityFrameworkCore;
using Model.Dto;

namespace API.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly CinemaDbContext _context;

        public UserRepository(CinemaDbContext context) 
        {
            _context = context;
        }

        public List<User> GetUserList(Pagination query)
        {
            var result = _context.Users.AsQueryable().OrderBy(x => x.FirstName + " " + x.LastName);
            var list = PaginatedList<User>.Create(result, query);
            return list;
        }
    }
}
