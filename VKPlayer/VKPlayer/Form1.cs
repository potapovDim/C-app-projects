using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace VKPlayer
{
    public partial class Form1 : Form
    {
        public List<Audio> audioList = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            new Form2().Show();
            backgroundWorker1.RunWorkerAsync();
        }
        public class Audio
        {
            public int aid { get; set; }
            public int owner_id { get; set; }
            public string artist { get; set; }
            public string title { get; set; }
            public int duration { get; set; }
            public string url { get; set; }
            public string lurics_id { get; set; }
            public int genre { get; set; }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            
                while (!Settings1.Default.auth)
                {
                    Thread.Sleep(500);
                }

                WebRequest request =
                    WebRequest.Create("https://api.vk.com/method/audio.getByID?owner_id=" + Settings1.Default.id + "&need_user=0&access_token=" + Settings1.Default.token);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);

                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                response.Close();
                responseFromServer = HttpUtility.HtmlDecode(responseFromServer);

                JToken token = JToken.Parse(responseFromServer);
                audioList = token["response"].Children().Skip(1).Select(c => c.ToObject<Audio>()).ToList();

                this.Invoke((MethodInvoker)delegate
                {
                    for (int i = 0; i < audioList.Count(); i++)
                    {
                        listBox1.Items.Add(audioList[i].artist + " - " + audioList[i].title);
                    }

                });
            
          //  catch (Exception exp) { MessageBox.Show(exp.Message.ToString()); }
        }
    }
}
