using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Open_Scene : MonoBehaviourPunCallbacks
{
    public void CreateStreamerMode()
    {
        PhotonNetwork.CreateRoom("The_room");
    }

    public void JoinStreamMode()
    {
        PhotonNetwork.JoinRoom("The_room");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Streamer_Mode");
    }
}
