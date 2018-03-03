using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Persistence;

public class Menu : MonoBehaviour {

    public Canvas MainCanvas;

    private GameManager gm = GameManager.instance;

    public void LoadOnline(int numPlayers) {
        Debug.Log("Menu.cs: Online button clicked...");
        gm.initNetwork(numPlayers);
        SceneManager.LoadScene((int)Scenes.Loading);
    }

    public void LoadSavedGame() {
        Debug.Log("Menu.cs: Load button clicked...");
        string selection = GameObject.FindGameObjectWithTag("FileSelection").GetComponent<SaveFileSelect>().getSaveSelection();
        pe_GameState gameState = LoadJson.loadGameState(selection);
        gm.initNetwork(gameState.players.total);
        gm.LoadGameMode = true;
        gm.pe_players = gameState.players;
        gm.pe_board = gameState.board;
        gm.pe_playerCardStack = gameState.playerCardStack;
        gm.pe_playerDiscardStack = gameState.playerDiscardStack;
        gm.pe_infectionCardStack = gameState.infectionCardStack;
        gm.pe_infectionDiscardStack = gameState.infectionDiscardStack;
        gm.pe_eventTransferManager = gameState.eventTransferManager;
        SceneManager.LoadScene((int)Scenes.Loading);
    }

    public void LoadOffline() {
        Debug.Log("Menu.cs: Offline button clicked...");
        gm.initLevel(false);
    }
}
