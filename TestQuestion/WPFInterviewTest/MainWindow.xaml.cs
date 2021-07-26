using System.Windows;
using Microsoft.Win32;
using System.Net;
using System.Net.Sockets;


namespace WPFInterviewTest
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }
        static string fileName = null;

        public void connectAndSend(object sender, RoutedEventArgs e)
        {
            if(fileName == null){
                MessageBox.Show("Please, Enter a file before starting the process");
                return;
            }
            byte[] bytes = new byte[1024];//buffer that will be sent.

            IPHostEntry host = Dns.GetHostEntry("localhost");//getting the localhost address, 127.0.0.1
            IPAddress ipAddress = host.AddressList[1];//setting the local ip as the address
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 54000);//setting the end point socket according to the address and host

            Socket client = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);//the client socket

            client.Connect(ipEndPoint);//connecting to the end point 

            // Send file fileName to remote device
            client.SendFile(fileName);
        }

        private void fileExplorerBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "SCV files (*.CSV)|*.csv|All files (*.*)|*.*";//filltering un wanted file types
            if (openFileDialog.ShowDialog() == true){
                fileName = openFileDialog.FileName;//setting the fileName to the file picked by the user 
            }
        }
    }
}
