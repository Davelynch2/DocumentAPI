using DocumentAPI.Db;
using DocumentAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DocumentAPI.Models;

namespace DocumentAPI.Controllers
{
	[Route("api/users")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly FileUserDbContext _context;
		private readonly IMapper _mapper;

		public UserController(FileUserDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] UserForCreationDto user)
		{
			User userEntity = _mapper.Map<User>(user);

			_context.Users.Add(userEntity);

			await _context.SaveChangesAsync();

			UserDto userToReturn = _mapper.Map<UserDto>(userEntity);

			return CreatedAtAction(nameof(GetUser), new { id = userToReturn.Id }, userToReturn);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUser(Guid id)
		{
			if (_context.Users == null)
			{
				return NotFound();
			}

			User user = _context.Users.FirstOrDefault(x => x.Id == id);

			if (user == null)
			{
				return NotFound();
			}

			UserDto userToReturn = _mapper.Map<UserDto>(user);

			return Ok(userToReturn);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			IEnumerable<User> users = _context.Users.OrderBy(u => u.LastName).ToList();

			IEnumerable<UserDto> usersToReturn = _mapper.Map<IEnumerable<UserDto>>(users);

			return Ok(usersToReturn);
		}

	}
}
