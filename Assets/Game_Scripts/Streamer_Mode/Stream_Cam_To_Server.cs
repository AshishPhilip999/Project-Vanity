using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Stream_Cam_To_Server : MonoBehaviourPunCallbacks
{
    public RawImage CamImage;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
      GameObject camImage = PhotonNetwork.Instantiate( CamImage.name , new Vector2(0 , 0) , Quaternion.identity );

        camImage.transform.parent = canvas.transform;
        camImage.transform.position = new Vector2(200 , 200 );
    }
}
