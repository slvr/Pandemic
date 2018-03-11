using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class aPawn : Pawn {

	public bool aUniqueActionTaken;
	public Dictionary<Button,GameObject> display = new Dictionary<Button, GameObject>();
	public List<GameObject> aPlayerCards;
	public List<Button> aPlayerCardsDisplay;
	public int index=0;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void associateButtons(){
		for (int i = 0; i < aPlayerCards.Count; i++) {
			string y = "CC" + i.ToString ();
			Button x = GameObject.Find(y).GetComponent<Button>();



			display.Add (x, aPlayerCards [i]);
		}

		foreach(KeyValuePair<Button, GameObject> entry in display)
		{
			entry.Key.GetComponentInChildren<Text> ().text = entry.Value.name;
		}

	}

	/*
	public void associateButtons(){
		for (int i = 0; i < aPlayerCards.Count; i++) {
			
			string y = "CC" + i.ToString ();
			Button x = GameObject.Find(y).GetComponent<Button>();
			aPlayerCardsDisplay.Insert(i, x);
		}
		for(int i = 0 ; i < aPlayerCardsDisplay.Count; i++){
			aPlayerCardsDisplay [i].GetComponentInChildren<Text>().text = aPlayerCards [i].name;

		}

	}
*/
	public GameObject removeCard(GameObject pPlayerCard){
		aPlayerCards.Remove (pPlayerCard);
		return pPlayerCard;
	}

	public void addCard(GameObject pPlayerCard){
		aPlayerCards.Add (pPlayerCard);
	}
	/*
	public void clickButton(){
		GameObject pressedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

		Debug.Log (display.ContainsKey (pressedButton));

		GameObject pPlayerCard = display[pressedButton];

		GameObject GameEngine = GameObject.Find ("GameEngine");


		GameObject pPawn = GameEngine.GetComponent<GameEngineClass> ().currentPlayer.GetComponent<PlayerClass> ().aPawn;

		GameEngine.GetComponent<GameEngineClass> ().TakeDirectFlight (pPlayerCard, pPawn);

	}

*/
}
