namespace NoGravity.Data.Repositories.Interfaces
{
    public interface ICarrierRepository
    {
        void AddCarrier(Carrier carrier);
        void DeleteCarrier(Carrier carrier);
        IEnumerable<Carrier> GetAllCarriers();
        Carrier GetCarrierById(int id);
        void SaveChanges();
        void UpdateCarrier(Carrier carrier);
    }
}