using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Stream_Cam_To_Server : MonoBehaviourPunCallbacks
{
    public Canvas StreamModeUI;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
      GameObject camImage = PhotonNetwork.Instantiate(StreamModeUI.name , new Vector2(0 , 0) , Quaternion.identity );
    }
}
