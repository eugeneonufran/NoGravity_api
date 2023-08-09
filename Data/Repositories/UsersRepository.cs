namespace NoGravity.Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly NoGravityDbContext _dbContext;

        public UsersRepository(NoGravityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = await _dbContext.Users
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    SecondName = u.SecondName,
                    Email = u.Email
                })
                .ToListAsync();

            return users;
        }

        public async Task<User> GetUserById(int userId)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;

            return user;
        }

        public async Task<User> CreateUser(User user)
        {

            _dbContext.Users.Add(user);

           user.Id=_dbContext.SaveChanges();

            return user;
        }

        public async Task<UserDTO> UpdateUser(int userId, UserDTO userDTO)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (existingUser == null)
                return null;

            existingUser.FirstName = userDTO.FirstName;
            existingUser.SecondName = userDTO.SecondName;
            existingUser.Email = userDTO.Email;

            await _dbContext.SaveChangesAsync();

            return userDTO;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}