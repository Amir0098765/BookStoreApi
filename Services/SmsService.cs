using BookStoreApi.Interface;

namespace BookStoreApi.Services
{
    public class SmsService : IMessageService
    {
        public SmsService()
        {
            Console.WriteLine("Sms Service Created");
        }
        public void Send()
        {
            Console.WriteLine("Sms Sent");
        }
    }
}
