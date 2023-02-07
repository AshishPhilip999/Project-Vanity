using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Camera_Movement : MonoBehaviour
{
    public PhotonView Cam;

    private void Start()
    {
        Cam = GetComponent<PhotonView>();
    }

     void Update()
    {
       
        if (Cam.IsMine)
        {
            float MouseY = Input.GetAxis("Mouse Y");
            // float MouseY = Input.GetAxis("MouseY");
            transform.Rotate(-MouseY, 0, 0);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
