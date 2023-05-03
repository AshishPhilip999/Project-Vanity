using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Voice;

public class Streamer_Settings_Menu : MonoBehaviourPunCallbacks
{
    public PhotonView thisView;

    public GameObject SelectedTop;

    public GameObject[] Contents;

    public bool streamer_settings_active = false;

    public Animator streamer_settings_anim;

    public GameObject videoPlayerIcon;
    public GameObject streamMic;

    private void Start()
    {
        DisableContents();
        Contents[0].SetActive(true);

        thisView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!streamer_settings_active)
                {
                    streamer_settings_active = true;

                    streamer_settings_anim.SetBool("isOpen", true);

                }
                else
                {
                    streamer_settings_anim.SetBool("isOpen", false);

                    streamer_settings_active = false;
                }
            }
        }

        
    }

    public void SelectMenu(GameObject button)
    {
        DisableContents();
        SelectedTop.transform.localPosition = new Vector3( button.transform.localPosition.x , 483.9f , 0 );


        GameObject SettingMenu = button.transform.GetChild(1).gameObject;

        SettingMenu.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void DisableContents()
    {
        foreach(GameObject content in Contents)
        {
            content.SetActive(false);
        }
    }

    public void EnableDisableVideoPlayer(Toggle useVideoPlayer)
    {
        thisView.RPC("EnableVideoPlayer" , RpcTarget.AllBuffered , useVideoPlayer.isOn);
    }


    [PunRPC]
    public void EnableVideoPlayer(bool useVideoPlayer)
    {
        if(useVideoPlayer)
        {
            videoPlayerIcon.SetActive(true);
        }
        else
        {
            videoPlayerIcon.SetActive(false);
        }
    }

    public void EnablePlayerMic(Toggle useMic)
    {
        
    }
}
