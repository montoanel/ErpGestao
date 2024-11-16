using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace ErpGestao
{
    public partial class WebcamCaptureForm : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private Bitmap capturedImage;
        public WebcamCaptureForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            //inicializar dispositivos de video

            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in videoDevices)
            {
                cmbwebcamlist.Items.Add(device.Name);
            }
            if (videoDevices.Count > 0)
            {
                cmbwebcamlist.SelectedIndex = 0;
            }

        }

        private void btnstartrecord_Click(object sender, EventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
            }

            videoSource = new VideoCaptureDevice(videoDevices[cmbwebcamlist.SelectedIndex].MonikerString);
            videoSource.NewFrame += VideoSource_NewFrame;
            videoSource.Start();
        }

        private void btnstoprecord_Click(object sender, EventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
            }
        }

        private void btnsavepicture_Click(object sender, EventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
            }
            if (pctcapturar.Image != null)
            {
                capturedImage = new Bitmap(pctcapturar.Image);
                pctsave.Image = capturedImage;

                //salvar a imagem em um arquivo temporario

                string tempFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "captured_image.jpg");
                capturedImage.Save(tempFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                MessageBox.Show("Imagem capturada e salva em:" + tempFilePath);

            }
        }

        private void btnvoltar_Click(object sender, EventArgs e)
        {

            //encerra o serviço da webcam
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.NewFrame -= VideoSource_NewFrame;
            }

            this.Close();
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap frame = (Bitmap)eventArgs.Frame.Clone();
            pctcapturar.Image = frame;
        }

        public Bitmap GetCapturedImage()
        {
            return capturedImage;
        }

        private void WebcamCaptureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //encerra serviço da camera

            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.NewFrame -= VideoSource_NewFrame;
            }
        }
    }
}
