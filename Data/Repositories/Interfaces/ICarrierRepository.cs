namespace NoGravity.Data.Repositories.Interfaces
{
    public interface ICarrierRepository
    {
        Task<CarrierDTO> CreateCarrier(CarrierDTO carrierDTO);
        Task<bool> DeleteCarrier(int carrierId);
        Task<IEnumerable<CarrierDTO>> GetAllCarriers();
        Task<CarrierDTO> GetCarrierById(int carrierId);
        Task<CarrierDTO> UpdateCarrier(int carrierId, CarrierDTO carrierDTO);
    }
}