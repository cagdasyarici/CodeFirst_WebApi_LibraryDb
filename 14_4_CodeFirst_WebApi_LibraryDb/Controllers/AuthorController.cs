using _14_4_CodeFirst_WebApi_LibraryDb.DTOs.AuthorDTOs;
using _14_4_CodeFirst_WebApi_LibraryDb.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _14_4_CodeFirst_WebApi_LibraryDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private LibraryDBContext db;
        public AuthorController(LibraryDBContext _db) 
        {
            db=_db;
        }
        [HttpGet]
        [Route("GetAllAuthors")]
        public ActionResult<IEnumerable<AuthorDTO>> GetAll()
        {
            List<AuthorDTO> authors = db.Authors.Select(x => new AuthorDTO
            {
                Id = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
            }).ToList();
            if (authors == null)
            {
                return NotFound();
            }
            return authors;
        }
        [HttpGet]
        [Route("AuthorDetail")]
        public ActionResult<AuthorDTO> GetAuthor(int id)
        {
            Author author = db.Authors.FirstOrDefault(x => x.ID.Equals(id));
            if (author == null) return NotFound();
            AuthorDTO authorDTO = new AuthorDTO()
            {
                Id = author.ID,
                FirstName= author.FirstName,
                LastName = author.LastName
            };
            return Ok(authorDTO);
        }
        [HttpPost]
        [Route("CreateAuthor")]
        public ActionResult<AuthorCreateDTO> CreateAuthor(AuthorCreateDTO authorCreateDTO)
        {
            try
            {
                Author author = new Author()
                {
                    FirstName = authorCreateDTO.FirstName,
                    LastName = authorCreateDTO.LastName
                };
                db.Authors.Add(author);
                db.SaveChanges();
                return Ok(authorCreateDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex); 
            }
        }
        [HttpPut]
        [Route("UpdateAuthor")]
        public ActionResult<AuthorDTO> UpdateAuthor(AuthorDTO authorDTO)
        {
            Author author = db.Authors.Find(authorDTO.Id);
            if (author == null) return NotFound();
            try
            {
                author.FirstName = authorDTO.FirstName;
                author.LastName = authorDTO.LastName;
                db.Entry<Author>(author).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(authorDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpDelete]
        [Route("DeleteAuthor")]
        public IActionResult Delete(int id) 
        {
            Author author = db.Authors.Find(id);
            if(author == null) return NotFound();
            try
            {
                db.Authors.Remove(author);
                db.SaveChanges();
                return Ok(author);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
            
        }
    }
}
