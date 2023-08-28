using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/{controller}")]
    public class OfferController : Controller
    {
        private readonly IOfferRepository _offerRepository;

        public OfferController(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetOffers()
        {
            var offers = _offerRepository.GetOffers();
            return Ok(offers);
        }
    }
}
