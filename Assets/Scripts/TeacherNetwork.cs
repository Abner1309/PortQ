using Unity.Netcode;
using UnityEngine;

public class TeacherNetwork : MonoBehaviour
{
    public void CreateVirtualClassroom()
    {
        if (NetworkManager.Singleton == null)
        {
            Debug.LogError("NetworkManager não encontrado na cena!");
            return;
        }
        
        Debug.Log("Host iniciado com sucesso.");
        
        /* bool started = NetworkManager.Singleton.StartHost();
        if (started)
        {
            Debug.Log("Host iniciado com sucesso. Aguardando conexões...");
        }
        else
        {
            Debug.LogError("Falha ao iniciar o host.");
        }*/
    }
}
