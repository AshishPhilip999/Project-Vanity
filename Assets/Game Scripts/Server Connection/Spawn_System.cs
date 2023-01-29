using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawn_System : MonoBehaviourPunCallbacks
{
    public GameObject Player;

    public GameObject Spawn_Point;

    private void Start()
    {
        PhotonNetwork.Instantiate(Player.name , Spawn_Point.transform.position , Quaternion.identity);
    }
}
