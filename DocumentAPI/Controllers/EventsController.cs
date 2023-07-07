using AutoMapper;
using DocumentAPI.Db;
using DocumentAPI.DTO;
using DocumentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocumentAPI.Controllers
{
	[Route("api/events")]
	public class EventsController : ControllerBase
	{
		private readonly FileUserDbContext _context;
		private readonly IMapper _mapper;

		public EventsController(FileUserDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> AddEvent([FromBody] EventForCreationDto eventcreationDto)
		{
			User user = _context.Users.FirstOrDefault(u => u.Id == eventcreationDto.DeclaredById);

			if (user == null)
			{
				return BadRequest($"The user with Id {eventcreationDto.DeclaredById} is not found");
			}

			Event eventEntity = _mapper.Map<Event>(eventcreationDto);

			eventEntity.DeclaredBy = user;

			_context.Events.Add(eventEntity);
			_context.SaveChanges();

			EventDto eventDto = _mapper.Map<EventDto>(eventEntity);


			return CreatedAtAction(nameof(GetEvent), new { id = eventDto.Id }, eventDto);
		}

		[HttpGet("{id:int}", Name = "EventById")]
		public async Task<IActionResult> GetEvent(int id)
		{
			var eventEntity = _context.Events.Include(x => x.DeclaredBy).FirstOrDefault(u => u.Id == id);

			if (eventEntity == null)
			{
				return NotFound();
			}

			EventDto eventDto = _mapper.Map<EventDto>(eventEntity);

			return Ok(eventDto);
		}

		[HttpGet(Name = "GetEvents")]
		public async Task<IActionResult> GetAllEvents()
		{
			var events = await _context.Events.Include(x => x.DeclaredBy).Include(x => x.Documents).ToListAsync();

			var eventsDto = _mapper.Map<IEnumerable<EventDto>>(events);

			return Ok(eventsDto);
		}

		[HttpPut]
		public async Task<IActionResult> AddDocumentToEvent(int eventId, int documentId)
		{
			var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Id == eventId);

			if (eventEntity == null)
			{
				return NotFound($"The event with this id {eventId} doesn't exist");
			}

			var documentEntity = await _context.Files.FirstOrDefaultAsync(e => e.Id == documentId);

			if (documentEntity == null)
			{
				return NotFound($"The document with this id {documentId} doesn't exist");
			}

			if(eventEntity.Documents == null)
			{
				eventEntity.Documents = new List<FileModel>();
			}

			eventEntity.Documents.Add(documentEntity);

			await _context.SaveChangesAsync();

			return NoContent();
		}

		[Route("{id}")]
		[HttpDelete]
		public async Task<IActionResult> DeleteEvent(int id)
		{
			var eventToBeDeleted = await _context.Events.SingleOrDefaultAsync(x => x.Id == id);

			if (eventToBeDeleted == null)
			{
				return NotFound($"The event with id {id} doesn't exists");
			}

			if (!string.IsNullOrEmpty(eventToBeDeleted.Description))
			{
				return Forbid($"This event cannot be deleted");
			}

			_context.Events.Remove(eventToBeDeleted);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
