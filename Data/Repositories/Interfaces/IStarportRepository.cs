using NoGravity.Data.DTO.Starports;
namespace NoGravity.Data.Repositories.Interfaces
{
    public interface IStarportRepository
    {
        Task<StarportDTO> CreateStarport(StarportDTO starportDTO);
        Task<bool> DeleteStarport(int starportId);
        Task<IEnumerable<StarportDTO>> GetAllStarports();
        Task<StarportDTO> GetStarportById(int starportId);
        Task<StarportDTO> UpdateStarport(int starportId, StarportDTO starportDTO);
    }
}