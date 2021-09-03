using System.Threading;
using System.Threading.Tasks;

namespace QueryExecutR
{
    public interface IQueryExecutor
    {
        Task<TResult> Execute<TResult>(IQuery<TResult> request, CancellationToken cancellationToken = default);
    }
}