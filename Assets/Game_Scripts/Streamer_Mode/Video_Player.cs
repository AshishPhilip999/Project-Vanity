using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Video_Player : MonoBehaviour
{
    [SerializeField]
    private RenderTexture videotexture;


    public GameObject videoplayerparent;
    public GameObject PlayButton;
    public GameObject PauseButton;

    public GameObject MuteButton;
    public GameObject UnMuteButton;

    [SerializeField]
    private Animator videoplayeranim;

    public VideoPlayer videoPlayer;
    public string videoUrl = "yourvideourl";

    private bool videoplayeron;
    private bool videoisplaying;

    private void Start()
    {
        videoPlayer.SetDirectAudioMute(0, true);
    }
    public void OnClickVideoButton()
    {
        if(!videoplayeron)
        {
            OpenVideoPlayer();
        }else
        {
            CloseVideoPlayer();
        }
    }

/*    public void OnClickPlayPause(Button button)
    {
        if(videoisplaying)
        {
            PauseVideo(button);
        }else
        {
            PlayVideo(button);
        }
    }*/

    public void PlayVideo()
    {
        videoisplaying = true;
        PlayButton.SetActive(false);
        PauseButton.SetActive(true);
        videoPlayer.Play();
    }

    public void PauseVideo()
    {
        videoisplaying = false;
        PlayButton.SetActive(true);
        PauseButton.SetActive(false);
        videoPlayer.Pause();
    }

    public void MuteVideo()
    {
        MuteButton.SetActive(false);
        UnMuteButton.SetActive(true);
        videoPlayer.SetDirectAudioMute(0, true);
    }

    public void UnMuteVideo()
    {
        MuteButton.SetActive(true);
        UnMuteButton.SetActive(false);
        videoPlayer.SetDirectAudioMute(0, false);
    }

    private void OpenVideoPlayer()
    {
        videoplayeron = true;
        videoplayeranim.SetBool("isOn" , true);
    }

    private void CloseVideoPlayer()
    {
        videoplayeron = false;
        videoplayeranim.SetBool("isOn" , false);
    }
    


}
