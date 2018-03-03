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

        GameObject boardGO;
        GameObject settingsGO;

        addPlayers();

        //initiate player cards


        //initiate infection cards
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

    //HideBoard????

    void addPlayers() {
        //???
    }

    void loadPlayers(Persistence.pe_players pe_players) {
        //???
    }

    public int GetNewPlayerIndex() {
        return currentConnections++;
    }
}
