using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MTouchPadServerShell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitService();
        }

        private ITouchServer _touchServer = null;
        private void InitService()
        {
            _touchServer = new TouchServer();
            _touchServer.Start(result =>
                                  {
                                      tb_ServerState.Text = result ? "Server running." : "Server failed to start.";
                                  });
        }

        private void BtnOkClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState= WindowState.Minimized;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _touchServer.Stop();
        }
    }
}
