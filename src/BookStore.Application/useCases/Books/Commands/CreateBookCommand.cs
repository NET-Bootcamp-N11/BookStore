namespace BookStore.Application.useCases.Books.Commands
{
    public class CreateBookCommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
    }
}
