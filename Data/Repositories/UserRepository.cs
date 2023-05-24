namespace NoGravity.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NoGravityDbContext _dbContext;

        public UserRepository(NoGravityDbContext dbContext)
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

        public async Task<UserDTO> GetUserById(int userId)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                Email = user.Email
            };
        }

        public async Task<UserDTO> CreateUser(UserDTO userDTO)
        {
            var user = new User
            {
                FirstName = userDTO.FirstName,
                SecondName = userDTO.SecondName,
                Email = userDTO.Email,
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            userDTO.Id = user.Id;
            return userDTO;
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
    }
}