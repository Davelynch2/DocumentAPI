using DocumentAPI.Db;
using DocumentAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace DocumentAPI.Controllers
{
	[Route("api/files")]
	[ApiController]
	public class FileController : ControllerBase
	{
		 private readonly FileUserDbContext _context;
		public FileController(FileUserDbContext contex)
		{
			_context = contex;
		}

		[HttpPost]
		public IActionResult UploadFile(IFormFile postedFile)
		{
			if (postedFile == null || postedFile.Length == 0)
			{
				return BadRequest("No file was selected.");
			}
				
			byte[] fileBytes;

			using (MemoryStream ms = new MemoryStream())
			{
				postedFile.CopyTo(ms);
				fileBytes = ms.ToArray();
			}

			FileModel file = new FileModel
			{
				ContentType = postedFile.ContentType,
				Name = Path.GetFileName(postedFile.FileName),
				Data = fileBytes
			};

			_context.Files.Add(file);
			_context.SaveChanges();

			return Ok("File uploaded successfully");
		}

		[Route("{id}")]
		[HttpGet]
		public IActionResult DownloadFile(int id)
		{
			var file = _context.Files.SingleOrDefault(f => f.Id == id);

			if (file == null)
			{
				return NotFound();
			}

			return this.File(file.Data, file.ContentType, file.Name);
		}
	}
}
