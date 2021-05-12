using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using UnityEngine.UI;
using System.Text;

public class ServerScript : MonoBehaviour
{
    public void My_SendMsg()
    {
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress ipaddress = IPAddress.Parse("127.0.0.1");     // The IP address to connect to a server.
        IPEndPoint endpoint = new IPEndPoint(ipaddress, 8888);  // The port number.
        server.Connect(endpoint);
        string msg = "Hello. I'm here.";
        server.Send(Encoding.UTF8.GetBytes(msg));
        Thread.Sleep(5000);
        server.Close();
    }
}    
