using System;
using System.IO;
using Newtonsoft.Json;

namespace Cegeka.Guild.Pokeverse.Persistence.EventStoreDb
{
    internal static class JsonSerialization
    {
        public static object Deserialize(byte[] buffer, Type type)
        {
            using (var stream = new MemoryStream(buffer))
            {
                using (var reader = new StreamReader(stream))
                {
                    return JsonSerializer.CreateDefault().Deserialize(reader, type);
                }
            }
        }

        public static T Deserialize<T>(byte[] buffer)
        {
            using (var stream = new MemoryStream(buffer))
            {
                using (var reader = new StreamReader(stream))
                {
                    using (var jsonReader = new JsonTextReader(reader))
                    {
                        return JsonSerializer.CreateDefault().Deserialize<T>(jsonReader);
                    }
                }
            }
        }
    }
}