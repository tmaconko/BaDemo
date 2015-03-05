using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BaDemo.Orders.Models
{
    [DataContract]
    public class Order
    {
        public Order()
        {
            ItemOrders = new List<ItemOrder>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public DateTime Timestamp { get; set; }

        [DataMember]
        public IEnumerable<ItemOrder> ItemOrders { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}
