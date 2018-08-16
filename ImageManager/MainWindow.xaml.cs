using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ImageManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int LEFT_IMAGE_MAX_INDEX = 4;
        private const int RIGHT_IMAGE_MAX_INDEX = 4;

        private ImageCommand imageCommand;
        private List<ImageSource> leftImages;
        private List<ImageSource> rightImages;
        private int leftImageCurrentIndex;
        private int rightImageCurrentIndex;

        private DispatcherTimer timer;
        private int milliSeconds = 0;
        private bool isRecording;

        public MainWindow()
        {
            InitializeComponent();
            leftImages = new List<ImageSource>();
            rightImages = new List<ImageSource>();
            leftImageCurrentIndex = 0;
            rightImageCurrentIndex = 0;
            ImageSearch();
            imageLeft.Source = leftImages[leftImageCurrentIndex];
            imageRight.Source = rightImages[rightImageCurrentIndex];
            isRecording = false;
            imageCommand = new ImageCommand();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(10000);
            timer.Tick += TimerTick;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            milliSeconds++;
        }
        #region ImageMethod
        private void ImageSearch()
        {
            for (int i = 1; i <= 5; i++)
            {
                leftImages.Add(new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\LeftImages\" + i.ToString() + ".jpg")));
                rightImages.Add(new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\RightImages\" + i.ToString() + ".jpg")));
            }
        }
        private void ImageLeftMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (leftImageCurrentIndex >= LEFT_IMAGE_MAX_INDEX)
            {
                leftImageCurrentIndex = 0;
            }
            imageLeft.Source = leftImages[++leftImageCurrentIndex];
            if(isRecording)
            {
                imageCommand.AddCommand("WAIT-" + milliSeconds.ToString());
                milliSeconds = 0;
                imageCommand.AddCommand("L-LMB");
            }
        }

        private void ImageLeftMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (leftImageCurrentIndex <= 0)
            {
                leftImageCurrentIndex = LEFT_IMAGE_MAX_INDEX;
            }
            imageLeft.Source = leftImages[--leftImageCurrentIndex];
            if (isRecording)
            {
                imageCommand.AddCommand("WAIT-" + milliSeconds.ToString());
                milliSeconds = 0;
                imageCommand.AddCommand("L-RMB");
            }
        }

        private void ImageRightMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (rightImageCurrentIndex >= LEFT_IMAGE_MAX_INDEX)
            {
                rightImageCurrentIndex = 0;
            }
            imageRight.Source = rightImages[++rightImageCurrentIndex];
            if (isRecording)
            {
                imageCommand.AddCommand("WAIT-" + milliSeconds.ToString());
                milliSeconds = 0;
                imageCommand.AddCommand("R-LMB");
            }
        }

        private void ImageRightMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (rightImageCurrentIndex <= 0)
            {
                rightImageCurrentIndex = RIGHT_IMAGE_MAX_INDEX;
            }
            imageRight.Source = rightImages[--rightImageCurrentIndex];
            if (isRecording)
            {
                imageCommand.AddCommand("WAIT-" + milliSeconds.ToString());
                milliSeconds = 0;
                imageCommand.AddCommand("R-RMB");
            }
        }
        #endregion

        private void StartButtonClick(object sender, RoutedEventArgs e)
        {
            timer.Start();
            isRecording = true;
        }

        private void StopButtonClick(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            isRecording = false;
            foreach (var i in imageCommand.Commands)
            {
                commandListBox.Items.Add(i);
            }
        }

        #region FileWork

        #endregion
        #region otherButtons
        private void PerformButtonClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < imageCommand.Commands.Count; i++)
            {
                string[] str = imageCommand.Commands[i].Split(new char[] { '-' });
                if (str[0] == "WAIT")
                {
                    Thread.Sleep(int.Parse(str[1]));
                }
                else if (str[0] == "L")
                {
                    if (str[1] == "LMB")
                    {
                        ImageLeftMouseLeftButtonDown(sender, null);
                    }
                    else
                    {
                        ImageLeftMouseRightButtonDown(sender, null);
                    }
                }
                else if (str[0] == "R")
                {
                    if (str[1] == "LMB")
                    {
                        ImageRightMouseLeftButtonDown(sender, null);
                    }
                    else
                    {
                        ImageRightMouseRightButtonDown(sender, null);
                    }
                }
            }
        }

        private void DownloadButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
