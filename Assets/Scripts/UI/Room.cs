using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Room : MonoBehaviourPunCallbacks
{
    [SerializeField] private PanelSwitch panelSwitch;
    [SerializeField] private GameObject playerList;
    [SerializeField] private GameObject playerListItem;

    private Dictionary<int, GameObject> playerListEntries;

    private GameObject localPlayer;

    public override void OnJoinedRoom()
    {
        if (playerListEntries == null)
        {
            playerListEntries = new Dictionary<int, GameObject>();
        }

        UpdatePlayerList();
    }

    public override void OnLeftRoom()
    {
        panelSwitch.UnActivePanels(new[] {PanelSwitch.ROOM});
        panelSwitch.ActivePanel(PanelSwitch.LOBBY);

        if (playerListEntries != null)
        {
            foreach (var playerObject in playerListEntries.Values)
            {
                Debug.Log("Destroy " + playerObject.name);
                Destroy(playerObject);
            }

            playerListEntries.Clear();
        }
        playerListEntries = null;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player enter room " + newPlayer.NickName);
        GameObject entry = Instantiate(playerListItem, playerList.transform);

        var listObject = entry.GetComponent<PlayerListItem>();

        listObject.Initialize(newPlayer.NickName);

        if (playerListEntries.ContainsKey(newPlayer.ActorNumber))
        {
            playerListEntries.Remove(newPlayer.ActorNumber);
        }

        playerListEntries.Add(newPlayer.ActorNumber, entry);
    }

    private void UpdatePlayerList()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject playerObject = Instantiate(playerListItem, playerList.transform);
            var listObject = playerObject.GetComponent<PlayerListItem>();
            listObject.Initialize(p.NickName);

            if (localPlayer == null)
            {
                if (PhotonNetwork.LocalPlayer.NickName.Equals(p.NickName))
                {
                    localPlayer = playerObject;
                }
            }

            playerListEntries.Add(p.ActorNumber, playerObject);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player out " + otherPlayer.NickName);
        if (playerListEntries != null)
        {
            Destroy(playerListEntries[otherPlayer.ActorNumber].gameObject);
            playerListEntries.Remove(otherPlayer.ActorNumber);

            foreach (var playerObject in playerListEntries.Values)
            {
                Debug.Log("Destroy " + playerObject.name);
                Destroy(playerObject);
            }

            playerListEntries.Clear();
            UpdatePlayerList();
        }
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        var startFlag = propertiesThatChanged["Start"];
        if (startFlag != null)
        {
            PhotonNetwork.LoadLevel("SampleScene");
        }
    }

    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int index = 0;
            var list = PhotonNetwork.PlayerList;
            for (int i = 0; i < list.Length; i++)
            {
                list[i].SetCustomProperties(new Hashtable() {{"position", index++}});
            }

            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() {{"Start", true}});
        }
    }

    public void Cancel()
    {
        PhotonNetwork.LeaveRoom();
    }
}