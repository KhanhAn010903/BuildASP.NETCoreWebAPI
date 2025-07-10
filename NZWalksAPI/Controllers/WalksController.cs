using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalksController(IMapper mapper, IWalkRepository walkRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var walksDomainModel = await walkRepository.GetAllAsync();
        var walksDTO = mapper.Map<List<WalkDTO>>(walksDomainModel);
        return Ok(walksDTO);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
    {
        var walkDomainModel = mapper.Map<Walk>(addWalkRequestDTO);
        await walkRepository.CreateAsync(walkDomainModel);
        return Ok(mapper.Map<WalkDTO>(walkDomainModel));
    }
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var walkDomainModel = await walkRepository.GetByIdAsync(id);
        if (walkDomainModel == null)
        {
            return NotFound();
        }
        var walkDto = mapper.Map<WalkDTO>(walkDomainModel);
        return Ok(walkDto);
    }
    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDTO updateWalkRequestDTO)
    {
        var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDTO);
        walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
        if (walkDomainModel == null)
        {
            return NotFound();
        }
        return Ok(mapper.Map<WalkDTO>(walkDomainModel));
    }
    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var walkDomainModel = await walkRepository.DeleteAcync(id);
        if (walkDomainModel == null) return NotFound();
        var walkDto = mapper.Map<WalkDTO>(walkDomainModel);
        return Ok(walkDto);
    }
}