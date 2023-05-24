namespace NoGravity.Data.Repositories
{
    public class CarrierRepository : ICarrierRepository
    {
        private readonly NoGravityDbContext _dbContext;

        public CarrierRepository(NoGravityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Carrier> GetAllCarriers()
        {
            return _dbContext.Carriers.ToList();
        }

        public Carrier GetCarrierById(int id)
        {
            return _dbContext.Carriers.Find(id);
        }

        public void AddCarrier(Carrier carrier)
        {
            _dbContext.Carriers.Add(carrier);
        }

        public void UpdateCarrier(Carrier carrier)
        {
            _dbContext.Carriers.Update(carrier);
        }

        public void DeleteCarrier(Carrier carrier)
        {
            _dbContext.Carriers.Remove(carrier);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}

