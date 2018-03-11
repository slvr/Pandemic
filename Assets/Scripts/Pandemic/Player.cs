using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public string aPlayerName;
	public string aPassword;
	public bool isHost;
	public PlayerStatus aPlayerStatus;
	public string aPlayerID;
	public GameObject aPawn;
	public bool isMyTurn;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void scanPossibleMoves(){
		if (!isMyTurn) {
			//Show EventCards moves
		}
		else {
		//Show adjacent cities to drive to
		//Show cities associated to PlayerCards where TakeDirectFlight is possible

		}
			
	}

	public void setMyTurn(bool myTurn){
		isMyTurn = myTurn;
	}

	public void updateDisplay() {
		aPawn.GetComponent<aPawn> ().associateButtons ();
	}

    public CityNode getLocation() {
        return null;
    }


}
