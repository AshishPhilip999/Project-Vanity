using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Web_Cam : MonoBehaviour
{

    public RawImage image;
    WebCamTexture webCam;

     void Start()
    {
       webCam = new WebCamTexture();

        image.texture = webCam;

        webCam.Play();
    }

    private void OnDestroy()
    {
        webCam.Stop();
    }
}
