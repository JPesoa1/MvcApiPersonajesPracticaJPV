﻿using MvcApiPersonajesPracticaJPV.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcApiPersonajesPracticaJPV.Services
{
    public class ServicePersonaje
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApi;

        public ServicePersonaje(IConfiguration configuration)
        {
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>
                ("ApiUrls:ApiCrudPersonajes");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }



        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/personaje";
            List<Personaje> personajes =
                await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<Personaje> FindPersonajeAsync(int id)
        {
            string request = "/api/personaje/" + id;
            Personaje personaje=
                await this.CallApiAsync<Personaje>(request);
            return personaje;
        }

        
        public async Task DeletePersonajeAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personaje/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
               
            }
        }

        public async Task InsertPersonajeAsync
            (string nombre,string imagen , int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personaje";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                Personaje personajes = new Personaje();
                personajes.Nombre = nombre;
                personajes.Imagen = imagen;
                personajes.IdSerie = idserie;

                string json = JsonConvert.SerializeObject(personajes);
               
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        public async Task UpdatePersonajeAsync
            (int idpersonaje, string nombre,string imagen, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personaje";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                Personaje personajes = new Personaje();
                personajes.IdPersoanje = idpersonaje;
                personajes.Nombre = nombre;
                personajes.Imagen = imagen;
                personajes.IdSerie = idserie;
                string json = JsonConvert.SerializeObject(personajes);
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }






    }
}
