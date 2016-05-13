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

namespace VK
{
    public partial class Form1AudioPlayer : Form
    {
        WMPLib.IWMPMedia media;
        WMPLib.IWMPPlaylist playList;
        public Form1AudioPlayer()
        {
            InitializeComponent();

        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            new Form2Autorization().ShowDialog();
            backgroundWorker1.RunWorkerAsync();
        }
        public List<Audio> audioList = null;
        
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!Settings1.Default.auth)
            {
                Thread.Sleep(500);
            }

            WebRequest request =
                WebRequest.Create("https://api.vk.com/method/audio.get?user_id=" + Settings1.Default.user_ID + "&v=5.50&need_user=0&access_token=" + Settings1.Default.user_token);
            MessageBox.Show(request.ToString());
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader dataReader = new StreamReader(dataStream);
            string responseFromServer = dataReader.ReadToEnd();
            dataReader.Close();
            dataStream.Close();
            response.Close();

            responseFromServer = HttpUtility.HtmlDecode(responseFromServer);
            //MessageBox.Show(responseFromServer);

            JToken token = JToken.Parse(responseFromServer);
            audioList = token["response"]["items"].Children().Skip(1).Select(c => c.ToObject<Audio>()).ToList();
            this.Invoke((MethodInvoker)delegate 
            {
                playList = axWindowsMediaPlayer1.playlistCollection.newPlaylist("vkPlayList");
                for (int i = 0; i < audioList.Count(); i++)
                {
                    media = axWindowsMediaPlayer1.newMedia(audioList[i].url);
                    playList.appendItem(media);
                    listBox1.Items.Add(audioList[i].artist + " - " + audioList[i].title);
                }
                axWindowsMediaPlayer1.currentPlaylist = playList;
                axWindowsMediaPlayer1.Ctlcontrols.stop();
            });
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                axWindowsMediaPlayer1.Ctlcontrols.currentItem = axWindowsMediaPlayer1.currentPlaylist.get_Item(listBox1.SelectedIndex);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          Form3MusicDownload musicDownload=  new Form3MusicDownload();
          musicDownload.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings1.Default.Reset();
            MessageBox.Show(Settings1.Default.user_token);
        }

      
      
    }
}

