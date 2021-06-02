using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatInit : MonoBehaviour
{
    public Text who;
    public Text message;

    public void initChat(string whoText, string messageText)
    {
        who.text = whoText;
        message.text = messageText;
    }
}
