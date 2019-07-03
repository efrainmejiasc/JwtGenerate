using Newtonsoft.Json;
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
using System.Threading;
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
            NumberFactory();
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
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:58663/api/CreateClient");
            User User = new User();
            User = SetUser(false);
            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("Username", User.Username));
            formData.Add(new KeyValuePair<string, string>("Password", User.Password));
            formData.Add(new KeyValuePair<string, string>("EmailAddress", User.Email));
            formData.Add(new KeyValuePair<string, string>("SignatureApp", User.SignatureApp));
            formData.Add(new KeyValuePair<string, string>("FechaRegistro", User.RegisteredDate.ToString()));
            formData.Add(new KeyValuePair<string, string>("ExpiracionToken", User.ExpiracionToken));
            request.Content = new FormUrlEncodedContent(formData);
            var stringified = JsonConvert.SerializeObject(User);
            var response = await client.PostAsync("http://localhost:58663/api/CreateClient", new StringContent(stringified, Encoding.UTF8, "application/json"));
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

        private User SetUser(bool n)
        {
            User User = new User
            {
                Id = 0,
                Name = "Efrain",
                LastName = "Mejias C",
                Username = "EfrainMejiasC",
                Password = "1234santiago",
                Email = "efrainmejiasc@gmail.com",
                PhoneNumber = "+5804204133677",
                FavoriteGame = "BaseBall",
                Gender = "Masculino",
                BirthDate = Convert.ToDateTime("1972/02/08"),
                RegisteredDate = DateTime.UtcNow,
                ExpiracionToken = "",
                SignatureApp = "MiFirmaElectronica",
                RegisteredStatus = n
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
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:58663/api/LoginClient");
            User User = new User();
            User = SetUser(false);
            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("Username", User.Username));
            formData.Add(new KeyValuePair<string, string>("Password", User.Password));
            formData.Add(new KeyValuePair<string, string>("Email", User.Email));

            request.Content = new FormUrlEncodedContent(formData);
            var stringified = JsonConvert.SerializeObject(User);
            var response = await client.PostAsync("http://localhost:58663/api/LoginClient", new StringContent(stringified, Encoding.UTF8, "application/json"));
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


        public string NumberFactory()
        {
            string resultado = string.Empty;
            int s = DateTime.Now.Millisecond;
            for (int i = 0; i <= 3; i++)
            {
                if (i == 0)
                    resultado = Aleatorio(s).ToString();
                else
                    resultado = s.ToString() + Aleatorio(s).ToString();

                Thread.Sleep(600);
                s = DateTime.Now.Millisecond;
            }
            textBox1.Text = resultado;
            return resultado;
        }

        private int Aleatorio(int s)
        {
            Random rnd = new Random(s);
            int n = rnd.Next(1, 9);
            return n;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            ActivarCuenta();
        }

        public async Task<string> ActivarCuenta()
        {
            string resultado = string.Empty;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:58445/api/CreateClient");
            CodeToVerification ToVerification = new CodeToVerification();
            ToVerification  = SetToVerification();
            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("UserName", ToVerification.Username));
            formData.Add(new KeyValuePair<string, string>("Password", ToVerification.Password));
            formData.Add(new KeyValuePair<string, string>("Email", ToVerification.Email));
            formData.Add(new KeyValuePair<string, string>("Code", ToVerification.Code));

            request.Content = new FormUrlEncodedContent(formData);
            var stringified = JsonConvert.SerializeObject(ToVerification);
            var response = await client.PutAsync("http://localhost:58445/api/CreateClient", new StringContent(stringified, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                resultado = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                resultado = response.StatusCode.ToString();
            }
            return resultado;
        }

        private CodeToVerification SetToVerification()
        {
            CodeToVerification S = new CodeToVerification()
            {
                Username = "EfrainMejiasC",
                Password = "1234santiago",
                Email = "efrainmejiasc@gmail.com",
                Code = "426680",
                Status = true
            };
            return S;
        }


    }
}
