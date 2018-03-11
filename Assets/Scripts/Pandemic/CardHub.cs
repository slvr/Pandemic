using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardHub : MonoBehaviour {

	public List<GameObject> aPlayerDeck;
	public List<GameObject> aPlayerDiscard;
	public List<GameObject> aInfectionDeck;
	public List<GameObject> aInfectionDiscard;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addPlayerDeck(GameObject pPlayerCard){
		aPlayerDeck.Add (pPlayerCard);
	}

	public GameObject removePlayerDeck(){
		GameObject firstCard = aPlayerDeck [0];
		aPlayerDeck.RemoveAt(0);
		return firstCard;
	}

	public void addInfectionDeck(GameObject pInfectionCard){
		aInfectionDeck.Add (pInfectionCard);
	}

	public GameObject removeInfectionDeck(){
		GameObject firstCard = aInfectionDeck [0];
		aInfectionDeck.RemoveAt (0);
		return firstCard;
	}

	public void addPlayerDiscard(GameObject pPlayerCard){
		aPlayerDiscard.Add (pPlayerCard);
	}

	public GameObject removePlayerDiscard(GameObject pPlayerCard){
		aPlayerDiscard.Remove (pPlayerCard);
		return pPlayerCard;
	}

	public void addInfectionDiscard(GameObject pInfectionCard){
		aInfectionDiscard.Add (pInfectionCard);
	}

	public GameObject removeInfectionDiscard(GameObject pInfectionCard){
		aInfectionDiscard.Remove (pInfectionCard);
		return pInfectionCard;
	}

	public void initializePlayerDeck(){
		aPlayerDeck.Clear ();
		 List<GameObject> CityMap = GameObject.Find ("CityHub").GetComponent<CityHub> ().CityNodeList;
		for (int i = 0; i < CityMap.Count; i++) {
			aPlayerDeck.Add (CityCard.get (CityMap [i]));
		}

		//TODO Add EventCards

		//Shuffling
		for (int i = 0; i < aPlayerDeck.Count; i++) {
			GameObject temp = aPlayerDeck[i];
			int randomIndex = Random.Range(i, aPlayerDeck.Count);
			aPlayerDeck[i] = aPlayerDeck[randomIndex];
			aPlayerDeck[randomIndex] = temp;
		}

		//Add Epidemic Card
		int x = aPlayerDeck.Count/EpidemicCard.numberOfEC();
		for (int i = 0; i < EpidemicCard.numberOfEC (); i++) {
			int y = Random.Range (i * x, (i + 1) * x);
			aPlayerDeck.Insert(y+i,EpidemicCard.get(i));
		}
	}

	public void initializeInfectionDeck(){
		aInfectionDeck.Clear ();
		List<GameObject> CityMap = GameObject.Find ("CityHub").GetComponent<CityHub> ().CityNodeList;
		for (int i = 0; i < CityMap.Count; i++) {
			aInfectionDeck.Add (InfectionCard.get (CityMap [i]));
		}

		//Shuffling
		for (int i = 0; i < aInfectionDeck.Count; i++) {
			GameObject temp = aInfectionDeck[i];
			int randomIndex = Random.Range(i, aInfectionDeck.Count);
			aInfectionDeck[i] = aInfectionDeck[randomIndex];
			aInfectionDeck[randomIndex] = temp;
		}
	}


}
