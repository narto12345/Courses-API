using AutoMapper;
using Courses_API.Dtos;
using Courses_API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Controllers
{

	[ApiController]
	[Route("/api/details")]
	public class DetailController : ControllerBase
	{
		private readonly ApplicationDbContext _contextDb;
		private readonly IMapper _mapper;
		public DetailController(ApplicationDbContext applicationDbContext, IMapper mapper)
		{
			_contextDb = applicationDbContext;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<List<DetailDto>> Get()
		{
			List<Detail> detailsFoud = await _contextDb.Details.ToListAsync();
			List<DetailDto> detailsDto = _mapper.Map<List<DetailDto>>(detailsFoud);
			return detailsDto;
		}

		[HttpGet("{id:int}", Name = "ObtenerDetalleDeUsuario")]
		public async Task<ActionResult> Get(int id)
		{
			Detail? detailFound = await _contextDb.Details.FirstOrDefaultAsync(x => x.Id == id);

			if (detailFound is null)
			{
				return NotFound();
			}

			DetailDto detailDto = _mapper.Map<DetailDto>(detailFound);

			return Ok(detailDto);
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromBody] DetailRequestDto detailRequestDto)
		{
			bool existUser = await _contextDb.Users.AnyAsync(user => user.Id  == detailRequestDto.UserIdFk);

			if (!existUser)
			{
				return BadRequest($"El usuario con id '{detailRequestDto.UserIdFk}' no existe");
			}

			bool existDetail = await _contextDb.Details.AnyAsync(detailFound => detailFound.UserIdFk == detailRequestDto.UserIdFk);

			if (existDetail)
			{
				return BadRequest($"El usuario con id '{detailRequestDto.UserIdFk}' ya tiene un detalle creado");
			}

			Detail detail = _mapper.Map<Detail>(detailRequestDto);

			_contextDb.Add(detail);
			await _contextDb.SaveChangesAsync();

			DetailDto detailDto = _mapper.Map<DetailDto>(detail);

			return CreatedAtRoute("ObtenerDetalleDeUsuario", new { id = detail.Id}, detailDto);
		}

		[HttpPatch("{id:int}")]
		public async Task<ActionResult> Patch(int id, JsonPatchDocument<DetailPatchDto> patchDocument)
		{
			if (patchDocument is null)
			{
				return BadRequest();
			}

			Detail? detailFound = await _contextDb.Details.FirstOrDefaultAsync(detailFound => detailFound.Id == id);

			if (detailFound == null)
			{
				return NotFound();
			}

			DetailPatchDto detailPatchDtoDb = _mapper.Map<DetailPatchDto>(detailFound);
			patchDocument.ApplyTo(detailPatchDtoDb, ModelState);
			bool isValid = TryValidateModel(detailPatchDtoDb);

			if (!isValid)
			{
				return ValidationProblem();
			}

			_mapper.Map(detailPatchDtoDb, detailFound);
			await _contextDb.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			int registersDeleted = await _contextDb.Details.Where(detail => detail.Id == id)
														   .ExecuteDeleteAsync();

			if (registersDeleted == 0)
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
