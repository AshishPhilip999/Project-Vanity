using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Set_Player_Username : MonoBehaviour
{
    public InputField Username;
    public GameObject MainMenu;
    public void Set_Player_Name(GameObject button)
    {
        PhotonNetwork.NickName = Username.text;
        PlayerPrefs.SetString("Username" , Username.text);
        MainMenu.SetActive(true);
        button.SetActive(false);
        Username.gameObject.SetActive(false);
    }
}
