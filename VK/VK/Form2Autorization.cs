using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VK
{
    public partial class Form2Autorization : Form
    {
        public Form2Autorization()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://oauth.vk.com/authorize?client_id=5430178&scope=audio,offline&redirect_uri=https://oauth.vk.com/blank.html&display=popup&response_type=token&v=5.50");
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            toolStripStatusLabel1.Text = "Load";
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            toolStripStatusLabel1.Text = "Done";

            try
            {
                string browserURL = webBrowser1.Url.ToString();
                string l = browserURL.Split('#')[1];
                if (l[0] == 'a')
                {
                    Settings1.Default.user_token = l.Split('&')[0].Split('=')[1];
                    Settings1.Default.user_ID = l.Split('=')[3];
                    Settings1.Default.auth = true;
                    MessageBox.Show(Settings1.Default.user_token);
                    this.Close();
                }
            }
            catch { }
        }
    }
}
