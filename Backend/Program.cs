using Backend;
using Backend.Controller;
using Backend.Model;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
HttpClient httpClient = new HttpClient();
HttpListener server = new HttpListener();
server.Prefixes.Add("http://127.0.0.1:8888/connection/");
server.Start();
while (true)
{
    var context = await server.GetContextAsync();
    var body = context.Request.InputStream;
    var method = context.Request.HttpMethod;
    var encoding = context.Request.ContentEncoding;
    var reader = new StreamReader(body, encoding);
    string query = reader.ReadToEnd();
    Console.WriteLine(query);
    string table= context.Request.Headers[0]!;
    if (method == "POST")
    {
        switch(table)
        {
            case "client": ClientController.AddClient(query,context);break;
        }
    }
    else if(method=="GET")
    {
        switch (table)
        {
            case "client": ClientController.getClients(context); break;
        }
    }
    else if (method == "PUT")
    {
        switch (table)
        {
            case "client": break;
        }
    }
   
}
server.Stop();

