using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Common
{
    public static class Extensions
    {
        public static Maybe<T> FirstOrNothing<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            try
            {
                return collection.First(predicate);
            }
            catch
            {
                return Maybe<T>.None;
            }
        }
    }
}