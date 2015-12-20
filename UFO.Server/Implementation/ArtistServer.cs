﻿namespace UFO.Server.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using UFO.DAL.Common;
    using UFO.Domain;
    using UFO.Server.Interfaces;

    public class ArtistServer : DatabaseObjectServer<Artist>,
                                IArtistServer,
                                IArtistServerAsync
    {
        internal ArtistServer()
        {
        }

        #region IArtistServer Members

        public async void SendNotificationEmail(IEnumerable<Artist> artists)
        {
            artists = artists ?? new List<Artist>();

            foreach (var artist in artists.Where(artist => artist.Email != null))
            {
                var performances = (await Server.PerformanceServer.GetUpcomingByArtistAsync(artist))?.ToList()
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
            }
        }

        #endregion

        #region IArtistServerAsync Members

        public Task SendNotificationEmailAsync(IEnumerable<Artist> artists)
        {
            return Task.Run(() => SendNotificationEmail(artists));
        }

        #endregion

        protected IBaseDAO<Artist> GetDAO() => DALFactory.CreateArtistDAO(Server.GetDatabase());

        protected override IBaseDAO<Artist> GetDatabaseObjectDAO() => GetDAO();
    }
}
