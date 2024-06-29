namespace BookService.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string BookIsbn { get; set; } = string.Empty;
        public DateOnly BookPublishedDate { get; set; } = DateOnly.MaxValue;
        public int BookCopiesAvailable { get; set; } = 0;

        // Database metadata. Should this property be in the Model?
        public DateTime BookCreatedStamp { get; set; } = DateTime.Now;

        // Database metadata. Should this property be in the Model?
        public DateTime? BookUpdatedStamp { get; set; }


    }
}
