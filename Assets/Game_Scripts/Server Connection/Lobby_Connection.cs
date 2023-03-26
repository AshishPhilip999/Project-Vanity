using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class Lobby_Connection : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
     void Start()
    {
        print("Connecting to server ...");    
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to server !");
        PhotonNetwork.LoadLevel(2);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Discconnected from server due to : " + cause.ToString());
    }
}
