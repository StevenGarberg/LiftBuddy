using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiftBuddy.Web.Clients
{
    public interface IClient<T>
    {
        Task<List<T>> GetAll();
        Task<T> Get(string id);
        Task<T> Create(object request);
        Task<T> Update(string id, object request);
        Task Delete(string id);
    }
}