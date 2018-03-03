using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using LitJson;
using System.Linq;
using Persistence;

public static class SaveJson {

    static FileStream fileStream;

    public static void saveJson(string fileToWrite) {
        pe_SavefileNames pe_saveFileNames = new pe_SavefileNames();
        string[] saveFileNames = LoadJson.loadSaveFileNames().pe_saveFileNames;
        string[] newSaveFileNames;
        if (saveFileNames.Contains(fileToWrite)){
            newSaveFileNames = saveFileNames;
        }
        else {
            newSaveFileNames = new string[saveFileNames.Length + 1];
            saveFileNames.CopyTo(newSaveFileNames, 0);
            newSaveFileNames[saveFileNames.Length] = fileToWrite;
        }
        pe_saveFileNames.pe_saveFileNames = newSaveFileNames;
        writeToJson(JsonMapper.ToJson(pe_saveFileNames), "saveFileIndex.json");

        pe_GameState gameState = new pe_GameState();
        gameState.eventTransferManager = saveEventTransferManager();
        gameState.players = savePlayers();
        gameState.board = saveBoard();
        gameState.playerCardStack = savePlayerCardStack();
        gameState.playerDiscardStack = savePlayerDiscardStack();
        gameState.infectionCardStack = saveInfectionCardStack();
        gameState.infectionDiscardStack = saveInfectionDiscardStack();
        string jsonResult = JsonMapper.ToJson(gameState);
        Debug.Log(jsonResult);
        writeToJson(jsonResult, fileToWrite + ".json");
    }

    public static pe_ETM saveEventTransferManager() {
        pe_ETM pe_etm = new pe_ETM();
        //EventTransferManager eventTransferManager = GameObject.FindGameObjectsWithTag("EventTransferManager").GetComponent<EventTransferManager>();
        //pe_etm.currentPlayerTurn = eventTransferManager.currentPlayerTurn;
        /* ... */
        return pe_etm;
    }

    public static pe_players savePlayers() {
        pe_players pe_players = new pe_players();
        /* ... */
        return pe_players;
    }

    public static pe_board saveBoard() {
        pe_board pe_board = new pe_board();
        /* ... */
        return pe_board;
    }

    public static pe_playerCardStack savePlayerCardStack() {
        pe_playerCardStack pe_pcs = new pe_playerCardStack();
        /* ... */
        return pe_pcs;
    }

    public static pe_playerDiscardStack savePlayerDiscardStack() {
        pe_playerDiscardStack pe_pds = new pe_playerDiscardStack();
        /* ... */
        return pe_pds;
    }

    public static pe_infectionCardStack saveInfectionCardStack() {
        pe_infectionCardStack pe_ics = new pe_infectionCardStack();
        /* ... */
        return pe_ics;
    }

    public static pe_infectionDiscardStack saveInfectionDiscardStack() {
        pe_infectionDiscardStack pe_ids = new pe_infectionDiscardStack();
        /* ... */
        return pe_ids;
    }

    public static void writeToJson(string jsonString, string filePath) {
        if (!File.Exists(filePath)) {
            fileStream = File.Create(filePath);
            fileStream.Close();
            fileStream.Dispose();
        }
        using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8)) {
            writer.WriteLine(jsonString);
        }
    }
}
