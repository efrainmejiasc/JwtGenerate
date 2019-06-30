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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestJwtGenerate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           CreateUser();
        }

        public async Task<string> CreateUser()
        {
            string resultado = string.Empty;
            try
            {
                HttpClient client = new HttpClient();
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:58663/api/test");
                User User = new User();
                User = SetUser();
                var formData = new List<KeyValuePair<string, string>>();
                formData.Add(new KeyValuePair<string, string>("Username", User.Username));
                formData.Add(new KeyValuePair<string, string>("Password", User.Password));
                formData.Add(new KeyValuePair<string, string>("EmailAddress", User.EmailAddress));
                formData.Add(new KeyValuePair<string, string>("SignatureApp", User.SignatureApp));
                request.Content = new FormUrlEncodedContent(formData);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    resultado = response.Content.ReadAsStringAsync().Result;
                    User = JsonConvert.DeserializeObject<User>(resultado);
                }
                else
                {
                    resultado = response.StatusCode.ToString();
                }
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
            }

            return resultado;
        }

        private User SetUser()
        {
            User User = new User
            {
                Username = "EfrainMejias",
                Password = "1234santiago",
                EmailAddress = "efrainmejiasc@gmail.com",
                SignatureApp = "MiFirmaElectronica"
            };
            return User;
        }

    }
}
