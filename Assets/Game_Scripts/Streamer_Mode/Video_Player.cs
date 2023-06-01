using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Photon.Pun;

public class Video_Player : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public static bool VideoQueuePanelOn;


    private double syncmargin = 3.0;

    [SerializeField]
    private RenderTexture videotexture;

    PhotonView thisView;

    public GameObject videoplayerparent;
    public GameObject PlayButton;
    public GameObject PauseButton;

    public GameObject MuteButton;
    public GameObject UnMuteButton;
    public GameObject AddToQueuePanel;

    public Text VideoTitle;
    public InputField URLInput;

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
            PlayButton.GetComponent<Button>().interactable = false;
            PauseButton.GetComponent<Button>().interactable = false;
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

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(VideoQueuePanelOn)
            {
                AddToQueueWaive();
            }
        }

    }


    public void AddToQueueInvoke()
    {
        AddToQueuePanel.SetActive(true);
        VideoQueuePanelOn = true;
    }

    public void AddToQueueWaive()
    {
        AddToQueuePanel.SetActive(false);
        VideoQueuePanelOn = false;
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
        thisView.RPC("InstantiateVideo" , RpcTarget.AllBuffered , "Vanity Old Intro - By PieDom Studios" , URLInput.text);
        AddToQueueWaive();

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

        PlayButton.SetActive(false);
        PauseButton.SetActive(true);
        videoPlayer.Play();
    }

    [PunRPC]
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
    
    [PunRPC]
    public void SyncVideoPlayer( double time )
    {
        double currsynctime = videoPlayer.time - time;

        if( currsynctime > syncmargin || currsynctime  < (-syncmargin) )
        {
            videoPlayer.time = time;
        }
    }
    

}
