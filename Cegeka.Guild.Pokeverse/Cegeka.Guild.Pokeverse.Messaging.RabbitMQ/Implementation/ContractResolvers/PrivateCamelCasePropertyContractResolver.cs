﻿using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cegeka.Guild.Pokeverse.RabbitMQ.ContractResolvers
{
    public class PrivateCamelCasePropertyContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);

            if (prop.Writable)
            {
                return prop;
            }

            var property = member as PropertyInfo;
            if (property == null)
            {
                return prop;
            }

            var hasPrivateSetter = property.GetSetMethod(true) != null;
            prop.Writable = hasPrivateSetter;

            return prop;
        }
    }
}