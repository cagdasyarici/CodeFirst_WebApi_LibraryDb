namespace _14_4_CodeFirst_WebApi_LibraryDb.Entities
{
    public class BookType:BaseEntity
    {
        public int BookID { get; set; }
        public virtual Book Book { get; set; }
        public int TypeID { get; set; }
        public virtual Type Type { get; set; }
    }
}
