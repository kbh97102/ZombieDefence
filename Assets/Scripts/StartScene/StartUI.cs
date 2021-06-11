using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviourPunCallbacks
{
    public GameObject helpPanel;
    public Text helpText;

    const string playerNamePrefKey = "PlayerName";
    
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        helpText.text = OnClickHelp();


        if (PlayerPrefs.HasKey(playerNamePrefKey))
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString(playerNamePrefKey);
        }
        else
        {
            PlayerPrefs.SetString(playerNamePrefKey, "Default Player Name");
            PhotonNetwork.NickName = "Default Player Name";
        }
    }

    public void OnClickStartGame()
    {
        Debug.Log("Start Click");
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("IN");
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = "1";
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LoadLevel("Lobby");
        PhotonNetwork.JoinLobby();
    }

    public void OnClickHelpButton()
    {
        helpPanel.SetActive(true);
    }

    public string OnClickHelp()
    {
        return
            "* You can move WASD\n  Aiming and Shooting by Arrow Keys\n" +
            "* Press K -> stop bgm\n  Press L -> play bgm\n" +
            "* Survive from zombie and\n  Protect your core as longer as you can\n" +
            "* When you start a game\n  you can't get hp and ammo\n" +
            "* you can play With friend\n  or use random matching\n  or play alone\n" +
            "* You can make a room";
    }

    public void ExitProgram()
    {
        Application.Quit();
    }

    public void UnActiveHelpPanel()
    {
        helpPanel.SetActive(false);
    }
}