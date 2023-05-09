﻿using Microsoft.EntityFrameworkCore;
using NoGravity.Data.DataModel;
using NoGravity.Data.Repositories.Interfaces;
using NoGravity.Data.Tables;

namespace NoGravity.Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly NoGravityDbContext _dbContext;

        public TicketRepository(NoGravityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Ticket> GetById(Guid id)
        {
            return await _dbContext.Tickets.FindAsync(id);
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _dbContext.Tickets.ToListAsync();
        }

        public async Task Create(Ticket ticket)
        {
            await _dbContext.Tickets.AddAsync(ticket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Ticket ticket)
        {
            _dbContext.Tickets.Update(ticket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var ticket = await GetById(id);
            if (ticket != null)
            {
                _dbContext.Tickets.Remove(ticket);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}