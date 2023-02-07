using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Movement : MonoBehaviour
{
    public PhotonView PlayerView;
    public CharacterController Player;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (PlayerView.IsMine)
        {
            float MouseX = Input.GetAxis("Mouse X");
            transform.Rotate(0, MouseX, 0);

            float MoveX = Input.GetAxis("Horizontal") * 2f * Time.deltaTime ;
            float MoveZ = Input.GetAxis("Vertical") * 2f  * Time.deltaTime;

            Vector3 Movement = new Vector3(-MoveX, 0, -MoveZ);
            Player.Move(transform.rotation * Movement);
        }
    }
}
