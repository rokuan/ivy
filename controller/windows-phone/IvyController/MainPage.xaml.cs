using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Net.Sockets;

namespace IvyController
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string ipValue;
        private int portValue;

        // Constructeur
        public MainPage()
        {
            InitializeComponent();
        }

        private void connection(object sender, RoutedEventArgs e)
        {
            if (ipAddress.Text == "")
            {
                //Afficher un message
                System.Windows.MessageBox.Show("Veuillez specifier une adresse");
                return;
            }

            if (ipAddress.Text.StartsWith("127.0.0."))
            {
                //Afficher un message
                System.Windows.MessageBox.Show("L'adresse 'localhost' n'est pas supportee");
                return;
            }

            try
            {
                portValue = int.Parse(port.Text);
            }
            catch (Exception ex1)
            {
                System.Windows.MessageBox.Show("Le port doit etre un entier");
                return;
            }

            ipValue = ipAddress.Text;

            try
            {
                SocketAsyncEventArgs sa = new SocketAsyncEventArgs();
                IPAddress ip = IPAddress.Parse(ipValue);

                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);                

                sa.RemoteEndPoint = new IPEndPoint(ip, portValue);
                sock.ConnectAsync(sa);

                /* TODO: Basculer vers la page du Pad et envoyer les donnees de connexion */
                sock.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("La connexion a echoue");
                System.Windows.MessageBox.Show(ex.StackTrace);
                return;
            }

            NavigationService.Navigate(new Uri("/PadPage.xaml?ipValue=" + ipAddress.Text + "&portValue=" + port.Text, UriKind.Relative));
        }
    }
}