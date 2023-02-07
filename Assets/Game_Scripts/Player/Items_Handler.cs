using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Photon.Pun;

public class Items_Handler : MonoBehaviour, IPunObservable
{
    public List<GameObject> Sources;

    public VolumeProfile volumeP;
    public Vignette vig;
    public ColorAdjustments Night_Vision;

    public bool TorchEquiped = true;
    public bool TorchOn = false;

    public bool NightvisionEquipped = true;
    public bool NightvisionOn = false;

    public Camera cam;

    public Light_System LightSources;
    public GameObject Torch_Light;
    public GameObject Night_Vision_Light;

    public PhotonView Player;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(TorchOn);
            stream.SendNext(cam.transform.rotation);
            Debug.Log("Status Shared");
        }
        else if(stream.IsReading)
        {
            bool isactive = (bool)stream.ReceiveNext();
            cam.transform.rotation = (Quaternion)stream.ReceiveNext();
            Torch_Light.SetActive(isactive);
            Debug.Log("Status recieved");
        }
    }

    private void Start()
    {

        Player = GetComponent<PhotonView>();

        volumeP = GameObject.FindGameObjectWithTag("Volume").GetComponent<Volume>().profile;
        vig = (Vignette)volumeP.components[0];
        Night_Vision = (ColorAdjustments)volumeP.components[4];

        LightSources = GameObject.FindGameObjectWithTag("LightSystem").GetComponent<Light_System>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Player.IsMine)
        {
            if (TorchEquiped)
            {
                if (Input.GetKeyDown("h"))
                {
                    if (!TorchOn)
                    {
                        Torch_Light.SetActive(true);
                        TorchOn = true;
                    }
                    else
                    {
                        Torch_Light.SetActive(false);
                        TorchOn = false;
                    }
                }
            }

            if (NightvisionEquipped)
            {
                if (Input.GetKeyDown("g"))
                {
                    if (!NightvisionOn)
                    {
                        vig.active = true;
                        Night_Vision.active = true;
                        NightvisionOn = true;
                        Night_Vision_Light.SetActive(true);
                    }
                    else
                    {
                        vig.active = false;
                        Night_Vision.active = false;
                        NightvisionOn = false;
                        Night_Vision_Light.SetActive(false);
                    }
                }
            }
        }
    }

}
