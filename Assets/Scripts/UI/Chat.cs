using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Chat;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class Chat : MonoBehaviour, IChatClientListener
{

    [SerializeField] private GameObject chatList;
    [SerializeField] private GameObject chatItem;
    [SerializeField] private InputField inputField;

    private ChatClient chatClient;
    private string lobbyChannel = "lobby";
    private string currentChannel;
    private string userName;
    

    private void Start()
    {
        Application.runInBackground = true;
        userName = PhotonNetwork.LocalPlayer.NickName;
        currentChannel = lobbyChannel;
        ConnectToChatServer();
    }

    private void Update()
    {
        if (chatClient != null)
        {
            chatClient.Service();
        }
    }
    
    private void OnApplicationQuit()
    {
        chatClient.Disconnect();
    }
    
    private void ConnectToChatServer()
    {
        var settings = PhotonNetwork.PhotonServerSettings.AppSettings.GetChatSettings();
        chatClient = new ChatClient(this);
        chatClient.UseBackgroundWorkerForSending = true;
        chatClient.AuthValues = new AuthenticationValues(userName);
        chatClient.ConnectUsingSettings(settings);
    }
    
    
    private void AddMessage(string who,string message)
    {
        Debug.Log(message);
        var conversationObject = Instantiate(chatItem, chatList.transform);
        conversationObject.GetComponent<ChatInit>().initChat(who, message);
    }
    
    public void OnSendMessage()
    {
        var message = inputField.text;
        inputField.text = "";
        chatClient.PublishMessage(currentChannel, message);
        inputField.Select();
        inputField.ActivateInputField();
    }

    public void ConnectToRoomChat(string chatRoom)
    {
        chatClient.Unsubscribe(new string[]{lobbyChannel});
        chatClient.Subscribe(new string[] {chatRoom});
        currentChannel = chatRoom;
    }
    
    public void ConnectToLobby()
    {
        string[] channels = new string[chatClient.PublicChannels.Count];
        var index = 0;
        foreach (var channel in chatClient.PublicChannels.Values)
        {
            channels[index++] = channel.Name;
        }

        currentChannel = lobbyChannel;
        chatClient.Unsubscribe(channels);
        chatClient.Subscribe(new string[] {lobbyChannel});
    }
    
    #region Chat Methods

    public void DebugReturn(DebugLevel level, string message)
    {
    }

    public void OnDisconnected()
    {
        Debug.Log("Chat Disconnected");
    }

    public void OnConnected()
    {
        chatClient.Subscribe(new string[] {lobbyChannel});
    }

    public void OnChatStateChange(ChatState state)
    {
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        if (channelName.Equals(currentChannel))
        {
            AddMessage(senders[0], messages[0]+"");
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        foreach (var channel in channels)
        {
            var hi = userName + " is connected to chat channel : "+channel;
            chatClient.PublishMessage(channel, hi);
        }
    }

    public void OnUnsubscribed(string[] channels)
    {
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
    }

    public void OnUserSubscribed(string channel, string user)
    {
        Debug.Log("user "+user+" channel "+channel);
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
    }

    #endregion
    
   
}
