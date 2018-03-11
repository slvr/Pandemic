using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Handles network instantiation and vigilance, initialized on call from GameManager
 * NOTE: This is a singleton
 * USAGE: Ensure NetworkManager is located in a bare-bones prefab. No loader is required
 * for NetworkManager, as GameManager handles this process.
 * SEE: GameManager.cs
 * */

public class NetworkManager : MonoBehaviour {

    public int numPlayersSet;
    public static NetworkManager instance = null;
    private readonly bool OFFLINEMODE = false;
    private GameManager gm = GameManager.instance;
    private PhotonPlayer otherPlayer;
    private bool isConnected = false;

    private void Awake(){
        if (instance == null){
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Debug.Log("NetworkManager.cs: Network initialization...");
        Connect();
    }

    //Connect to cloud
    void Connect(){
        PhotonNetwork.ConnectUsingSettings(gm.GetBuild());
    }

    //Display Photon connection details
    private void OnGUI(){
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    //Attempt to connect to client of random server
    void OnJoinedLobby() {
        Debug.Log("NetworkManager.cs: Attempting to join room...");
        PhotonNetwork.JoinRandomRoom();
    }

    //If no room exists, create a new room
    void OnPhotonRandomJoinFailed() {
        Debug.Log("NetworkManager.cs: Join Failed...");
        Debug.Log("NetworkManager.cs: Creating New Room...");
        PhotonNetwork.CreateRoom(null);
    }

    //If a player successfully joins a room
    void OnJoinedRoom() {
        Debug.Log("NetworkManager.cs: Room Joined...");
        OnStart();
    }

    //Upon connection
    void OnPhotonPlayerConnected(PhotonPlayer newPlayer) {
        OnStart();
    }

    //OnStart: occurs when all players have joined a room
    void OnStart() {
        PhotonPlayer[] players = PhotonNetwork.otherPlayers;
        if (players.Length != numPlayersSet) {
            Debug.Log("NetworkManager.cs: Player count insufficient...");
            return;
        }

        otherPlayer = players[0];
        Debug.Log("~~~GAME READY~~~");
        isConnected = true;
        PhotonNetwork.networkingPeer.SetReceivingEnabled(1, true);
        gm.initLevel(true);
    }

    public bool getConnectionStatus() {
        return isConnected;
    }
}