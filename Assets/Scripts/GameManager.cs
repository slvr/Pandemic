using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * The game manager is the core absolute of the game. It is instantiated on boot and persists regardless of which
 * scene is loaded.
 * NOTE: This is a singleton class
 * 
 * USAGE: Ensure this script is loaded in a bare-bones prefab. To initialize, attach Loader.cs script onto an 
 * absolute game object in a given scene (ex: the camera). Then attach the GameManager prefab onto said script.
 * 
 * SCENE INITIALIZATION: On initialization, GameManager will automatically load Scene 0 (Main Menu)
 * */

//all scenes
public enum Scenes {
    MainMenu,
    Loading,
    MainScene,
    Offline
};

public class GameManager : MonoBehaviour {

    //Game build
    private string BUILD = "Pandemic";

    //static single instance
    public static GameManager instance = null;

    /*  NETWORK  */
    public GameObject networkObject; //NetworkManager GameObject
    private NetworkManager network; //NetworkManager instance

    /*  LEVEL  */
    public GameObject levelObject;
    private LevelManager lm;

    /*  PERSISTENCE  */
    public bool LoadGameMode = false;
    public Persistence.pe_players pe_players;
    public Persistence.pe_board pe_board;
    public Persistence.pe_playerCardStack pe_playerCardStack;
    public Persistence.pe_playerDiscardStack pe_playerDiscardStack;
    public Persistence.pe_infectionCardStack pe_infectionCardStack;
    public Persistence.pe_infectionDiscardStack pe_infectionDiscardStack;
    public Persistence.pe_ETM pe_eventTransferManager;

    //Before Start
    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        initGame();
    }

    void initGame() {
        Debug.Log("GameManager.cs: Initializing Game...");
        Debug.Log(Scenes.MainMenu);
        SceneManager.LoadScene((int)Scenes.MainMenu); //Load Main Menu
    }

    //Initialize Network, to be called on play button
    public void initNetwork(int numPlayers) {
        if (NetworkManager.instance == null) {
            Instantiate(networkObject);
        }
        network = NetworkManager.instance;
        network.numPlayersSet = numPlayers - 1;
    }

    //Initialize level after initializing network
    public void initLevel(bool online) {
        if (LevelManager.instance == null) {
            Instantiate(levelObject);
        }
        lm = LevelManager.instance;
        if (LoadGameMode)
        {
            lm.LoadLevelSceneWithSaveFile(online, pe_players);
        }
        else {
            lm.LoadLevelScene(online);
        }
    }

    public string GetBuild() {
        return BUILD;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
