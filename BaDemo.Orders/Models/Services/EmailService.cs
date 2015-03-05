using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Logging;

namespace BaDemo.Orders.Models.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILoggerFacade _logger;
        private const string EmailsFilePath = "Emails.xml";
        
        public EmailService(ILoggerFacade logger)
        {
            _logger = logger;
        }

        public Task SendEmail(Order order)
        {
            return Task.Run(() =>
            {
                _logger.Log(string.Format("Sending email to {0} for order {1}.", order.Email, order.Id), Category.Info, Priority.Medium);
                
                const string from = "info@bademo.lt";
                string to = order.Email;

                string subject = string.Format("Order {0}: {1}", order.Id, order.Title);

                var contentBuilder = new StringBuilder();
                contentBuilder.AppendLine("Dear User,");
                contentBuilder.AppendLine("Thank you for purchasing in our e-shop.");
                contentBuilder.AppendLine(string.Format("Your order {0}: {1} ({2}) will be completed soon. There are items that were ordered:", order.Id, order.Title, order.Timestamp));
                foreach (var itemOrder in order.ItemOrders)
                {
                    contentBuilder.AppendLine(string.Format("{0}: {1} items", itemOrder.Item.Title, itemOrder.Count));
                }
                contentBuilder.AppendLine(string.Format("We hope you enjoyed the stuff."));
                contentBuilder.AppendLine(string.Format("Best regards,"));
                contentBuilder.AppendLine(string.Format("BaDemo Team"));

                var email = new Email
                {
                    From = from,
                    To = to,
                    Subject = subject,
                    Content = contentBuilder.ToString(),
                };

                using (var writer = new StreamWriter(EmailsFilePath, true))
                {
                    var stringStream = new MemoryStream();
                    var serializer = new DataContractJsonSerializer(typeof(Email));
                    serializer.WriteObject(stringStream, email);

                    writer.WriteLine(Encoding.Default.GetString(stringStream.ToArray()));
                }

                _logger.Log(string.Format("Email to {0} for order {1} sent successfully.", order.Email, order.Id), Category.Info, Priority.Medium);
            });
        }

        [DataContract]
        private class Email
        {
            [DataMember]
            public string From { get; set; }

            [DataMember]
            public string To { get; set; }

            [DataMember]
            public string Subject { get; set; }

            [DataMember]
            public string Content { get; set; }
        }
    }
}