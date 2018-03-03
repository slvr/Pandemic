using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The Loader is a script which loads the GameManager.
 * Attach this to absolutegame object ONLY. Each scene should have one loader present.
 * 
 * 
 * */

public class Loader : MonoBehaviour {

    public GameObject gameManager;

    void Awake(){
        if (GameManager.instance == null) {
            Instantiate(gameManager);
        }
    }
}
