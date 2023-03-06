using Photon.Pun;
using UnityEngine;

public class NetworkTest : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect Master...");
        PhotonNetwork.JoinOrCreateRoom("MetaFaker", new Photon.Realtime.RoomOptions { MaxPlayers = 6 }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connect....");
        PhotonNetwork.Instantiate("Prefabs/CarrotRPC", Vector3.zero, Quaternion.identity);
    }   
}
