using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController(NZWalksDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var regionsDomain = dbContext.Regions.ToList();
        var regionsDTO = new List<RegionDTO>();
        foreach (var regionDomain in regionsDomain)
        {
            regionsDTO.Add(new RegionDTO()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            });
        }
        return Ok(regionsDTO);
    }
    [HttpGet]
    [Route("{id:Guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var regionDomain = dbContext.Regions.Find(id);
        if (regionDomain == null)
        {
            return NotFound();
        }
        var regionDto = new RegionDTO()
        {
            Id = regionDomain.Id,
            Code = regionDomain.Code,
            Name = regionDomain.Name,
            RegionImageUrl = regionDomain.RegionImageUrl
        };
        return Ok(regionDto);
    }

    [HttpPost]
    public IActionResult Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
    {
        var regionDomainModel = new Region
        {
            Code = addRegionRequestDTO.Code,
            Name = addRegionRequestDTO.Name,
            RegionImageUrl = addRegionRequestDTO.RegionImageUrl
        };
        dbContext.Regions.Add(regionDomainModel);
        dbContext.SaveChanges();

        var regionDto = new RegionDTO()
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };
        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
    {
        var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
        if (regionDomainModel == null) NotFound();
        regionDomainModel.Code = updateRegionRequestDTO.Code;
        regionDomainModel.Name = updateRegionRequestDTO.Name;
        regionDomainModel.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;
        dbContext.SaveChanges();
        var regionDto = new RegionDTO()
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };
        return Ok(regionDto);
    }
    [HttpDelete]
    [Route("{id:Guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
        if (regionDomainModel == null) return NotFound();
        dbContext.Regions.Remove(regionDomainModel);
        dbContext.SaveChanges();
        var regionDto = new RegionDTO()
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };
        return Ok(regionDto);
    }

}