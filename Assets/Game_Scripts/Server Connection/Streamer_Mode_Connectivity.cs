using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Streamer_Mode_Connectivity : MonoBehaviourPunCallbacks
{
    public void Create_Streamer_Mode()
    {
        PhotonNetwork.CreateRoom("Streamer Mode");
    }

    public void Join_Streamer_Mode()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        print("Joined Streamer Mode !");
        PhotonNetwork.LoadLevel("Streamer_Mode");
    }
}
