using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Web_Cam : MonoBehaviour
{

    WebCamTexture webCam;

     void Start()
    {
       webCam = new WebCamTexture();

        this.gameObject.GetComponent<RawImage>().texture = webCam;

        webCam.Play();
    }

    private void OnDestroy()
    {
        webCam.Stop();
    }
}
