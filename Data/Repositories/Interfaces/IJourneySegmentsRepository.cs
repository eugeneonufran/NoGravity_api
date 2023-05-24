namespace NoGravity.Data.Repositories.Interfaces
{
    public interface IJourneySegmentsRepository
    {
        Task<JourneySegmentDTO> CreateJourneySegment(JourneySegmentDTO journeySegmentDTO);
        Task<bool> DeleteJourneySegment(int journeySegmentId);
        Task<IEnumerable<JourneySegmentDTO>> GetAllJourneySegments();
        Task<JourneySegmentDTO> GetJourneySegmentById(int journeySegmentId);
        Task<JourneySegmentDTO> UpdateJourneySegment(int journeySegmentId, JourneySegmentDTO journeySegmentDTO);
    }
}