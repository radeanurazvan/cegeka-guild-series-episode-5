﻿using System;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Infrastructure
{
    internal sealed class RepositoryMediator : IRepositoryMediator
    {
        private readonly IServiceProvider provider;

        public RepositoryMediator(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IReadRepository<T> Read<T>() where T : AggregateRoot => provider.GetService<IReadRepository<T>>();

        public IWriteRepository<T> Write<T>() where T : AggregateRoot => provider.GetService<IWriteRepository<T>>();
    }
}