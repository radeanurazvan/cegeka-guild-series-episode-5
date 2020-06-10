using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static async Task<Maybe<T>> ToMaybe<T>(this Task<Result<T>> resultTask)
        {
            return (await resultTask).ToMaybe();
        }

        public static Maybe<T> ToMaybe<T>(this Result<T> result)
        {
            if (result.IsFailure)
            {
                return Maybe<T>.None;
            }

            return result.Value;
        }

        public static string GetFriendlyName(this Type type)
        {
            var friendlyName = type.Name;
            if (!type.IsGenericType)
            {
                return friendlyName;
            }

            var iBacktick = friendlyName.IndexOf('`');
            if (iBacktick > 0)
            {
                friendlyName = friendlyName.Remove(iBacktick);
            }
            friendlyName += "<";
            var typeParameters = type.GetGenericArguments();
            for (var i = 0; i < typeParameters.Length; ++i)
            {
                var typeParamName = GetFriendlyName(typeParameters[i]);
                friendlyName += (i == 0 ? typeParamName : "," + typeParamName);
            }
            friendlyName += ">";

            return friendlyName;
        }

        public static string GetFriendlyFullName(this Type type)
        {
            var friendlyName = type.FullName;
            if (!type.IsGenericType)
            {
                return friendlyName;
            }

            var iBacktick = friendlyName.IndexOf('`');
            if (iBacktick > 0)
            {
                friendlyName = friendlyName.Remove(iBacktick);
            }
            friendlyName += "<";
            var typeParameters = type.GetGenericArguments();
            for (var i = 0; i < typeParameters.Length; ++i)
            {
                var typeParamName = GetFriendlyName(typeParameters[i]);
                friendlyName += (i == 0 ? typeParamName : "," + typeParamName);
            }
            friendlyName += ">";

            return friendlyName;
        }

        public static string ToUtf8String(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}