using BookStoreApi.Interface;

namespace BookStoreApi.Services
{
    public class EmailService : IMessageService
    {
        public EmailService()
        {
            Console.WriteLine("EmailService Created");
        }
        public void Send()
        {
            Console.WriteLine("Email Sent");
        }

    }
}
