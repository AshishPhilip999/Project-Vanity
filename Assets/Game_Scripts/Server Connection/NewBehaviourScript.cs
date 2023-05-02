using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NewBehaviourScript : MonoBehaviour
{
    public Text text;


    // Update is called once per frame
    void Update()
    {
        text.text = PhotonNetwork.CloudRegion;
    }
}
