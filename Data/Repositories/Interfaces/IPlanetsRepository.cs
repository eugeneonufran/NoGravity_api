using NoGravity.Data.DTO.Planets;

namespace NoGravity.Data.Repositories.Interfaces
{
    public interface IPlanetsRepository
    {
        Task<PlanetDTO> CreatePlanet(PlanetDTO planetDTO);
        Task<bool> DeletePlanet(int planetId);
        Task<IEnumerable<PlanetDTO>> GetAllPlanets();
        Task<PlanetDTO> GetPlanetById(int planetId);
        Task<PlanetDTO> UpdatePlanet(int planetId, PlanetDTO planetDTO);
    }
}