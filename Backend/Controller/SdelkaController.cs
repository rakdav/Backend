using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backend.Controller
{
    public class SdelkaController
    {
        public async static void getSdelkas(HttpListenerContext context)
        {
            using (TestdbContext db = new TestdbContext())
            {
                List<Sdelka> list = db.Sdelkas.ToList();
                string json = JsonSerializer.Serialize<List<Sdelka>>(list);
                var response = context.Response;
                string responseText = json;
                byte[] buffer = Encoding.UTF8.GetBytes(responseText);
                response.ContentLength64 = buffer.Length;
                response.ContentType = "text/gson";
                response.ContentEncoding = Encoding.UTF8;
                using Stream output = response.OutputStream;
                await output.WriteAsync(buffer);
                await output.FlushAsync();
                Console.WriteLine("Запрос обработан");
            }
        }
    }
}
