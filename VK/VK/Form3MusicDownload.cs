using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace VK
{
    public partial class Form3MusicDownload : Form
    {
        public AlbumResponse[] AllComposition { get; private set; }
        WMPLib.IWMPMedia media;
        WMPLib.IWMPPlaylist playList;
        public Form3MusicDownload()
        {
            InitializeComponent();

        }
        private string VkRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var responseText = reader.ReadToEnd();
            return responseText;
        }
        private void LoadUserComposition(string userID)
        {
            var str = string.Format("https://api.vk.com/method/users.get?uids={0}", userID);
            var responseText = VkRequest(str);
            try
            {
                var users = JsonConvert.DeserializeObject<VkUsers>(responseText);
                var uid = users.response[0].uid;
                str = string.Format("https://api.vk.com/method/audio.get?uid={0}&access_token={1}",
                    uid, Settings1.Default.user_token);

                responseText = VkRequest(str);
                var album = JsonConvert.DeserializeObject<VkAlbum>(responseText);
                AllComposition = album.restonse;
                for(int i=0;i<downloadAudioList.Count();i++)
                {
                    media = axWindowsMediaPlayer1.newMedia(downloadAudioList[i].url);
                    playList.appendItem(media);
                    listBox1.Items.Add(downloadAudioList[i].artist + " - " + downloadAudioList[i].title);
               
                }
                
            }
            catch (Exception ex) { }
        }
        public List<Audio> downloadAudioList = null;

        private void Form3MusicDownload_Load(object sender, EventArgs e)
        {
            MessageBox.Show(Settings1.Default.user_ID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (user_textBox.Text == ""&&group_textBox.Text=="")
            {
                LoadUserComposition(Settings1.Default.user_ID);
                return;
            }
            if(user_textBox.Text!="")
            {
                LoadUserComposition(user_textBox.Text);
                return;
            }
            if(group_textBox.Text!="")
            {
                LoadUserComposition(group_textBox.Text);
                return;
            }
        }

        
    }
}
