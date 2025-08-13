using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace WriteOnGoogleCalendar
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region "accesso con app, fallace, necessita di autenticazione quando scade"
            //// Percorso al file delle credenziali scaricato da Google Cloud
            //string credPath = "credentials.json";

            //UserCredential credential;

            //using (var stream = new FileStream(credPath, FileMode.Open, FileAccess.Read))
            //{
            //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            //        GoogleClientSecrets.Load(stream).Secrets,
            //        new[] { CalendarService.Scope.Calendar },
            //        "user",
            //        CancellationToken.None,
            //        new FileDataStore("token.json", true)).Result;
            //}

            //// Inizializza il servizio Calendar
            //var service = new CalendarService(new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = credential,
            //    ApplicationName = "Console Google Calendar Example",
            //});

            //// Crea un nuovo evento
            //var newEvent = new Event()
            //{
            //    Summary = "Evento di prova",
            //    Location = "Online",
            //    Description = "Evento creato da Console App .NET Core",
            //    Start = new EventDateTime()
            //    {
            //        DateTime = DateTime.Now.AddMinutes(1),
            //        TimeZone = "Europe/Rome",
            //    },
            //    End = new EventDateTime()
            //    {
            //        DateTime = DateTime.Now.AddMinutes(61),
            //        TimeZone = "Europe/Rome",
            //    },
            //};

            //// Inserisci l'evento nel calendario principale
            //var createdEvent = service.Events.Insert(newEvent, "licksandpatterns@gmail.com").Execute();

            //Console.WriteLine("Evento creato: {0}", createdEvent.HtmlLink);
            #endregion

            #region ""
            // Percorso al file JSON del Service Account
            var serviceAccountFile = "service-account.json";

            // Email del calendario condiviso
            // oppure un altro ID calendario
            var calendarId = "licksandpatterns@gmail.com"; 

            // Carica le credenziali del Service Account
            GoogleCredential credential;
            using (var stream = new FileStream(serviceAccountFile, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(CalendarService.Scope.Calendar);
            }

            // Crea il servizio Calendar
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Calendar Service Account Example",
            });

            // Crea un evento di test
            var newEvent = new Event()
            {
                Summary = "Evento da Service Account",
                Location = "Online",
                Description = "Creato senza login utente",
                Start = new EventDateTime()
                {
                    DateTime = DateTime.Now.AddHours(1),
                    TimeZone = "Europe/Rome",
                },
                End = new EventDateTime()
                {
                    DateTime = DateTime.Now.AddHours(2),
                    TimeZone = "Europe/Rome",
                },
            };

            // Inserisci l'evento
            var createdEvent = service.Events.Insert(newEvent, calendarId).Execute();

            //Console.WriteLine($"Evento creato: {createdEvent.HtmlLink}");
            #endregion
        }
    }
}
