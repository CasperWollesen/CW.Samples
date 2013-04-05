using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using CW.Sample.WCF.WcfExampleService.Client;
using CW.Sample.WCF.WcfExampleService.Host;
using CW.Sample.WCF.WcfExampleService.Models;
using NUnit.Framework;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WcfExampleServiceHost _host;

        public MainWindow()
        {
            InitializeComponent();
        }

  

        private async void ActionAsyncClick(object sender, RoutedEventArgs e)
        {
            var btn = (sender as Button);

            if (btn == null)
            {
                return;
            }

            btn.Content = "Starting";
            
            var client = new WcfExampleServiceAsyncClient("localhost:10000");
            SimpleSyncMethodResponseModel responseSimpleSyncMethod = await client.SimpleSyncMethodAsync(new SimpleSyncMethodRequestModel { Message = "AsyncCall" });

            if (responseSimpleSyncMethod != null)
            {
                btn.Content += " " + responseSimpleSyncMethod.Message;
            }

            btn.Content += " Done";
        }

        private void ActionSyncClick(object sender, RoutedEventArgs e)
        {
            var btn = (sender as Button);

            if (btn == null)
            {
                return;
            }

            btn.Content = "Starting";
            
            var client = new WcfExampleServiceAsyncClient("localhost:10000");
            SimpleSyncMethodResponseModel responseSimpleSyncMethod = client.SimpleSyncMethod(new SimpleSyncMethodRequestModel { Message = "AsyncCall" });

            if (responseSimpleSyncMethod != null)
            {
                btn.Content += " " + responseSimpleSyncMethod.Message;
            }

            
            btn.Content += " Done";
        }

        private bool Active;
        private void StartHost()
        {
            Active = true;
            var thread = new System.Threading.Thread(() =>
                {
                    _host = new WcfExampleServiceHost("localhost:10000");
                    _host.Start();

                    while (Active)
                    {
                        System.Threading.Thread.Sleep(10);
                    }


                });
            thread.Start();

            
        }

        private void CloseHost()
        {
            Active = false;
            _host.Close();
            _host.Dispose();
        }

        private void ActionWindowLoaded(object sender, RoutedEventArgs e)
        {
            StartHost();
        }

        private void ActionWindowClosed(object sender, EventArgs e)
        {
            CloseHost();
        }
    }
}
