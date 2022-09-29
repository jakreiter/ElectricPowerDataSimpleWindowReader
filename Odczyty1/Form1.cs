using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Security.Policy;
using Timer = System.Windows.Forms.Timer;
using Microsoft.Extensions.Configuration;

namespace Odczyty1
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.getData();

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            Timer tmr = new Timer();
            tmr.Interval = 3000;
            tmr.Tick += new EventHandler(timer_Tick);
            tmr.Start();

        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.getData();
        }

        public async void getData()
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(200);

            string url = System.IO.File.ReadAllText(@"url.txt").Trim();                      

            try
            {

                //textBox1.Text = textBox1.Text + "Establishing connection to " + url;
                using (var streamReader = new System.IO.StreamReader(await client.GetStreamAsync(url)))
                {
                    //Console.WriteLine("----Received message:");
                    while (!streamReader.EndOfStream)
                    {
                        var message = await streamReader.ReadLineAsync();

                        if (message.Contains("mac"))
                        {

                            var msgParams = JsonSerializer.Deserialize<System.Collections.Generic.Dictionary<string, JsonElement>>(message);
                            if (msgParams != null)
                            {
                                float power1 = float.Parse(msgParams["power1"].ToString());
                                power1 = power1 / 1000;
                                lblPower.Text = power1.ToString("0.0");

                                float energy6 = float.Parse(msgParams["energy6"].ToString());
                                energy6 = energy6 / 1000;

                                float energy1 = float.Parse(msgParams["energy1"].ToString());
                                energy1 = (float)(energy1 * 0.8 / 1000);

                                lblToday.Text = energy6.ToString("0.0");
                                lblBilans.Text = energy1.ToString("0");
                                if (energy1<0)  lblBilans.ForeColor = Color.Red;
                                else lblBilans.ForeColor = Color.Green;

                            }


                        }
                        else
                        {
                            //Console.WriteLine($"| {message}");
                        }
                    }
                    textBox1.Text = textBox1.Text + "End\n ";
                }
            }
            catch (Exception ex)
            {
                lblPower.Text = "?";
                lblToday.Text = "?";
                lblBilans.Text = "?";
                //textBox1.Text = "Exception\n ";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblPower_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}