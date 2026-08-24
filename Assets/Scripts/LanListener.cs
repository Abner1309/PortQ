using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class RoomInfo
{
    public string RoomName;
    public string IpAddress;
    public float LastSeenTime;
}

public class LanListener : MonoBehaviour
{
    [SerializeField] private int listenPort = 47777;

    private UdpClient udpClient;
    private bool isListening;

    public Dictionary<string, RoomInfo> DiscoveredRooms { get; private set; } = new Dictionary<string, RoomInfo>();

    public event Action OnRoomListUpdated;

    public void StartListening()
    {
        if (isListening) return;

        udpClient = new UdpClient(listenPort);
        isListening = true;
        DiscoveredRooms.Clear();
        udpClient.BeginReceive(OnDataReceived, null);
    }

    public void StopListening()
    {
        isListening = false;
        udpClient?.Close();
        udpClient = null;
    }

    private void OnDataReceived(IAsyncResult result)
    {
        if (!isListening || udpClient == null) return;

        try
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = udpClient.EndReceive(result, ref remoteEndPoint);
            string message = Encoding.UTF8.GetString(data);

            string[] parts = message.Split('|');
            if (parts.Length == 3 && parts[0] == "HOST")
            {
                string roomName = parts[1];
                string ip = parts[2];

                DiscoveredRooms[ip] = new RoomInfo
                {
                    RoomName = roomName,
                    IpAddress = ip,
                    LastSeenTime = Time.time
                };

                OnRoomListUpdated?.Invoke();
            }

            udpClient.BeginReceive(OnDataReceived, null);
        }
        catch (ObjectDisposedException)
        {
            // socket foi fechado, ignora
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Erro ao receber broadcast: {e.Message}");
        }
    }

    private void Update()
    {
        // Remove salas que não enviam broadcast há mais de 5 segundos (host caiu)
        List<string> toRemove = new List<string>();
        foreach (var kvp in DiscoveredRooms)
        {
            if (Time.time - kvp.Value.LastSeenTime > 5f)
                toRemove.Add(kvp.Key);
        }

        if (toRemove.Count > 0)
        {
            foreach (string key in toRemove)
                DiscoveredRooms.Remove(key);

            OnRoomListUpdated?.Invoke();
        }
    }

    private void OnDestroy()
    {
        StopListening();
    }
}
