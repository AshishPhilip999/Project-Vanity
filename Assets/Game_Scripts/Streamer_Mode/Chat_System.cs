using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Chat_System : MonoBehaviourPunCallbacks
{
    public InputField ChatBox;
    public GameObject Message;
    public GameObject Content;

    public void Send_Message()
    {
        GetComponent<PhotonView>().RPC("Get_Message" , RpcTarget.All , ChatBox.text);
    }

    [PunRPC]
    public void Get_Message(string RecievedMessage)
    {
      GameObject CurrMessage =  Instantiate(Message , Vector3.zero , Quaternion.identity , Content.transform);
        CurrMessage.GetComponent<Message>().message.text = RecievedMessage;
    }
}
