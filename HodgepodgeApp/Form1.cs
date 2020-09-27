using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HodgepodgeApp
{
    public partial class Form1 : Form
    {
        Bitmap image;
        int m_hour, m_minit, m_sec;

        public Form1()
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(0.1);
            m_hour = 0;
            m_minit = 0;
            m_sec = 0;
            timer1 = new Timer();
            timer1.Interval = 1000;
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Go_Click(object sender, EventArgs e)
        {
            Navigate(this.textBox1.Text);
        }

        private void Navigate(String address)
        {
            if (String.IsNullOrEmpty(address)) return;
            if (address.Equals("about:blank")) return;
            if (!address.StartsWith("http://") &&
                !address.StartsWith("https://"))
            {
                address = "http://" + address;
            }
            try
            {
                webBrowser1.Navigate(new Uri(address));
            }
            catch (System.UriFormatException)
            {
                return;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Navigate(this.textBox1.Text);
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.textBox1.Text = webBrowser1.Url.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.png; *.png; *.jpg; *.bmp | All Files (*.*) | *.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                updateImg();
            }
        }

        private void updateImg()
        {
            pictureBox1.Image = image;
            pictureBox1.Refresh();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (image is null) return;
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            updateImg();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (image is null) return;
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            image = null;
            updateImg();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                timer1.Interval = 1000;
                button6.Text = "Старт";
                button5.Enabled = true;
            }
            else
            {
                timer1.Interval = 1000;
                timer1.Enabled = true;
                button6.Text = "Стоп";
                button5.Enabled = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
            }
            timer1.Stop();
            textBox3.Text = Convert.ToString(m_hour);
            textBox2.Text = Convert.ToString(m_minit);
            textBox4.Text = Convert.ToString(m_sec);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            m_hour = 0;
            m_minit = 0;
            m_sec = 0;
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
        }

        private void updateTime(object sender, EventArgs e)
        {
            if (timer1 is null) return;
            if (m_sec == 0)
            {
                if (m_minit == 0)
                {
                    if (m_hour == 0)
                    {
                        button6_Click(sender, e);
                        return;
                    }
                    else
                    {
                        m_hour--;
                        m_minit = 59;
                        m_sec = 59;
                    }
                }
                else
                {
                    m_minit--;
                    m_sec = 59;
                }
            }
            else
            {
                m_sec--;
            }
            int hour = m_hour;
            int sec = m_sec;
            int minit = m_minit;
            textBox3.Text = Convert.ToString(hour);
            textBox4.Text = Convert.ToString(minit);
            textBox2.Text = Convert.ToString(sec);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (timer1.Enabled) return;
            try
            {
                m_hour = Convert.ToInt32(textBox3.Text);
                if(m_hour < 0) textBox3.Text = "0";
            }
            catch (System.Exception exep)
            {
                textBox3.Text = Convert.ToString(m_hour);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (timer1.Enabled) return;
            try
            {
                m_sec = Convert.ToInt32(textBox2.Text);
                if (m_sec < 0) textBox2.Text = "0";
                else if (m_sec >= 60) textBox2.Text = "59";
            }
            catch (System.Exception exep)
            {
                textBox2.Text = Convert.ToString(m_minit);
            }
        }

            private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (timer1.Enabled) return;
            try
            {
                m_minit = Convert.ToInt32(textBox4.Text);
                if (m_minit < 0 ) textBox4.Text = "0";
                else if (m_minit >= 60) textBox4.Text = "59";
            }
            catch (System.Exception exep)
            {
                textBox4.Text = Convert.ToString(m_minit);
            }
        }
    }
 }
