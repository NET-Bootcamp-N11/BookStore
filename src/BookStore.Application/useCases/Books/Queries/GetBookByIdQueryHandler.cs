namespace BookStore.Application.useCases.Books.Queries
{
    public class GetBookByIdQueryHandler
    {
        public long Id { get; set; }

        private readonly IAppDbContext _appDb;

        public GetBookByIdQuery(IAppDbContext appDb)
        {
            _appDb = appDb;
        }
        
        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _appDb.Books.FirstOrDefaultAsync(x=> x.Id == request.Id);
            return user;
        }
    }
}
