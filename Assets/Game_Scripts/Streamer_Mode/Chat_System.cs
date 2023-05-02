using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.Collections;

public class Chat_System : MonoBehaviourPunCallbacks
{
    public InputField ChatBox;
    public GameObject Message;
    public GameObject Content;
    public GameObject ChatboxParent;

    private bool chatboxon;
    public bool isSelected;

    public ScrollRect scrollrect;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(isSelected)
            {
                if(ChatBox.text != "")
                {
                    Send_Message();
                }
            }
        }
    }


    public void IsSelected()
    {
        isSelected = true;
    }

    public void IsDeSelected()
    {
        isSelected = false;
    }

    public void Send_Message()
    {
        GetComponent<PhotonView>().RPC("Get_Message" , RpcTarget.All , (PhotonNetwork.NickName + " : " + ChatBox.text) );
        ChatBox.text = "";
    }

    [PunRPC]
    public void Get_Message(string RecievedMessage)
    {
      GameObject CurrMessage =  Instantiate(Message , Vector3.zero , Quaternion.identity , Content.transform);
      CurrMessage.GetComponent<Message>().message.text = RecievedMessage;
    }

    public void OnChatButtonClick()
    {
        if (chatboxon)
        {
            CloseChatBox();
        }
        else
        {
            OpenChatBox();
        }
    }

    private void OpenChatBox()
    {
        chatboxon = true;
        ChatboxParent.SetActive(true);
    }

    private void CloseChatBox()
    {
        chatboxon = false;
        ChatboxParent.SetActive(false);
    }

}
