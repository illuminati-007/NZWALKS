using AutoMapper;
using FP_NZWALKS.CustomActionFilters;
using FP_NZWALKS.Models.Domain;
using FP_NZWALKS.Models.DTO;
using FP_NZWALKS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FP_NZWALKS.Controllers
{
    // /api/walks/
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {

            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }



        //Create Walk
        //POST: /api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                //Map dto to dm
                var WalkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
                await walkRepository.CreateAsync(WalkDomainModel);
                //map dm to dto

                return Ok(mapper.Map<WalkDto>(WalkDomainModel));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        //Get Walk
        //POST: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery]string? sortBy, [FromQuery]bool? isAscending, [FromQuery]int pageNumber = 1, [FromQuery]int pageSize=1000)
        {

            var WalksDomainModel = await walkRepository.GetAllAsync(filterOn,filterQuery,sortBy,isAscending ?? true,  pageNumber ,  pageSize );

            //Map dm to dto

            return Ok(mapper.Map<List<WalkDto>>(WalksDomainModel));
        }


        //Get walk by id 
        //GET: /api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var WalkDomainModel = await walkRepository.GetByIdAsync(id);
            if (WalkDomainModel == null)
            {
                return NotFound();
            }
            //Map dm to dto

            return Ok(mapper.Map<WalkDto>(WalkDomainModel));

        }


        //Update walk by id
        //PUT: /api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            //Map  Dto to dm
            var WalkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            WalkDomainModel = await walkRepository.UpdateAsync(id, WalkDomainModel);

            if (WalkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(WalkDomainModel));

        }

        //Delete a walk by id
        //DELETE: /api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }
            //Map dm to dto

            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));

        }
    }
}

 
    

