namespace EmailConnector
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;

    /// <summary>
    /// Disposable Class used to Send Emails using SMTP Server.
    /// </summary>
    public class EmailService : IDisposable
    {
        private readonly SmtpClient client;
        private readonly string sender;

        public EmailService(string host, int port, string sender, string username, string password, bool enableSsl)
        {
            this.sender = sender;

            this.client = new SmtpClient(host, port)
            {
                UseDefaultCredentials = false,
                EnableSsl = enableSsl,
                Credentials = new NetworkCredential(username, password)
            };
        }

        /// <summary>
        /// Async method to Send Mail Message to one or many recipients.
        /// </summary>
        /// <param name="subject">subject of the email.</param>
        /// <param name="body">html body string of the email.</param>
        /// <param name="recipients">list of recipients email addresses</param>
        public void SendEmail(string subject, string body, params string[] recipients)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentException(nameof(subject), "value cannot be null or empty");
            }

            if (string.IsNullOrWhiteSpace(body))
            {
                throw new ArgumentException(nameof(body), "value cannot be null or empty");
            }

            if (recipients == null || recipients.Length == 0 || !recipients.Any(m => !string.IsNullOrWhiteSpace(m)))
            {
                throw new ArgumentException("Email should contain at least 1 valid recipient");
            }

            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(sender, "No Reply");
                mailMessage.Sender = mailMessage.From;
                mailMessage.Body = body;
                mailMessage.Subject = subject;

                mailMessage.IsBodyHtml = true;

                foreach (var recipient in recipients)
                {
                    if (!string.IsNullOrWhiteSpace(recipient))
                    {
                        mailMessage.To.Add(recipient);
                    }
                }

                client.SendMailAsync(mailMessage).Wait();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (client != null)
                    {
                        client.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
