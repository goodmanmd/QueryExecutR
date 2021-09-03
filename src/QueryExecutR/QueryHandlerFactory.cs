using System;
using System.Collections.Generic;

namespace QueryExecutR
{
    public delegate object QueryHandlerFactory(Type serviceType);

    public static class QueryHandlerFactoryExtensions
    {
        public static T GetInstance<T>(this QueryHandlerFactory factory)
            => (T)factory(typeof(T));

        public static IEnumerable<T> GetInstances<T>(this QueryHandlerFactory factory)
            => (IEnumerable<T>)factory(typeof(IEnumerable<T>));
    }
}