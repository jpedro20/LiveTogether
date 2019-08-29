using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using LiveTogether.Data.Repositories;
using LiveTogether.Models.Dto;

namespace LiveTogether.Controllers
{
    [Route("api/[controller]")]
    public class WarrantiesController : Controller
    {
        private readonly IWarrantiesRepository _warrantiesRep;
        private readonly IMapper _mapper;

        public WarrantiesController(IWarrantiesRepository warrantiesRep, IMapper mapper)
        {
            _warrantiesRep = warrantiesRep;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetWarranties()
        {
            var warranties = await _warrantiesRep.GetAll();
            var warrantiesDto = _mapper.Map<List<WarrantyDto>>(warranties);

            return Ok(warrantiesDto);
        }
    }
}