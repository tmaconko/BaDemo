using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaDemo.Items.Models.Repositories
{
    public interface IItemsRepository
    {
        Task<IEnumerable<Item>> ListAsync();
        Task<Item> GetAsync(int id);
        Task<int> AddAsync(Item item);
        Task UpdateAsync(int id, Item item);
        Task DeleteAsync(int id);
    }
}
