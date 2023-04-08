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

    public bool isStreaming;

    public bool isSending;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

        isMasterClient = PhotonNetwork.IsMasterClient;

        if (isMasterClient)
        {
            // Start the webcam
            webcamTexture = new WebCamTexture();
            webcamTexture.Play();
        }
    }

    private void Update()
    {
        if (!isMasterClient)
        {
            return;
        }

        // Update the texture
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            rawImage.texture = webcamTexture;
        }

        if(isStreaming)
        {
            if(isSending)
            {
                SendWebcamTexture();
            }
        }
    }

    public void SendWebcamTexture()
    {
        isStreaming = true;

        isSending = false;
        if (!isMasterClient)
        {
            Debug.Log("Only the master client can start the webcam texture transfer.");
            return;
        }

        if (webcamTexture == null || !webcamTexture.isPlaying)
        {
            Debug.Log("Webcam texture is not available.");
            return;
        }

        // Encode the texture and send it through an RPC
        byte[] bytes = EncodeTextureToJPG(webcamTexture);
        photonView.RPC("ReceiveWebcamTexture", RpcTarget.Others, bytes);

        isSending = true;
    }

    [PunRPC]
    private void ReceiveWebcamTexture(byte[] bytes)
    {
        // Decode the received bytes into a texture and assign it to the RawImage
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);
        rawImage.texture = texture;
    }

    private byte[] EncodeTextureToJPG(WebCamTexture texture)
    {
        Texture2D tex = new Texture2D(texture.width, texture.height, TextureFormat.RGB24, false);
        tex.SetPixels(texture.GetPixels());
        tex.Apply();

        return tex.EncodeToJPG();
    }

    private void OnDestroy()
    {
        webcamTexture.Stop();
    }

   
}
