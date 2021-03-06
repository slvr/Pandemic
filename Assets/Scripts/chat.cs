﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon.Chat;
using UnityEngine.UI;

public class chat : MonoBehaviour,IChatClientListener {

    public ChatClient chatClient;
    public InputField playerName;
    string worldChat;
    public InputField msgInput;
    public Text msgArea;

    public GameObject MessagePanel;
    public GameObject LoginPanel;

	// Use this for initialization
	void Start () {
        Application.runInBackground = true;
        if (string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.ChatAppID)) {
            Debug.Log("chat.cs: No chat ID provided");
            return;
        }
        worldChat = "World";
        getConnected();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.chatClient == null){
            return;
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            sendMsg();
        }
        this.chatClient.Service();
    }

    public void getConnected() {
        Debug.Log("chat.cs: Trying to connect...");
        this.chatClient = new ChatClient(this);
        this.chatClient.Connect(PhotonNetwork.PhotonServerSettings.ChatAppID, "anything", new ExitGames.Client.Photon.Chat.AuthenticationValues(playerName.text));
    }

    public void OnConnected(){
        print("Connected");
        LoginPanel.SetActive(false);
        MessagePanel.SetActive(true);
        this.chatClient.Subscribe(new string[] { worldChat });
        this.chatClient.SetOnlineStatus(ChatUserStatus.Online);
    }

    public void sendMsg(){
        if (msgInput.text != "") {
            this.chatClient.PublishMessage(worldChat, msgInput.text);
        }
        msgInput.text = "";
    }

    public void OnDisconnected(){

    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages){
        for (int i = 0; i < senders.Length; i++) {
            msgArea.text += senders[i] + " : " + messages[i] + "\n";
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName){

    }

    public void OnSubscribed(string[] channels, bool[] results){
        this.chatClient.PublishMessage(worldChat, "joined");
    }

    public void OnUnsubscribed(string[] channels){

    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message){

    }

    public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message){

    }

    public void OnChatStateChange(ChatState state){

    }

    public void openPanel() {
        if (MessagePanel.activeSelf == true){
            MessagePanel.SetActive(false);
        }
        else {
            MessagePanel.SetActive(true);
        }
    }
}
