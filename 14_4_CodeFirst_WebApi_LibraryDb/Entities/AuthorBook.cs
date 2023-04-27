namespace _14_4_CodeFirst_WebApi_LibraryDb.Entities
{
    public class AuthorBook:BaseEntity
    {
        public int AuthorID { get; set; }
        public virtual Author Author { get; set; }
        public int BookID { get; set; }
        public virtual Book Book { get; set; }
    }
}
