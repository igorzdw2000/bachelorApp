using Core.Entities;
using Core.Interfaces;
using Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Infrastracture.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly AppDbContext _context;

        public OfferRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddOffer(Offer offer)
        {
            if(offer == null) throw new ArgumentNullException(nameof(offer));
             _context.Offers.Add(offer);
             _context.SaveChanges();
        }

        public async Task AddOfferDetails(OfferDetail offerDetail)
        {
            if(offerDetail == null) throw new ArgumentNullException(nameof(offerDetail));
            _context.OfferDetails.Add(offerDetail);
            _context.SaveChanges();
        }

        public async Task DeleteOffer(int id)
        {
            var offerToDelte = await _context.Offers.FindAsync(id);
            if(offerToDelte != null)
            {
                _context.Offers.Remove(offerToDelte);
            }
            _context.SaveChanges();
        }

        public async Task<Offer> GetOfferById(int id)
        {
            var offer = await _context.Offers
                .Include(s=>s.Customer)
                .FirstOrDefaultAsync(o=>o.OfferId == id);
            return offer;
        }

        public async Task<OfferDetail> GetOfferDetailById(int offerId)
        {
            var offerDetails = await _context.OfferDetails
                .Include(s => s.Service)
                .FirstOrDefaultAsync(o => o.OfferId == offerId);
            return offerDetails;
        }

        public async Task<IEnumerable<Offer>> GetOffers()
        {
            var offers = await _context.Offers
                .Include(c => c.Customer)
                .ToListAsync();
            return offers;
        }

        public async Task UpdateOffer(Offer offer)
        {
            if(offer == null)
            {
                throw new ArgumentNullException(nameof(offer));
            }
            var existingOffer = await _context.Offers.FindAsync(offer.OfferId);
            if (existingOffer != null)
            {
                _context.Offers.Update(offer);
                _context.SaveChanges();
            }
            
        }
    }
}
