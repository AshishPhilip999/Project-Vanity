using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Stream_Cam_To_Server : MonoBehaviourPunCallbacks
{
    public GameObject Player;
    public GameObject Content;

    // Start is called before the first frame update
    void Start()
    {
      PhotonNetwork.Instantiate(Player.name , new Vector2(0 , 0) , Quaternion.identity );
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

}
