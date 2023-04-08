using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Lobby_Queue_Manager : MonoBehaviourPunCallbacks
{
    public GameObject Player;
    public GameObject Content;

    public GameObject Playbutton;

    // Start is called before the first frame update
    void Start()
    {
      PhotonNetwork.Instantiate(Player.name , new Vector2(0 , 0) , Quaternion.identity );

        if(PhotonNetwork.IsMasterClient)
        {
            Playbutton.SetActive(true);
        }
    }

     void Update()
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag("PlayerPage");

        foreach(GameObject player in Players)
        {
            if(player.transform.parent == null)
            {
                player.transform.SetParent(Content.transform);
                player.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
                player.transform.GetChild(2).GetComponent<Text>().text = player.GetComponent<PhotonView>().Owner.NickName;
            }
        }
    }

    public void Play_Lobby()
    {
        photonView.RPC("LoadMansion", RpcTarget.All);
    }

    [PunRPC]
    public void LoadMansion()
    {
        PhotonNetwork.LoadLevel("The_Mansion");
    }

}
