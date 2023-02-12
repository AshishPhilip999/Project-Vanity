using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Host_Join_Connection : MonoBehaviourPunCallbacks
{

    public InputField Host_Room;
    public InputField Join_Room;

    public void CreateMansion()
    {
        PhotonNetwork.CreateRoom("The_room");
    }

    public void JoinMansion()
    {
        PhotonNetwork.JoinRoom("The_room");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("The_Mansion");
    }

}
