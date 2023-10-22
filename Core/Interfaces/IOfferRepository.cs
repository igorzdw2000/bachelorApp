using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Core.Interfaces
{
    public interface IOfferRepository
    {
        Task<IEnumerable<Offer>> GetOffers();
        Task<Offer> GetOfferById(int id);
        Task AddOffer(Offer offer);
        Task AddOfferDetails(OfferDetail offerDetail);
        Task DeleteOffer(int id);
        Task UpdateOffer(Offer offer);
        Task<OfferDetail> GetOfferDetailById(int offerId);

    }
}
