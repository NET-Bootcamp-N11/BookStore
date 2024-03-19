﻿using BookStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Authors.Queries
{
    public class GetAuthorByNameQuery : IRequest<Author>
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
