using System;

namespace EmailConnector
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please enter email server host or IP:");
                var host = Console.ReadLine();

                Console.WriteLine("Please enter the port:");
                var port = int.Parse(Console.ReadLine());

                Console.WriteLine("Please enter the sender:");
                var sender = Console.ReadLine();

                Console.WriteLine("Please enter the username:");
                var username = Console.ReadLine();

                Console.WriteLine("Please enter the password:");
                var password = Console.ReadLine();

                Console.WriteLine("Please enter the enable SSL yes/no:");
                var enableSsl = Console.ReadLine().ToLower() == "yes";

                Console.WriteLine("Please enter the receiver's email:");
                var email = Console.ReadLine();

                var emailService = new EmailService(host, port, sender, username, password, enableSsl);

                emailService.SendEmail("Test subject", $"Test body at {DateTime.Now:yyyy-MM-dd-HH-mm-ss}", email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Please hit enter to exit...");
            Console.ReadLine();
        }
    }
}
