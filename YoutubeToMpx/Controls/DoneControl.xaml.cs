using System;
using System.Windows;
using System.Windows.Controls;

namespace YoutubeDownloader.Controls
{
    /// <summary>
    /// Interaction logic for DoneControl.xaml
    /// </summary>
    public partial class DoneControl : UserControl
    {
        public delegate void DoneReadyDelegate(object sender, EventArgs e);
        public event DoneReadyDelegate DoneReady;

        public DoneControl()
        {
            InitializeComponent();
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void GoAgainButton_Click(object sender, RoutedEventArgs e)
        {
            DoneReady?.Invoke(this, EventArgs.Empty);
        }
    }
}
