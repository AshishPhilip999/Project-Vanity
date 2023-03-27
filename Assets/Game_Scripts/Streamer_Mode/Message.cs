using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Message : MonoBehaviour
{
    public Text message;

     void Start()
    {
        GetComponent<RectTransform>().SetAsFirstSibling();
    }
}
