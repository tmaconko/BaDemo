using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace BaDemo.Items.Models.Repositories
{
    public class ItemsRepository : IItemsRepository, IDisposable
    {
        private IList<int> AvailableIds { get; set; }
        private IList<Item> Items { get; set; }

        private const string ItemsFilePath = "Items.xml";
        private const string ItemsIdsFilePath = "ItemsIds.xml";

        public ItemsRepository()
        {
            using (var fileStream = new FileStream(ItemsFilePath, FileMode.OpenOrCreate))
            {
                var serializer = new DataContractJsonSerializer(typeof (List<Item>));
                Items = (IList<Item>) serializer.ReadObject(fileStream);
            }

            using (var fileStream = new FileStream(ItemsIdsFilePath, FileMode.OpenOrCreate))
            {
                var serializer = new DataContractJsonSerializer(typeof (List<int>));
                AvailableIds = (IList<int>) serializer.ReadObject(fileStream);
            }
        }

        public Task<IEnumerable<Item>> ListAsync()
        {
            return Task.Run(() => Items.AsEnumerable());
        }

        public Task<Item> GetAsync(int id)
        {
            return Task.Run(() => Items.FirstOrDefault(p => p.Id == id));
        }

        public Task<int> AddAsync(Item item)
        {
            return Task.Run(() =>
            {
                int id = AvailableIds.First();
                item.Id = id;

                AvailableIds.Remove(id);
                Items.Add(item);

                return id;
            });
        }

        public Task UpdateAsync(int id, Item item)
        {
            return Task.Run(() =>
            {
                var oldItem = Items.FirstOrDefault(p => p.Id == id);
                if (oldItem == null)
                    return;

                oldItem.Description = item.Description;
                oldItem.Title = item.Title;
                oldItem.Code = item.Code;
            });
        }

        public Task DeleteAsync(int id)
        {
            return Task.Run(() =>
            {
                var item = Items.FirstOrDefault(p => p.Id == id);
                if (item == null)
                    return;

                Items.Remove(item);
                AvailableIds.Add(item.Id);
            });
        }

        public void Dispose()
        {
            using (var fileStream = new FileStream(ItemsFilePath, FileMode.Create))
            {
                var serializer = new DataContractJsonSerializer(typeof (List<Item>));
                serializer.WriteObject(fileStream, Items);
            }

            using (var fileStream = new FileStream(ItemsIdsFilePath, FileMode.Create))
            {
                var serializer = new DataContractJsonSerializer(typeof (List<int>));
                serializer.WriteObject(fileStream, AvailableIds);
            }
        }
    }
}
