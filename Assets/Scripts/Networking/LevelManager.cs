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

        //GameObject boardGO;
        //GameObject settingsGO;

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
        GameObject player1Object = (GameObject)Instantiate(catanPlayerPrefab);
        Player player1 = player1Object.GetComponent<Player>();
        player1.playerColor = Color.blue;
        player1.playerName = "Nehir";
        player1.playerNumber = 1;
        player1.goldCoins = 2;
        player1.avatar = Resources.Load<Sprite>("avatars/charlie-chaplin");
        player1.gameObject.tag = "Player";

        GameObject player2Object = (GameObject)Instantiate(catanPlayerPrefab);
        Player player2 = player2Object.GetComponent<Player>();
        player2.playerColor = Color.red;
        player2.playerName = "Angela";
        player2.playerNumber = 2;
        player2.goldCoins = 2;
        player2.avatar = Resources.Load<Sprite>("avatars/steve-jobs");
        player2.gameObject.tag = "Player";

        GameObject player3Object = (GameObject)Instantiate(catanPlayerPrefab);
        Player player3 = player3Object.GetComponent<Player>();
        player3.playerColor = Color.yellow;
        player3.playerName = "Milosz";
        player3.playerNumber = 3;
        player3.goldCoins = 2;
        player3.avatar = Resources.Load<Sprite>("avatars/barack-obama");
        player3.gameObject.tag = "Player";

        GameObject player4Object = (GameObject)Instantiate(catanPlayerPrefab);
        Player player4 = player4Object.GetComponent<Player>();
        player4.playerColor = Color.green;
        player4.playerName = "Carl";
        player4.playerNumber = 4;
        player4.avatar = Resources.Load<Sprite>("avatars/che-guevara");
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
        GameObject player1Object = (GameObject)Instantiate(catanPlayerPrefab);
        Player player1 = player1Object.GetComponent<Player>();
        player1.playerName = pe_players.playerArray[0].playerName;
        player1.playerColor = Color.blue;
        player1.playerNumber = pe_players.playerArray[0].playerNumber;
        player1.goldCoins = pe_players.playerArray[0].goldCoins;
        player1.victoryPoints = pe_players.playerArray[0].victoryPoints;
        player1.assets = new AssetTuple(pe_players.playerArray[0].assets[0],
                                        pe_players.playerArray[0].assets[1],
                                        pe_players.playerArray[0].assets[2],
                                        pe_players.playerArray[0].assets[3],
                                        pe_players.playerArray[0].assets[4],
                                        pe_players.playerArray[0].assets[5],
                                        pe_players.playerArray[0].assets[6],
                                        pe_players.playerArray[0].assets[7],
                                        pe_players.playerArray[0].assets[8],
                                        pe_players.playerArray[0].assets[9],
            pe_players.playerArray[0].assets[10],
            pe_players.playerArray[3].assets[11],
            pe_players.playerArray[3].assets[12]);
        player1.avatar = Resources.Load<Sprite>("avatars/" + pe_players.playerArray[0].avatar);
        player1.progressCards = new List<ProgressCardType>(pe_players.playerArray[0].progressCards.Select(x => (ProgressCardType)x));

        player1.gameObject.tag = "Player";

        GameObject player2Object = (GameObject)Instantiate(catanPlayerPrefab);
        Player player2 = player2Object.GetComponent<Player>();
        player2.playerName = pe_players.playerArray[1].playerName;
        player2.playerColor = Color.red;
        player2.playerNumber = pe_players.playerArray[1].playerNumber;
        player2.goldCoins = pe_players.playerArray[1].goldCoins;
        player2.victoryPoints = pe_players.playerArray[1].victoryPoints;
        player2.assets = new AssetTuple(pe_players.playerArray[1].assets[0],
            pe_players.playerArray[1].assets[1],
            pe_players.playerArray[1].assets[2],
            pe_players.playerArray[1].assets[3],
            pe_players.playerArray[1].assets[4],
            pe_players.playerArray[1].assets[5],
            pe_players.playerArray[1].assets[6],
            pe_players.playerArray[1].assets[7],
            pe_players.playerArray[1].assets[8],
            pe_players.playerArray[1].assets[9],
            pe_players.playerArray[1].assets[10],
            pe_players.playerArray[3].assets[11],
            pe_players.playerArray[3].assets[12]);
        player2.avatar = Resources.Load<Sprite>("avatars/" + pe_players.playerArray[1].avatar);
        player2.progressCards = new List<ProgressCardType>(pe_players.playerArray[1].progressCards.Select(x => (ProgressCardType)x));
        player2.gameObject.tag = "Player";

        GameObject player3Object = (GameObject)Instantiate(catanPlayerPrefab);
        Player player3 = player3Object.GetComponent<Player>();
        player3.playerName = pe_players.playerArray[2].playerName;
        player3.playerColor = Color.yellow;
        player3.playerNumber = pe_players.playerArray[2].playerNumber;
        player3.goldCoins = pe_players.playerArray[2].goldCoins;
        player3.victoryPoints = pe_players.playerArray[2].victoryPoints;
        player3.assets = new AssetTuple(pe_players.playerArray[2].assets[0],
            pe_players.playerArray[2].assets[1],
            pe_players.playerArray[2].assets[2],
            pe_players.playerArray[2].assets[3],
            pe_players.playerArray[2].assets[4],
            pe_players.playerArray[2].assets[5],
            pe_players.playerArray[2].assets[6],
            pe_players.playerArray[2].assets[7],
            pe_players.playerArray[2].assets[8],
            pe_players.playerArray[2].assets[9],
            pe_players.playerArray[2].assets[10],
            pe_players.playerArray[3].assets[11],
            pe_players.playerArray[3].assets[12]);
        player3.avatar = Resources.Load<Sprite>("avatars/" + pe_players.playerArray[2].avatar);
        player3.progressCards = new List<ProgressCardType>(pe_players.playerArray[2].progressCards.Select(x => (ProgressCardType)x));
        player3.gameObject.tag = "Player";

        GameObject player4Object = (GameObject)Instantiate(catanPlayerPrefab);
        Player player4 = player4Object.GetComponent<Player>();
        player4.playerName = pe_players.playerArray[3].playerName;
        player4.playerColor = Color.green;
        player4.playerNumber = pe_players.playerArray[3].playerNumber;
        player4.goldCoins = pe_players.playerArray[3].goldCoins;
        player4.victoryPoints = pe_players.playerArray[3].victoryPoints;
        player4.assets = new AssetTuple(pe_players.playerArray[3].assets[0],
            pe_players.playerArray[3].assets[1],
            pe_players.playerArray[3].assets[2],
            pe_players.playerArray[3].assets[3],
            pe_players.playerArray[3].assets[4],
            pe_players.playerArray[3].assets[5],
            pe_players.playerArray[3].assets[6],
            pe_players.playerArray[3].assets[7],
            pe_players.playerArray[3].assets[8],
            pe_players.playerArray[3].assets[9],
            pe_players.playerArray[3].assets[10],
            pe_players.playerArray[3].assets[11],
            pe_players.playerArray[3].assets[12]
        );
        player4.avatar = Resources.Load<Sprite>("avatars/" + pe_players.playerArray[3].avatar);
        player4.progressCards = new List<ProgressCardType>(pe_players.playerArray[3].progressCards.Select(x => (ProgressCardType)x));
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
