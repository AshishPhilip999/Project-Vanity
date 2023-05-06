using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Photon.Pun;

public class Video_Player : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private RenderTexture videotexture;

    PhotonView thisView;

    public GameObject videoplayerparent;
    public GameObject PlayButton;
    public GameObject PauseButton;

    public GameObject MuteButton;
    public GameObject UnMuteButton;

    public Text VideoTitle;

    [SerializeField]
    private Animator videoplayeranim;

    public VideoPlayer videoPlayer;
    public string videoUrl = "yourvideourl";

    private bool videoplayeron;
    private bool videoisplaying;

    private void Start()
    {
        videoPlayer.SetDirectAudioMute(0, true);
        thisView = GetComponent<PhotonView>();

        if(PhotonNetwork.IsMasterClient)
        {
            PlayButton.SetActive(true);
        }
        else
        {
            PlayButton.SetActive(false);
            PauseButton.SetActive(false);
        }
    }

    private void Update()
    {

        if(PhotonNetwork.IsMasterClient )
        {
            if(Input.GetKey("s"))
            {
                videoPlayer.time += 3.0;
            }

            thisView.RPC("SyncVideoPlayer" , RpcTarget.OthersBuffered , videoPlayer.time);
        }

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

    public void OnClickInstantiateVideo()
    {
        thisView.RPC("InstantiateVideo" , RpcTarget.AllBuffered , "Vanity Old Intro - By PieDom Studios" , "https://raw.githubusercontent.com/DataGramOfficial/VideoPlayer/main/Vanity%20Intro-1_Trim.mp4");
    }

    [PunRPC]
    public void InstantiateVideo(string VideoTitle , string VideoURL)
    {
        this.VideoTitle.text = VideoTitle;
        videoPlayer.url = VideoURL;
        PlayVideo();
    }

    public void OnClickPlayVideo()
    {
        thisView.RPC("PlayVideo", RpcTarget.AllBuffered);
    }

    public void OnClickPauseVideo()
    {
        thisView.RPC("PauseVideo", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void PlayVideo()
    {
        videoisplaying = true;
       if(PhotonNetwork.IsMasterClient)
        {
            PlayButton.SetActive(false);
            PauseButton.SetActive(true);
        }
        videoPlayer.Play();
    }

    [PunRPC]
    public void PauseVideo()
    {
        videoisplaying = false;
        if(PhotonNetwork.IsMasterClient)
        {
            PlayButton.SetActive(true);
            PauseButton.SetActive(false);
        }
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
    
    [PunRPC]
    public IEnumerator SyncVideoPlayer( double time )
    {
        videoPlayer.time = time;
        yield return new WaitForSeconds(3.0f);
    }
    

}
