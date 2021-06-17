using System;
using System.Collections.Generic;

namespace Swift.Balancer.Extensions
{
    public static class CollectionExtensions
    {
        public static T Random<T>(this List<T> collection)
            => collection[new Random().Next(0, collection.Count - 1)];
    }
}