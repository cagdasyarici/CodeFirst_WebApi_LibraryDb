namespace _14_4_CodeFirst_WebApi_LibraryDb.Entities
{
    public class Author:BaseEntity
    {
        public Author()
        {
            AuthorBooks = new HashSet<AuthorBook>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
