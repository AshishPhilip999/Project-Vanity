using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player_Join : MonoBehaviour
{
    public GameObject PlayerPage;

    void Start()
    {
        GetComponent<PhotonView>().RPC("AddPlayer" , RpcTarget.OthersBuffered );
    }


    [PunRPC]
    public void AddPlayer()
    {
        Instantiate(PlayerPage , Vector2.zero , Quaternion.identity , GameObject.FindGameObjectWithTag("DefaultCanvas").transform );
    }
}
