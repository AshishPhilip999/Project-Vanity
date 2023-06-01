using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Lobby_Queue_System : MonoBehaviour
{

    public GameObject[] PlayerPages;
    public Slider max_players;
    public GameObject LobbyContent;
    public GameObject QueueContent;

    public static List<Player> players = new List<Player>();

    List<Player> Lobby = new List<Player>();
    List<Player> Queue = new List<Player>();

    PhotonView thisView;

    private void Start()
    {
        thisView = GetComponent<PhotonView>();
    }

    private void Update()
    {

        if (!PhotonNetwork.IsMasterClient)
        {
            gameObject.SetActive(false);
        }

        PlayerPages = GameObject.FindGameObjectsWithTag("PlayerPage");
        SetPlayersInStream();
 


         thisView.RPC("AdjustLobbyandQueue" , RpcTarget.AllBuffered);
        
    }

    [PunRPC]
    public void AdjustLobbyandQueue()
    {
        Queue = new List<Player>();
        for(int i = (int)max_players.value + 1 ; i < players.Count; i++ )
        {
            Queue.Add(players[i]);
            PlayerPages[i].transform.SetParent(QueueContent.transform);
            PlayerPages[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }

    public void SetPlayersInStream()
    {
        players = new List<Player>();
        foreach(Player player in PhotonNetwork.PlayerListOthers)
        {
            players.Add(player);
        }
    }
}
