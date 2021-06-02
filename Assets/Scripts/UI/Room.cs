using System;
using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("Join ");
        
        if (playerListEntries == null)
        {
            playerListEntries = new Dictionary<int, GameObject>();
        }
        UpdatePlayerList();
    }
    
    private void UpdatePlayerList()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject playerObject = Instantiate(playerListItem, playerList.transform);
            var listObject = playerObject.GetComponent<playerListItem>();
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
        Destroy(playerListEntries[otherPlayer.ActorNumber].gameObject);
        playerListEntries.Remove(otherPlayer.ActorNumber);

        foreach (var playerObject in playerListEntries.Values)
        {
            Destroy(playerObject);
        }

        playerListEntries.Clear();
        
        UpdatePlayerList();
    }

}
