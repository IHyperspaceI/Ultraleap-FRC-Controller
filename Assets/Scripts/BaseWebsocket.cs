using UnityEngine;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Text;

public class BaseWebsocket : MonoBehaviour
{
    public HandManager manager;

    private TcpListener server;
    private TcpClient client;
    private NetworkStream stream;

    public string host = "127.0.0.1";
    public bool isConnected = false;

    private int port = 5000;
    private string status = "";


    // Start is called before the first frame update
    void Start()
    {
        // Should be like: "none;0,0,0;0,0,0;"
        //previousData = manager.getPose() + ";" + manager.getPosition() + ";" + manager.getRotation();

        OpenConnection();
    }

    public string GetStatus()
    {
        return status;
    }


    public void OpenConnection()
    {
        server = null;
        client = null;
        stream = null;

        status = ("Trying connection...");
        //host = GetLocalIPAddress();
        Debug.Log(host);


        new Thread(() =>
        {
            try
            {
                if (port == 5000)
                {
                    port = 8080;
                } else
                {
                    port = 5000;
                }

                server = new TcpListener(IPAddress.Any, port);
                server.Start();

                client = server.AcceptTcpClient();
                stream = client.GetStream();

                status = ("Client connected!");
                isConnected = true;
            }
            catch (System.Exception e)
            {
                status = e.ToString();
                OpenConnection();
                new WaitForSeconds(1);
            }
        }).Start(); // Start the Thread
    }

    public void SendMessageData(string message)
    {
        if (client != null && client.Connected)
        {
            byte[] encodedData = Encoding.ASCII.GetBytes(message);
            stream.Write(encodedData, 0, encodedData.Length);
            status = ("Message sent to client: " + message);
        } else
        {
            status = ("Client not connected!");
            isConnected = false;
        }
    }

    private void OnApplicationQuit()
    {
        SendMessageData("ded");
    }
}
