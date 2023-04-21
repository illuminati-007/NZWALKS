using AutoMapper;
using FP_NZWALKS.Data;
using FP_NZWALKS.Models.Domain;
using FP_NZWALKS.Models.DTO;
using FP_NZWALKS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FP_NZWALKS.CustomActionFilters;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace FP_NZWALKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper,
            ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        //GET  ALL REGIONS
        // GET: http://localhost:5173/api/regions
        [HttpGet]
      //  [Authorize(Roles ="Reader, Writer")]
        public async Task<IActionResult> GetAll()
        {
            
                
                // Get Data from Database - DomainModels
                var regionsDomain = await regionRepository.GetAllAsync();

                // Map Domain models in DTOs
                logger.LogInformation($"Finished GetallRegions request with data : {JsonSerializer.Serialize(regionsDomain)}");

                return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
            
        }


        // GET SINGLE REGION(Get region by ID)
        // GET: http://localhost:5173/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader , Writer")]
        public async Task<IActionResult> GetById([FromRoute]Guid id) {
            //var region = dbContext.Regions.Find(id);

            // Get region domain model from Database 
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null) {
                return NotFound();  
            }

            // Convert region domain model in to Region Dtos
            
            // Return Dto back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }


        //POST  to create new region 
        //POST: http://localhost:5173/api/regions/{id}
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
                //Map or convert Dto to DM
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                //Use Domain model to create Region
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                //MAp domain model back to dto

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);        
        }

        //Update region 
        //PUT: http://localhost:5173/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
                // map dto to 
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                //Check if region 
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }
                //convert domain model to 
                return Ok(mapper.Map<RegionDto>(regionDomainModel));
            }
           
      
        //Delete region
        //DELETE: http://localhost:5173/api/regions/{id}

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //map domain into dto
            
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
