using System.Threading.Tasks;

namespace BaDemo.Orders.Models.Services
{
    public interface IEmailService
    {
        Task SendEmail(Order order);
    }
}
