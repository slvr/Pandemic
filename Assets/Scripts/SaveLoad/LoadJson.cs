using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using LitJson;
using System;
using Persistence;

public static class LoadJson{

    static FileStream fileStream;

    public static pe_SavefileNames loadSaveFileNames() {
        pe_SavefileNames pe_saveFileNames = new pe_SavefileNames();
        pe_saveFileNames.pe_saveFileNames = new string[0];
        try
        {
            string jsonstr = string.Empty;
            using (StreamReader sr = new StreamReader("saveFileIndex.json", Encoding.UTF8, true))
            {
                while (!sr.EndOfStream)
                {
                    Debug.LogWarning("LOADING : " + (jsonstr += sr.ReadLine()));
                }
            }
            Debug.Log("LoadJson.cs: " + jsonstr);
            return JsonMapper.ToObject<pe_SavefileNames>(jsonstr);
        }
        catch (Exception e) {
            e.Message.ToString();
            return pe_saveFileNames;
        }
    }

    public static pe_GameState loadGameState() {
        try
        {
            string jsonstr = string.Empty;
            using (StreamReader sr = new StreamReader(Application.dataPath + "saveFile.json", Encoding.UTF8, true))
            {
                while (!sr.EndOfStream)
                {
                    Debug.LogWarning("LOADING : " + (jsonstr += sr.ReadLine()));
                }
            }
            Debug.Log("LoadJson.cs: " + jsonstr);
            return JsonMapper.ToObject<pe_GameState>(jsonstr);
        }
        catch (Exception e) {
            e.Message.ToString();
            return null;
        }
    }

    public static pe_GameState loadGameState(string fileName) {
        try
        {
            string jsonstr = string.Empty;
            using (StreamReader sr = new StreamReader(fileName + ".json", Encoding.UTF8, true))
            {
                while (!sr.EndOfStream)
                {
                    Debug.LogWarning("LOADING : " + (jsonstr += sr.ReadLine()));
                }
            }
            Debug.Log("LoadJson.cs: " + jsonstr);
            return JsonMapper.ToObject<pe_GameState>(jsonstr);
        }
        catch (Exception e) {
            e.Message.ToString();
            return null;
        }
    }

    public static pe_players loadPlayers() {
        return null;
    }
}
