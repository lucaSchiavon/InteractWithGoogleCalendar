using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteOnGoogleCalendar
{
    // Interfaccia
    public interface ICalendarService
    {
        void CreateEvent(CalendarEvent calendarEvent);
    }

    // Implementazione
    public class CalendarService : ICalendarService
    {
        private readonly CalendarService _calendarService;

        public CalendarService()
        {
            _calendarService = InitializeService();
        }

        private CalendarService InitializeService()
        {
            UserCredential credential;
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { CalendarService.Scope.Calendar },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            return new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Event Scheduler"
            });
        }

        public void CreateEvent(CalendarEvent calendarEvent)
        {
            var newEvent = new Event()
            {
                Summary = calendarEvent.Summary,
                Location = calendarEvent.Location,
                Description = calendarEvent.Description,
                Start = new EventDateTime() { DateTime = calendarEvent.Start, TimeZone = "Europe/Rome" },
                End = new EventDateTime() { DateTime = calendarEvent.End, TimeZone = "Europe/Rome" },
            };
            _calendarService.Events.Insert(newEvent, "primary").Execute();
        }
    }

}
