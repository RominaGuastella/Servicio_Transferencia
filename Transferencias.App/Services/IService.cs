namespace Transferencias.App.Services
{
    public interface IService<TRequest, TResponse> 
        where TRequest : class
        where TResponse : class
    {
        Task<TResponse> Create(TRequest request);
        Task<TResponse> Delete(int id);
        Task<IEnumerable<TResponse>> GetAll();
        Task<TResponse> GetById(int id);
        Task<TResponse> Update(int id, TRequest request);
        
    }
}
