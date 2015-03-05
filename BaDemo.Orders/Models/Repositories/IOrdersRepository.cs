using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaDemo.Orders.Models.Repositories
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Order>> ListAsync();
        Task<Order> GetAsync(int id);
        Task<int> AddAsync(Order order);
    }
}
