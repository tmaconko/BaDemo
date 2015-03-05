using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Logging;

namespace BaDemo.Orders.Models.Repositories
{
    public class OrdersRepository : IOrdersRepository, IDisposable
    {
        private readonly ILoggerFacade _logger;

        private IList<int> AvailableIds { get; set; }
        private IList<Order> Orders { get; set; }

        private const string OrdersFilePath = "Orders.xml";
        private const string OrdersIdsFilePath = "OrdersIds.xml";

        public OrdersRepository(ILoggerFacade logger)
        {
            _logger = logger;

            using (var fileStream = new FileStream(OrdersFilePath, FileMode.OpenOrCreate))
            {
                var serializer = new DataContractJsonSerializer(typeof (List<Order>));
                Orders = (IList<Order>) serializer.ReadObject(fileStream);
            }

            using (var fileStream = new FileStream(OrdersIdsFilePath, FileMode.OpenOrCreate))
            {
                var serializer = new DataContractJsonSerializer(typeof (List<int>));
                AvailableIds = (IList<int>) serializer.ReadObject(fileStream);
            }
        }

        public Task<IEnumerable<Order>> ListAsync()
        {
            return Task.Run(() =>
            {
                _logger.Log(string.Format("Accessing the {0} table for list of items.", "Order"), Category.Info, Priority.Medium);
                
                var items = Orders.AsEnumerable();
                
                _logger.Log(string.Format("Database returned {0} length list of {1} items.", Orders.Count, "Order"), Category.Info, Priority.Medium);

                return items;
            });
        }

        public Task<Order> GetAsync(int id)
        {
            return Task.Run(() =>
            {
                _logger.Log(string.Format("Accessing the {0} table for item with Id={1}.", "Order", id), Category.Info, Priority.Medium);

                var item = Orders.FirstOrDefault(p => p.Id == id);
                
                _logger.Log(string.Format("Database returned {0} {1} item.", item, "Order"), Category.Info, Priority.Medium);

                return item;
            });
        }

        public Task<int> AddAsync(Order order)
        {
            return Task.Run(() =>
            {
                _logger.Log(string.Format("Accessing the {0} table to add item", "Order"), Category.Info, Priority.Medium);

                int id = AvailableIds.FirstOrDefault();
                order.Id = id;

                AvailableIds.Remove(id);
                Orders.Add(order);

                _logger.Log(string.Format("Database returned {0} id for {1} item.", id, "Order"), Category.Info, Priority.Medium);

                return id;
            });
        }

        public void Dispose()
        {
            using (var fileStream = new FileStream(OrdersFilePath, FileMode.Create))
            {
                var serializer = new DataContractJsonSerializer(typeof (List<Order>));
                serializer.WriteObject(fileStream, Orders);
            }

            using (var fileStream = new FileStream(OrdersIdsFilePath, FileMode.Create))
            {
                var serializer = new DataContractJsonSerializer(typeof (List<int>));
                serializer.WriteObject(fileStream, AvailableIds);
            }
        }
    }
}
