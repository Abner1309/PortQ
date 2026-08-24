using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class LanBroadcaster : MonoBehaviour
{
    [SerializeField] private int broadcastPort = 47777;
    [SerializeField] private float broadcastInterval = 1f;
    [SerializeField] private string roomName = "Sala do Professor";

    private UdpClient udpClient;
    private float timer;
    private bool isBroadcasting;

    public void StartBroadcasting(string customRoomName)
    {
        roomName = customRoomName;
        udpClient = new UdpClient();
        udpClient.EnableBroadcast = true;
        isBroadcasting = true;
    }

    public void StopBroadcasting()
    {
        isBroadcasting = false;
        udpClient?.Close();
        udpClient = null;
    }

    private void Update()
    {
        if (!isBroadcasting) return;

        timer += Time.deltaTime;
        if (timer >= broadcastInterval)
        {
            timer = 0f;
            SendBroadcast();
        }
    }

    private void SendBroadcast()
    {
        try
        {
            string localIp = GetLocalIPAddress();
            string message = $"HOST|{roomName}|{localIp}";
            byte[] data = Encoding.UTF8.GetBytes(message);
            udpClient.Send(data, data.Length, new IPEndPoint(IPAddress.Broadcast, broadcastPort));
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Erro ao enviar broadcast: {e.Message}");
        }
    }

    private string GetLocalIPAddress()
    {
        foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
                return ip.ToString();
        }
        return "127.0.0.1";
    }

    private void OnDestroy()
    {
        StopBroadcasting();
    }
}
