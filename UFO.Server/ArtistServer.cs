namespace UFO.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class ArtistServer : DatabaseObjectServer<Artist>,
                                IBaseServer<Artist>
    {
        internal ArtistServer()
        {
        }

        public void SendNotificationEmail(IEnumerable<Artist> artists)
        {
            foreach (var artist in artists ?? new List<Artist>())
            {
                if (artist.Email == null)
                {
                    continue;
                }

                Task.Run(
                    () =>
                        {
                            var performances = new Server().PerformanceServer.GetByArtist(artist)?.ToList()
                                               ?? new List<Performance>();
                            if (performances.Count == 0)
                            {
                                return;
                            }

                            // create mail
                            var message = "Hello " + artist.Name + Environment.NewLine;
                            message += $"Here are upcoming performances:{Environment.NewLine}";

                            foreach (var performance in performances)
                            {
                                message += $"{performance.DateTime}: {performance.Venue.Name}{Environment.NewLine}";
                            }

                            // mail
                            var mail = new MailMessage("s1310307089@students.fh-hagenberg.at", artist.Email);
                            var client = new SmtpClient
                                {
                                    Port = 465,
                                    DeliveryMethod = SmtpDeliveryMethod.Network,
                                    UseDefaultCredentials = false,
                                    Host = "localhost"
                                };
                            mail.Subject = "Your Performances";
                            mail.Body = message;
                            client.Send(mail);
                        });
            }
        }

        protected IBaseDAO<Artist> GetDAO() => DALFactory.CreateArtistDAO(Server.GetDatabase());

        protected override IBaseDAO<Artist> GetDatabaseObjectDAO() => GetDAO();
    }
}
