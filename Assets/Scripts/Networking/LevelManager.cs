using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

//Called at the start of the game to build everything needed
public class LevelManager : Photon.MonoBehaviour {

    public static LevelManager instance = null;

    public GameObject boardPrefab;
    public GameObject settingsPrefab;
    public GameObject pandemicPlayersPrefab;

    public List<Player> players;

    private GameBoard board;

    public int currentConnections = 0;

    private void Awake(){
        if (instance == null){
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Debug.Log("LevelManager.cs: Manager initialized");
    }

    public void LoadLevelScene(bool online) {
        Debug.Log("SceneManager.cs: Loading Scene...");
        if (online){
            PhotonNetwork.LoadLevel((int)Scenes.MainScene);
        }
        else {
            SceneManager.LoadScene((int)Scenes.MainScene);
        }

        //GameObject boardGameObject;
        //GameObject settingsGameObject;

        CreateBoard();
        addPlayers();

        //initiate player cards


        //initiate infection cards



        EventTransferManager[] extraInstances = GameObject.FindObjectsOfType<EventTransferManager>();
        for (int i = 0; i < extraInstances.Length; i++){
            Destroy(extraInstances[i]);
        }

        GameObject ETManagerGO = (GameObject)PhotonNetwork.Instantiate("EventTransferManager", Vector3.zero, Quaternion.identity, 0);
        EventTransferManager ETManager = ETManagerGO.GetComponent<EventTransferManager>();
        DontDestroyOnLoad(ETManagerGO);

        ETManager.OnReadyToPlay();
    }

    public void LoadLevelSceneWithSaveFile(bool online, Persistence.pe_players pe_players) {
        Debug.Log("SceneManager.cs: Loading scene from save...");
        if (online){
            PhotonNetwork.LoadLevel((int)Scenes.MainScene);
        }
        else {
            SceneManager.LoadScene((int)Scenes.MainMenu);
        }
        loadPlayers(pe_players);
        GameObject ETManagerGO = (GameObject)PhotonNetwork.Instantiate("EventTransferManager", Vector3.zero, Quaternion.identity, 0);
        EventTransferManager ETManager = ETManagerGO.GetComponent<EventTransferManager>();

        DontDestroyOnLoad(ETManagerGO);
        ETManager.OnReadyToPlay();
    }

    void CreateBoard() {
        //board.Generate(board.transform);
    }

    void addPlayers() {
/*        players = new List<Player>(4);
 *        
        GameObject player1Object = (GameObject)Instantiate(pandemicPlayerPrefab);
        Player player1 = player1Object.GetComponent<Player>();
        // Initiate player 1...
        player1.gameObject.tag = "Player";

        GameObject player2Object = (GameObject)Instantiate(pandemicPlayerPrefab);
        Player player2 = player2Object.GetComponent<Player>();
        // Initiate player 2...
        player2.gameObject.tag = "Player";

        GameObject player3Object = (GameObject)Instantiate(pandemicPlayerPrefab);
        Player player3 = player3Object.GetComponent<Player>();
        // Initiate player 3...
        player3.gameObject.tag = "Player";

        GameObject player4Object = (GameObject)Instantiate(pandemicPlayerPrefab);
        Player player4 = player4Object.GetComponent<Player>();
        // Initiate player 4...
        player4.gameObject.tag = "Player";

        players.Add(player1);
        players.Add(player2);
        players.Add(player3);
        players.Add(player4);

        DontDestroyOnLoad(player1);
        DontDestroyOnLoad(player2);
        DontDestroyOnLoad(player3);
        DontDestroyOnLoad(player4);*/
    }

    void loadPlayers(Persistence.pe_players pe_players) {
        /*     players = new List<Player>(4);
             GameObject player1Object = (GameObject)Instantiate(pandemicPlayerPrefab);
             Player player1 = player1Object.GetComponent<Player>();
             player1.playerName = pe_players.playerArray[0].playerName;
             // Load player 1...
             player1.gameObject.tag = "Player";

             GameObject player2Object = (GameObject)Instantiate(pandemicPlayerPrefab);
             Player player2 = player2Object.GetComponent<Player>();
             player2.playerName = pe_players.playerArray[1].playerName;
             // Load player 2...
             player2.gameObject.tag = "Player";

             GameObject player3Object = (GameObject)Instantiate(pandemicPlayerPrefab);
             Player player3 = player3Object.GetComponent<Player>();
             player3.playerName = pe_players.playerArray[2].playerName;
             // Load player 3...
             player3.gameObject.tag = "Player";

             GameObject player4Object = (GameObject)Instantiate(pandemicPlayerPrefab);
             Player player4 = player4Object.GetComponent<Player>();
             player4.playerName = pe_players.playerArray[3].playerName;
             // Load player 4...
             player4.gameObject.tag = "Player";

             players.Add(player1);
             players.Add(player2);
             players.Add(player3);
             players.Add(player4);

             DontDestroyOnLoad(player1);
             DontDestroyOnLoad(player2);
             DontDestroyOnLoad(player3);
             DontDestroyOnLoad(player4);*/
    }

    public int GetNewPlayerIndex() {
        return currentConnections++;
    }
}
