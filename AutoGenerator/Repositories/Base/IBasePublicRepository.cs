using AutoGenerator.Helper;
using System.Linq.Expressions;

namespace AutoGenerator.Repositories.Base
{
    public interface IBasePublicRepository<TRequest, TResponse>
          where TRequest : class
          where TResponse : class
    {
        Task<IEnumerable<TResponse>> GetAllAsync();
        Task<TResponse?> GetByIdAsync(string id);
        Task<TResponse?> FindAsync(Expression<Func<TResponse, bool>> predicate);
        IQueryable<TResponse> GetQueryable();

        Task<TResponse> CreateAsync(TRequest entity);
        Task<IEnumerable<TResponse>> CreateRangeAsync(IEnumerable<TRequest> entities);

        Task<TResponse> UpdateAsync(TRequest entity);

        Task DeleteAsync(string id);
        Task DeleteRangeAsync(Expression<Func<TResponse, bool>> predicate);

        Task<bool> ExistsAsync(Expression<Func<TResponse, bool>> predicate);
        Task<int> CountAsync();

        Task<TResponse?> FindAsync(params object[] id);
        Task<bool> ExistsAsync(object value, string name = "Id");

        Task<PagedResponse<TResponse>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10);
        Task<TResponse?> GetByIdAsync(object id);
        Task DeleteAllAsync();
        Task DeleteAsync(TRequest entity);
        Task DeleteAsync(object value, string key = "Id");
        Task DeleteRange(List<TRequest> entities);

        Task<PagedResponse<TResponse>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null);
        Task<TResponse?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null);
    }



}
