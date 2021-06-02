using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Lobby : MonoBehaviourPunCallbacks
{

    [Header("UI")] 
    [SerializeField] private GameObject roomListView;
    
    [Header("prefabs")] [SerializeField] private GameObject roomListPrefab;
    
    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> roomListEntries;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            Debug.Log("Room Info " + info.Name);
        }

        ClearRoomListView();

        UpdateCachedRoomList(roomList);
        UpdateRoomListView();
    }
    
    public void UpdateRoomListView()
    {
        foreach (RoomInfo info in cachedRoomList.Values)
        {
            if (!roomListEntries.ContainsKey(info.Name))
            {
                var room = Instantiate(roomListPrefab, roomListView.transform);
                room.GetComponent<LobbyRoomInfo>().Initialize(info.Name, (byte) info.PlayerCount, info.MaxPlayers);

                roomListEntries.Add(info.Name, room);
            }
        }
    }
    
    private void UpdateCachedRoomList(List<RoomInfo> roominfos)
    {
        foreach (RoomInfo info in roominfos)
        {
            if (!info.IsOpen || !info.IsVisible || info.RemovedFromList)
            {
                if (cachedRoomList.ContainsKey(info.Name))
                {
                    cachedRoomList.Remove(info.Name);
                }

                continue;
            }

            if (cachedRoomList.ContainsKey(info.Name))
            {
                cachedRoomList[info.Name] = info;
            }
            else
            {
                cachedRoomList.Add(info.Name, info);
            }
        }
    }
    
    private void ClearRoomListView()
    {
        foreach (GameObject room in roomListEntries.Values)
        {
            Destroy(room);
        }

        roomListEntries.Clear();
    }


    public void CreateButtonClicked()
    {
        var roomName = "Default RoomName";

        byte maxPlayer;
        byte.TryParse("2", out maxPlayer);
        
        RoomOptions options = new RoomOptions {MaxPlayers = maxPlayer, PlayerTtl = 10000, IsVisible = true};
        
        PhotonNetwork.CreateRoom(roomName, options, null);
    }
}
