namespace NoGravity.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDTO> CreateUser(UserDTO userDTO);
        Task<bool> DeleteUser(int userId);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserById(int userId);
        Task<UserDTO> UpdateUser(int userId, UserDTO userDTO);
    }
}