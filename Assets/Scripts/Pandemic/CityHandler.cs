using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Drive(GameObject pBoard, GameObject pCity, GameObject pPawn){

		GameObject aCityHub = pBoard.GetComponent<Board> ().getCityHub ();
		aCityHub.GetComponent<CityHub> ().movePawn (pPawn, pCity);


	}

	public void TakeDirectFlight(GameObject pBoard, GameObject pPlayerCard, GameObject pPawn){
		
		GameObject aCityHub = pBoard.GetComponent<Board> ().getCityHub ();
		GameObject aCardHub = pBoard.GetComponent<Board> ().getCardHub ();
		GameObject destination = pPlayerCard.GetComponent<CityCard> ().getCityNode ().GetComponent<CityNode> ().getCity ();

		aCityHub.GetComponent<CityHub> ().movePawn (pPawn, destination);
		pPawn.GetComponent<aPawn> ().removeCard (pPlayerCard);
		aCardHub.GetComponent<CardHub> ().addPlayerDiscard (pPlayerCard);


	}
}
