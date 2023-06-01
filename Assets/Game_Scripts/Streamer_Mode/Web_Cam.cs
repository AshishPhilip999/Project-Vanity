using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Web_Cam : MonoBehaviour 
{

    private PhotonView photonView;

    private WebCamTexture webcamTexture;

    public RawImage rawImage;

    private bool isMasterClient;

    private Texture2D texture;
    private byte[] bytes;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

        isMasterClient = PhotonNetwork.IsMasterClient;


        // Start the webcam
        webcamTexture = new WebCamTexture();
        webcamTexture.Play();
       
    }

    private void Update()
    {
      
      rawImage.texture = webcamTexture;
      SendWebcamTexture();

    }

    public void SendWebcamTexture()
    {
        if (!isMasterClient) return;

        if (texture == null || texture.width != webcamTexture.width || texture.height != webcamTexture.height)
        {
            texture = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGB24, false);
        }

        texture.SetPixels(webcamTexture.GetPixels());
        texture.Apply();

        bytes = texture.EncodeToJPG();
        photonView.RPC("ReceiveWebcamTexture", RpcTarget.Others, bytes);

    }

    [PunRPC]
    private void ReceiveWebcamTexture(byte[] webcamTextureBytes)
    {
        if (texture == null || texture.width != webcamTexture.width || texture.height != webcamTexture.height)
        {
            texture = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGB24, false);
        }

        texture.LoadImage(webcamTextureBytes);
        GetComponent<Renderer>().material.mainTexture = texture;
    }

    /*private byte[] EncodeTextureToJPG(WebCamTexture texture)
    {
        Texture2D tex = new Texture2D(texture.width, texture.height, TextureFormat.RGB24, false);
        tex.SetPixels(texture.GetPixels());
        tex.Apply();

        return tex.EncodeToJPG();
    }*/


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(bytes);
        }
        else
        {
            bytes = (byte[])stream.ReceiveNext();
        }
    }

    private void OnDestroy()
    {
        webcamTexture.Stop();
    }

   
}
