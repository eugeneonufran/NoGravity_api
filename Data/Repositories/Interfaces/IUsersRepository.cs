namespace NoGravity.Data.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> CreateUser(User user);

        Task<User> GetUserByEmail(string email);
        Task<bool> DeleteUser(int userId);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<User> GetUserById(int userId);
        Task<UserDTO> UpdateUser(int userId, UserDTO userDTO);
    }
}