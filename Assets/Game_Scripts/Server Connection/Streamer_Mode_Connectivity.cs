using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Streamer_Mode_Connectivity : MonoBehaviourPunCallbacks
{
    public void Enter_Streamer_Mode()
    {

        if (!PhotonNetwork.IsConnected)
            return;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;

        PhotonNetwork.JoinOrCreateRoom("Streamer_Mode" , options , TypedLobby.Default );
    }

    public override void OnCreatedRoom()
    {
        print("Created Room Successfully ..");
    }

    public void Join_Streamer_Mode()
    {
        PhotonNetwork.JoinRoom("Streamer_Mode");
    }

    public override void OnJoinedRoom()
    {
        print("Joined Streamer_Mode !");
        PhotonNetwork.LoadLevel(4);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Room Creation failed ! " + message);
    }
}
