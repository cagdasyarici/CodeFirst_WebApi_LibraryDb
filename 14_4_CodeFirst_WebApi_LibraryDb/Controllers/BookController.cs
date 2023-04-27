using _14_4_CodeFirst_WebApi_LibraryDb.DTOs.BookDTOs;
using _14_4_CodeFirst_WebApi_LibraryDb.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _14_4_CodeFirst_WebApi_LibraryDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryDBContext db;
        public BookController(LibraryDBContext _db)
        {
            db = _db;
        }
        [HttpGet]
        [Route("GetAllBooks")]
        public ActionResult<IEnumerable<BookDTO>> GetAllBooks() 
        {
            List<BookDTO> bookDTOs = db.Books.Select(x=> new BookDTO()
            {
                Id=x.ID, Name=x.Name
            }).ToList();
            return Ok(bookDTOs);

        }
        [HttpGet]
        [Route("BookDetail")]
        public ActionResult<BookDTO> GetBook(int id) 
        {
            Book book = db.Books.FirstOrDefault(x=>x.ID==id);
            if (book == null) return NotFound();
            BookDTO bookDTO = new BookDTO()
            {
                Id = id,
                Name = book.Name,
            };
            AuthorBook authorBook = db.AuthorBooks.Where(x=>x.BookID==id).FirstOrDefault();
            bookDTO.AuthorName = db.Authors.FirstOrDefault(x => x.ID.Equals(authorBook.AuthorID)).FirstName;
            return Ok(bookDTO);
        }
        [HttpPost]
        [Route("CreateBook")]
        public ActionResult<BookCreateDTO> CreateBook(BookCreateDTO bookCreateDTO)
        {
            if (bookCreateDTO == null) return BadRequest();
            try
            {
                Author author = db.Authors.FirstOrDefault(x=>x.FirstName==bookCreateDTO.AuthorName);

                Book book = new Book()
                {
                    Name = bookCreateDTO.Name,
                };
                ExistOrCreateBookType(bookCreateDTO,book);
                string msg = ExistAuthor(author,book);
                if (msg != null) return NotFound();
                db.Books.Add(book);
                db.SaveChanges();
                return CreatedAtAction("GetBook",new {id=book.ID},book);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string ExistAuthor(Author? author, Book book)
        {
            if (author == null) return "Önce Yazar Bilgilerini Girin";
            try
            {
                AuthorBook authorBook = new AuthorBook()
                {
                    Book = book,
                    Author = author
                };
                author.AuthorBooks.Add(authorBook);
                book.AuthorBooks.Add(authorBook);
                db.AuthorBooks.Add(authorBook);
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ExistOrCreateBookType(BookCreateDTO bookCreateDTO, Book book)
        {
            List<Entities.Type> types = db.Types.Where(x => bookCreateDTO.Types.Contains(x.Name)).ToList();
            List<string> typeName = db.Types.Select(x => x.Name).ToList();
            List<string> names = bookCreateDTO.Types.Where(x =>! typeName.Contains(x)).ToList();
            foreach(var item in names)
            {
                Entities.Type type = new Entities.Type()
                {
                    Name = item
                };
                types.Add(type);
                db.Types.Add(type);
            }
            foreach(var type in types)
            {
                BookType bookType = new BookType()
                {
                    Book = book,
                    Type = type
                };
                book.BookTypes.Add(bookType);
                type.BookTypes.Add(bookType);
                db.BookTypes.Add(bookType);
            }
        }

        [HttpPut]
        [Route("UpdateBook")]
        public ActionResult<BookDTO> UpdateBook(BookDTO bookDTO)
        {
            Book book=db.Books.FirstOrDefault(x=>x.ID.Equals(bookDTO.Id));
            if (book == null) return NotFound(bookDTO);
            try
            {
                book.Name = bookDTO.Name;
                db.SaveChanges();
                return Ok(book);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteBook")]
        public IActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            
            if (book == null) return NotFound();
            try
            {
                AuthorBook authorBook = db.AuthorBooks.FirstOrDefault(x => x.BookID.Equals(book.ID));
                List<BookType> bookType = db.BookTypes.Where(x=>x.BookID==book.ID).ToList();
                db.AuthorBooks.Remove(authorBook);
                foreach(var item in bookType)
                {
                    db.BookTypes.Remove(item);
                }
                db.Books.Remove(book);
                db.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
