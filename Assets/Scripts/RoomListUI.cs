using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListUI : MonoBehaviour
{
    [SerializeField] private LanListener lanListener;
    [SerializeField] private Transform contentParent; // o "Content" de um ScrollView
    [SerializeField] private GameObject roomButtonPrefab; // prefab com um Button + Text

    private void Start()
    {
        lanListener.StartListening();
        lanListener.OnRoomListUpdated += RefreshList;
    }

    private void RefreshList()
    {
        // Limpa lista atual
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        // Recria a lista com base nas salas descobertas
        foreach (var room in lanListener.DiscoveredRooms.Values)
        {
            GameObject buttonObj = Instantiate(roomButtonPrefab, contentParent);
            buttonObj.GetComponentInChildren<Text>().text = room.RoomName;

            string ip = room.IpAddress; // captura local pro closure funcionar certo
            buttonObj.GetComponent<Button>().onClick.AddListener(() => JoinRoom(ip));
        }
    }

    private void JoinRoom(string ipAddress)
    {
        var transport = Unity.Netcode.NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>();
        transport.SetConnectionData(ipAddress, 7777); // porta do jogo, não confundir com a porta de discovery

        lanListener.StopListening();
        Unity.Netcode.NetworkManager.Singleton.StartClient();
    }
}
