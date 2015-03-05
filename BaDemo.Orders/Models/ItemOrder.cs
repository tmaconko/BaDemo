using System.Runtime.Serialization;
using BaDemo.Items.Models;

namespace BaDemo.Orders.Models
{
    [DataContract]
    public class ItemOrder
    {
        [DataMember]
        public Item Item { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}