﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestJwtGenerate
{
    public partial class Form1 : Form
    {

        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1NjE5ODU3MDMsImlzcyI6IlRoaXNpc215U2VjcmV0S2V5IiwiYXVkIjoiVGVzdC5jb20ifQ.jb39ebjjzyA6Y-_NVBT4PA3Y6I1sK61numdBygF3rB0";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
             CreateUser();
        }

        public async Task<string> CreateUser()
        {
            string resultado = string.Empty;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:58663/api/CreateUser");
            User User = new User();
            User = SetUser();
            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("Username", User.Username));
            formData.Add(new KeyValuePair<string, string>("Password", User.Password));
            formData.Add(new KeyValuePair<string, string>("EmailAddress", User.Email));
            formData.Add(new KeyValuePair<string, string>("SignatureApp", User.SignatureApp));
            formData.Add(new KeyValuePair<string, string>("FechaRegistro", User.FechaRegistro));
            formData.Add(new KeyValuePair<string, string>("ExpiracionToken", User.ExpiracionToken));
            request.Content = new FormUrlEncodedContent(formData);
            var stringified = JsonConvert.SerializeObject(User);
            var response = await client.PostAsync("http://localhost:58663/api/CreateUser", new StringContent(stringified, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                resultado = response.Content.ReadAsStringAsync().Result;
                User = JsonConvert.DeserializeObject<User>(resultado);
            }
            else
            {
                resultado = response.StatusCode.ToString();
            }
            return resultado;
        }

        private User SetUser()
        {
            User User = new User
            {
                Id = 0,
                Username = "EfrainMejias",
                Password = "1234santiago",
                Email = "efrainmejiasc@gmail.com",
                FechaRegistro = DateTime.UtcNow.ToString(),
                ExpiracionToken = "",
                SignatureApp ="MiFirmaElectronica"
            };
            return User;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           LoginUser();
        }

        private async Task<string> LoginUser()
        {
            string resultado = string.Empty;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:58663/api/LoginUser");
            User User = new User();
            User = SetUser();
            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("Username", User.Username));
            formData.Add(new KeyValuePair<string, string>("Password", User.Password));

            request.Content = new FormUrlEncodedContent(formData);
            var stringified = JsonConvert.SerializeObject(User);
            var response = await client.PostAsync("http://localhost:58663/api/LoginUser", new StringContent(stringified, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                resultado = response.Content.ReadAsStringAsync().Result;
                User = JsonConvert.DeserializeObject<User>(resultado);
            }
            else
            {
                resultado = response.StatusCode.ToString();
            }
            return resultado;
        }

        public string DecodeBase64(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] p = token.Split('.');
            string resultado = DecodeBase64(p[1]);
            int K = 0;
        }
    }
}
