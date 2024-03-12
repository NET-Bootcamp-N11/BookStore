using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Books.Commands
{

    public class CreateBookHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CreateBookHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }


        async Task<Book> IRequestHandler<CreateBookCommand, Book>.Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request);
            var res = await _appDbContext.Books.AddAsync(book);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }
    }
}
