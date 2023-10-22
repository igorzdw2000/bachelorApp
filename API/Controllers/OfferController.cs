using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/{controller}")]
    public class OfferController : Controller
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IServiceRepository _serviceRepository;

        public OfferController(IOfferRepository offerRepository, IMapper mapper, ICustomerRepository customerRepository, IServiceRepository serviceRepository)
        {
            _offerRepository = offerRepository;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _serviceRepository = serviceRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetOffers()
        {
            var offers = await _offerRepository.GetOffers();
            return Ok(offers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfferById(int id)
        {
            var offer = await _offerRepository.GetOfferById(id);
            return Ok(offer);
        }
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetOfferDetails(int id)
        {
            var offerDetails = await _offerRepository.GetOfferDetailById(id);
            return Ok(offerDetails);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOffer([FromBody] OfferDto offerDto)
        {
            if(offerDto == null) throw new ArgumentNullException(nameof(offerDto));
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            var customer = await _customerRepository.GetCustomerByIdAsync(offerDto.CustomerId);
            if(customer != null)
            {
                var offer = new Offer
                {
                    OfferDate = offerDto.OfferDate,
                    Address = offerDto.Address,
                    Phone = offerDto.Phone,
                    Email = offerDto.Email,
                    Description = offerDto.Description,
                    TotalEstimatedLaborCost = offerDto.TotalEstimatedLaborCost,
                    TotalEstimatedMaterialCost = offerDto.TotalEstimatedMaterialCost,
                    Customer = customer
                };
                await _offerRepository.AddOffer(offer);
                return CreatedAtAction(nameof(GetOfferById), new { id = offer.OfferId }, offer);
            }
            return BadRequest("Cannot find cusotmer with typed id");

        }
        [HttpPost("{details}")]
        public async Task<IActionResult> AddOfferDetails([FromBody] OfferDetailDto offerDetailDto)
        {
            if(offerDetailDto == null) throw new ArgumentNullException(nameof(offerDetailDto));
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            var offer = await _offerRepository.GetOfferById(offerDetailDto.OfferId);
            var service = await _serviceRepository.GetServiceById(offerDetailDto.ServiceId);
            if(offer != null && service != null)
            {
                var offerDetail = new OfferDetail
                {
                    Offer = offer,
                    Service = service,
                    EstimatedLaborCost = offerDetailDto.EstimatedLaborCost,
                    EstimatedMaterialCost = offerDetailDto.EstimatedMaterialCost,
                    EstimatedTime = offerDetailDto.EstimatedTime
                };
                await _offerRepository.AddOfferDetails(offerDetail);
                return Ok(offerDetail);
            }
            return BadRequest("Something went wrong during adding offer details");
        }

    }
}
