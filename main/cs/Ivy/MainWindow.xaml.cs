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
using System.Threading;
using System.Net.Sockets;
using Ivy.Components;
using Ivy.Activity;

namespace Ivy
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BaseActivity act;

        public MainWindow()
        {
            InitializeComponent();

            //ThreadStart t = new ThreadStart(connexionServeur);
            IvyComponent comp = IvyXMLParser.parseXML("test.xml");

            if (comp == null)
            {
                return;
            }

            content.Children.Add(comp.getComponent());

            Thread t = new Thread(new ThreadStart(delegate()
            {
                try
                {
                    act = new TestActivity(4);
                    act.connect();
                    act.waitForClients();
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(e.StackTrace);
                }
            }));

            t.Start();

        }

        private void OnWindowClosing(object sender, EventArgs e)
        {
            if (act != null)
            {
                act.disconnect();
            }
        }
    }
}
