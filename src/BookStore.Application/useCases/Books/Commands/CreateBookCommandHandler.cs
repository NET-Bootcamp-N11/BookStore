using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MapsterMapper;
using MediatR;

namespace BookStore.Application.useCases.Books.Commands
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public CreateBookCommandHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request);
            var res = await _appDbContext.Books.AddAsync(book);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }
    }
}
