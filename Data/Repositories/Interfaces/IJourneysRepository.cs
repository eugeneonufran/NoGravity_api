using NoGravity.Data.DTO.Journey;

namespace NoGravity.Data.Repositories.Interfaces
{
    public interface IJourneysRepository
    {
        Task<JourneyDTO> CreateJourney(JourneyDTO journeyDTO);
        Task<bool> DeleteJourney(int journeyId);
        Task<IEnumerable<JourneyDTO>> GetAllJourneys();
        Task<JourneyDTO> GetJourneyById(int journeyId);
        Task<JourneyDTO> UpdateJourney(int journeyId, JourneyDTO journeyDTO);
    }
}