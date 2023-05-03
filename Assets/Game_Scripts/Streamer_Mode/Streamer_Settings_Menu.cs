using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice;

public class Streamer_Settings_Menu : MonoBehaviour
{
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
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!streamer_settings_active)
            {
                streamer_settings_active = true;

                streamer_settings_anim.SetBool("isOpen" , true);

            }else
            {
                streamer_settings_anim.SetBool("isOpen" , false);

                streamer_settings_active = false;
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

    public void EnableVideoPlayer(Toggle useVideoPlayer)
    {
        if(useVideoPlayer.isOn)
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
