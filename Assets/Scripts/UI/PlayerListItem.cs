using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviour
{
    public Text playerNameText;

    public void Initialize(string name)
    {
        playerNameText.text = name;
    }
}
