﻿using BookStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Genres.Commands
{
    public class CreateGenreCommand : IRequest<Genre>
    {
        public string Name { get; set; }
    }
}
