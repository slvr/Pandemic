using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class EventTransferManager : Photon.MonoBehaviour {

    public static EventTransferManager instance = null;




    public int currentPlayerTurn = 0;

    /**
     * DECLARE GAMEOBJECT ATTRIBUTES
     * */

    /**
     * DECLARE TURN ATTRIBUTES
     * */

    /**
     * DECLARE PERSISTENCE ATTRIBUTES
     * */

    /**
     * DECLARE SETUP ATTRIBUTES
     * */
    public bool[] playerCheck;

    void Awake(){
        if (instance == null) {
            instance = this;
        }
        playerCheck = new bool[PhotonNetwork.playerList.Length];
    }

    // Update is called once per frame
    void Update () {
		
	}

    /**
     * METHODS FOR GENERAL USE AND CONNECTION
     * */
    public void OnReadyToPlay() {

    }

    public void OnEndTurn() {

    }

    public void OnEndGame() {

    }

    void GameOver() {

    }

    public void OnOperationFailure() {

    }

    /**
     * METHODS FOR FRESH GAME CREATION
     * */

    /**
     * METHODS FOR GAME CREATION FROM SAVE FILE
     * */

    /**
     * METHODS FOR GENERAL TURN ACTIONS
     * */

    /**
     * METHODS FOR SPECIALIZED TURN ACTIONS
     * */

    /**
     * METHODS FOR END OF TURN EVENTS
     * */

    /**
     * METHODS FOR EVENT CARDS
     * */

}

public enum MoveType {

}
