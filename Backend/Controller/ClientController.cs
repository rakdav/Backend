using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Backend.Controller
{
    class ClientController
    {
        public async static void AddClient(string json,HttpListenerContext context)
        {
            using (TestdbContext db = new TestdbContext())
            {
                Client? person = JsonSerializer.Deserialize<Client>(json);
                db.Clients.Add(new Client()
                {
                    City = person!.City,
                    Company = person.Company,
                    Firstname = person.Firstname,
                    Phone = person.Phone,
                    Lastname = person.Lastname,
                    Surname = person.Surname
                });
                await db.SaveChangesAsync();
                var response = context.Response;
                string responseText = "OK";
                byte[] buffer = Encoding.UTF8.GetBytes(responseText);
                response.ContentLength64 = buffer.Length;
                response.ContentType = "text/html";
                response.ContentEncoding = Encoding.UTF8;
                using Stream output = response.OutputStream;
                await output.WriteAsync(buffer);
                await output.FlushAsync();
                Console.WriteLine("Запрос обработан");
            }
        }
        public async static void getClients(HttpListenerContext context)
        {
            using (TestdbContext db = new TestdbContext())
            {
                List<Client> clients = db.Clients.ToList();
                string json = JsonSerializer.Serialize<List<Client>>(clients);
                var response = context.Response;
                string responseText = json;
                byte[] buffer = Encoding.UTF8.GetBytes(responseText);
                response.ContentLength64 = buffer.Length;
                response.ContentType = "text/html";
                response.ContentEncoding = Encoding.UTF8;
                using Stream output = response.OutputStream;
                await output.WriteAsync(buffer);
                await output.FlushAsync();
                Console.WriteLine("Запрос обработан");
            }
        }
    }
}
