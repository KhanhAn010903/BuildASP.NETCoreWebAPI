using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController(IRegionRepository regionRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var regionsDomain = await regionRepository.GetAllAsync();
        var regionsDTO = mapper.Map<List<RegionDTO>>(regionsDomain);
        return Ok(regionsDTO);
    }
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var regionDomain = await regionRepository.GetByIdAsync(id);
        if (regionDomain == null)
        {
            return NotFound();
        }
        var regionDto = mapper.Map<RegionDTO>(regionDomain);
        return Ok(regionDto);
    }

    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
    {
        var regionDomainModel = mapper.Map<Region>(addRegionRequestDTO);
        regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
        var regionDto = mapper.Map<RegionDTO>(regionDomainModel);
        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }


    [HttpPut]
    [Route("{id:Guid}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
    {

            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null) return NotFound();
            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regionDto);
    }
    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var regionDomainModel = await regionRepository.DeleteAcync(id);
        if (regionDomainModel == null) return NotFound();
        var regionDto = mapper.Map<RegionDTO>(regionDomainModel);
        return Ok(regionDto);
    }

}