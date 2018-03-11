using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEngine : MonoBehaviour {

	public GamePhase aGamePhase;
	public List<Scenario> aScenarios;
	public Hashtable playerTurnOrder;
	public int aActionsLeft;
	public GameObject aBoard;
	public GameObject aDiseaseHandler;
	public GameObject aCardHandler;
	public GameObject aCityHandler;
	public GameObject currentPlayer;
	public Text currentPlayerText;

    public UIManager uiManager;


	// Use this for initialization
	void Start () {
		//test line
		currentPlayer = GameObject.Find ("P1");
		currentPlayer.GetComponent<Player> ().setMyTurn (true);
		currentPlayer.GetComponent<Player> ().updateDisplay ();
		setCurrentPlayerText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Drive(GameObject pCity, GameObject pPawn){
		
		aCityHandler.GetComponent<CityHandler>().Drive (aBoard, pCity, pPawn);
	}

	public void TakeDirectFlight(GameObject pPlayerCard, GameObject pPawn){


		aCityHandler.GetComponent<CityHandler> ().TakeDirectFlight (aBoard, pPlayerCard, pPawn);
	}
	public void dummyTakeFlight () {
		TakeDirectFlight (GameObject.Find ("New-YorkCC"), currentPlayer.GetComponent<Player> ().aPawn);
	}
	public void dummyInitialize() {
		Drive (GameObject.Find ("Tokyo"), GameObject.Find ("BluePawn"));
	}
	public void DummyDrive2() {
		Drive (GameObject.Find ("Shangai"), currentPlayer.GetComponent<Player>().aPawn);
	}

	void setCurrentPlayerText(){
		currentPlayerText.text = "Player Turn : " + currentPlayer.name;
	}

	void setCurrentPlayer(GameObject pCurrentPlayer){
		this.currentPlayer = pCurrentPlayer;
	}

	//this is a test for 2 players only
	public void endTurn(){
		if (currentPlayer.Equals (GameObject.Find ("P1"))) {
			currentPlayer.GetComponent<Player> ().setMyTurn (false);
			setCurrentPlayer (GameObject.Find ("P2"));
			currentPlayer.GetComponent<Player> ().setMyTurn (true);
		}
		else
		{
			currentPlayer.GetComponent<Player> ().setMyTurn (false);
			setCurrentPlayer (GameObject.Find ("P1"));
			currentPlayer.GetComponent<Player> ().setMyTurn (true);
		}
		

		currentPlayer.GetComponent<Player> ().setMyTurn (true);
		currentPlayer.GetComponent<Player> ().updateDisplay ();
		setCurrentPlayerText();

	}
}
