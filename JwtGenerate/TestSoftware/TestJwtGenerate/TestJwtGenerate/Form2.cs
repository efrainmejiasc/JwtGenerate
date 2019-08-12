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
    public partial class Form2 : Form
    {
        private string accessToken = string.Empty;
        public Form2()
        {
            InitializeComponent();
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
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:60391/api/User/CreateUser");
            UserApi U = new UserApi();
            U.Email = "efrainmejiasc@gmail.com.com";
            U.Password = "1234Santiago" ;
            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("Email", U.Email));
            formData.Add(new KeyValuePair<string, string>("Password", U.Password));
            request.Content = new FormUrlEncodedContent(formData);
            var stringified = JsonConvert.SerializeObject(U);
            var response = await client.PostAsync("http://localhost:60391/api/User/CreateUser", new StringContent(stringified, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                resultado = response.Content.ReadAsStringAsync().Result;
                richTextBox1.Text = resultado;
            }
            else
            {
                resultado = response.StatusCode.ToString();
            }
            return resultado;
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
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:60391/api/User/Login");
            User U = new User();
            U.Email = "efrainmejiasc@gmail.com";
            U.Password = "1234Santiago";
            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("Password", U.Password));
            formData.Add(new KeyValuePair<string, string>("Email", U.Email));
            request.Content = new FormUrlEncodedContent(formData);
            var stringified = JsonConvert.SerializeObject(U);
            var response = await client.PostAsync("http://localhost:60391/api/User/Login", new StringContent(stringified, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                resultado = response.Content.ReadAsStringAsync().Result;
                richTextBox1.Text= resultado;
            }
            else
            {
                resultado = response.StatusCode.ToString();
            }
            accessToken = resultado;
            // UpdateUser();
            //DeleteUser();
            return resultado;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            LoginUser();

        }

        public async Task<string> UpdateUser()
        {
            StringToken Item = new StringToken();
            Item = JsonConvert.DeserializeObject<StringToken>(accessToken);
            string resultado = string.Empty;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Item.Token);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "http://localhost:60391/api/User/UpdateUser");
            Client U = new Client();
            U.Email = "marianita@yahoo.com";
            U.Password = "202020";
            U.NewPassword = "esteeselnuevopassword";
            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("Email", U.Email));
            formData.Add(new KeyValuePair<string, string>("Password", U.Password));
            formData.Add(new KeyValuePair<string, string>("NewPassword", U.NewPassword));
            request.Content = new FormUrlEncodedContent(formData);
            var stringified = JsonConvert.SerializeObject(U);
            HttpResponseMessage response = await client.PutAsync("http://localhost:60391/api/User/UpdateUser", new StringContent(stringified, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                resultado = response.Content.ReadAsStringAsync().Result;
                richTextBox1.Text = resultado;
            }
            else
            {
                resultado = response.StatusCode.ToString();
            }
            return resultado;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoginUser();
        }

        public async Task<string> DeleteUser()
        {
            StringToken Item = new StringToken();
            Item = JsonConvert.DeserializeObject<StringToken>(accessToken);
            string resultado = string.Empty;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Item.Token);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "http://localhost:60391/api/User/DeleteUser");
            UserApi U = new UserApi();
            U.Email = "marianita@yahoo.com";
            U.Password = "esteeselnuevopassword";
            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("Email", U.Email));
            formData.Add(new KeyValuePair<string, string>("Password", U.Password));
            request.Content = new FormUrlEncodedContent(formData);
            var stringified = JsonConvert.SerializeObject(U);
            HttpResponseMessage response = await client.DeleteAsync("http://localhost:60391/api/User/DeleteUser");
            if (response.IsSuccessStatusCode)
            {
                resultado = response.Content.ReadAsStringAsync().Result;
                richTextBox1.Text = resultado;
            }
            else
            {
                resultado = response.StatusCode.ToString();
            }
            return resultado;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CreateCompany();
        }

        public async Task<string> CreateCompany()
        {
            string resultado = string.Empty;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:60391/api/DataCompany");
            Company com = new Company();
            com = SetCompany();
            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("NameCompany",com.NameCompany));
            formData.Add(new KeyValuePair<string, string>("BusinessBranch", com.BusinessBranch));
            formData.Add(new KeyValuePair<string, string>("Email", com.Email));
            formData.Add(new KeyValuePair<string, string>("Phone", com.Phone));
            formData.Add(new KeyValuePair<string, string>("AnnualGross", com.AnnualGross.ToString()));
            formData.Add(new KeyValuePair<string, string>("CreateDate",com.CreateDate.ToString()));
            formData.Add(new KeyValuePair<string, string>("TypeCompany", com.TypeCompany));
            request.Content = new FormUrlEncodedContent(formData);
            var stringified = JsonConvert.SerializeObject(com);
            var response = await client.PostAsync("http://localhost:60391/api/DataCompany", new StringContent(stringified, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                resultado = response.Content.ReadAsStringAsync().Result;
                richTextBox1.Text = resultado;
            }
            else
            {
                resultado = response.StatusCode.ToString();
            }
            return resultado;
        }

         private void button6_Click(object sender, EventArgs e)
        {
            DeleteClient();
        }
        public async Task<string> DeleteClient()
        {
            StringToken Item = new StringToken();
            Item = JsonConvert.DeserializeObject<StringToken>(accessToken);
            string resultado = string.Empty;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Item.Token);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "http://localhost:60391/api/DataCompany/DeleteClient");
            Client ide = new Client();
            ide.Id = 9;
            var formData = new List<KeyValuePair<string, string>>();
            formData.Add(new KeyValuePair<string, string>("Id", ide.Id.ToString()));
            request.Content = new FormUrlEncodedContent(formData);
            var stringified = JsonConvert.SerializeObject(ide);
            HttpResponseMessage response = await client.DeleteAsync("http://localhost:60391/api/DataCompany/DeleteClient");
            if (response.IsSuccessStatusCode)
            {
                resultado = response.Content.ReadAsStringAsync().Result;
                richTextBox1.Text = resultado;
            }
            else
            {
                resultado = response.StatusCode.ToString();
            }
            return resultado;
        }


        public Company SetCompany()
        {
            Company C = new Company();
            try
            {

                C.NameCompany = "PDVSA";
                C.BusinessBranch = "PETROLEO";
                C.Email = "pdvsa@venezuela.com";
                C.Phone = "000589700";
                C.AnnualGross = 132155;
                C.CreateDate = DateTime.UtcNow;
                C.TypeCompany = "BIG";
                List<Subsidiary> list = new List<Subsidiary>();
                C.Subsidiary[0].NameSubsidiary = "DelSub";
                C.Subsidiary[0].Email = "delsub@dell.com";
                C.Subsidiary[0].Phone = "21354679";
                C.Subsidiary[0].AnnualGross = 476235;
                C.Subsidiary[0].CreateDate = DateTime.Now;
            }
            catch (Exception ex)
            {

            }
            return C;

        }

        public class Ide
        {
           public int Id { get; set; }
        }

        public class UserApi
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public DateTime CreateDate { get; set; }
        }

        public class Client
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string NewPassword { get; set; }
        }

        public class StringToken
        {
            public string Token { get; set; }
        }

        public class Company
        {
            public int Id { get; set; }
            public string NameCompany { get; set; }
            public string BusinessBranch { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public float AnnualGross { get; set; }
            public DateTime CreateDate { get; set; }
            public string TypeCompany { get; set; }
            public List<Subsidiary> Subsidiary { get; set; }
        }

        public class Subsidiary
        {
            public int Id { get; set; }
            public int CompanyId { get; set; }
            public string NameSubsidiary { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public float AnnualGross { get; set; }
            public DateTime CreateDate { get; set; }
            public string TypeSubsidiary { get; set; }
        }

       
    }
}
