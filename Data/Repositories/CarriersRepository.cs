namespace NoGravity.Data.Repositories
{
    public class CarrierRepository : ICarrierRepository
    {
        private readonly NoGravityDbContext _dbContext;

        public CarrierRepository(NoGravityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CarrierDTO>> GetAllCarriers()
        {
            var carriers = await _dbContext.Carriers
                .Select(c => new CarrierDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync();

            return carriers;
        }

        public async Task<CarrierDTO> GetCarrierById(int carrierId)
        {
            var carrier = await _dbContext.Carriers
                .Where(c => c.Id == carrierId)
                .Select(c => new CarrierDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .FirstOrDefaultAsync();

            return carrier;
        }

        public async Task<CarrierDTO> CreateCarrier(CarrierDTO carrierDTO)
        {
            var carrier = new Carrier
            {
                Name = carrierDTO.Name,
                Description = carrierDTO.Description
            };

            _dbContext.Carriers.Add(carrier);
            await _dbContext.SaveChangesAsync();

            carrierDTO.Id = carrier.Id;
            return carrierDTO;
        }

        public async Task<CarrierDTO> UpdateCarrier(int carrierId, CarrierDTO carrierDTO)
        {
            var existingCarrier = await _dbContext.Carriers.FirstOrDefaultAsync(c => c.Id == carrierId);

            if (existingCarrier == null)
                return null;

            existingCarrier.Name = carrierDTO.Name;
            existingCarrier.Description = carrierDTO.Description;

            await _dbContext.SaveChangesAsync();

            return carrierDTO;
        }

        public async Task<bool> DeleteCarrier(int carrierId)
        {
            var carrier = await _dbContext.Carriers.FirstOrDefaultAsync(c => c.Id == carrierId);

            if (carrier == null)
                return false;

            _dbContext.Carriers.Remove(carrier);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
