using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using QueryExecutR.Wrappers;

namespace QueryExecutR
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly QueryHandlerFactory _factory;
        private static readonly ConcurrentDictionary<Type, QueryHandlerBase> QueryHandlers = new ConcurrentDictionary<Type, QueryHandlerBase>();

        public QueryExecutor(QueryHandlerFactory factory)
        {
            _factory = factory;
        }

        public Task<TResult> Execute<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            var queryType = query.GetType();
            var responseType = typeof(TResult);

            var handler = (QueryHandlerWrapper<TResult>) QueryHandlers.GetOrAdd(queryType,
                t => CreateQueryHandlerWrapper(t, responseType));

            return handler.Execute(query, cancellationToken, _factory);
        }

        private QueryHandlerBase CreateQueryHandlerWrapper(Type queryType, Type responseType)
        {
            var wrapperInstance = (QueryHandlerBase) Activator.CreateInstance(typeof(QueryHandlerWrapperImpl<,>).MakeGenericType(queryType, responseType))!;
            return wrapperInstance ?? throw new InvalidOperationException($"Could not create wrapper type for {queryType}");
        }
    }
}