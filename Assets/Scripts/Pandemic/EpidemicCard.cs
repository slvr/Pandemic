using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpidemicCard : PlayerCard {

	public static GameObject[] EpidemicCards;
	int key;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void initialize(int numberOfEC){
		EpidemicCards = new GameObject[numberOfEC];

		for(int i =0; i < numberOfEC; i++) {
			GameObject newEpidemicCard = new GameObject("EpidemicCard"+i);
			newEpidemicCard.AddComponent<EpidemicCard> ();
			newEpidemicCard.GetComponent<EpidemicCard> ().key = i;

			EpidemicCards[i] = newEpidemicCard;
		}


	}


	public static GameObject get(int key)
	{

		return EpidemicCards[key];
	}

	public static int numberOfEC(){
		return EpidemicCards.Length;
	}
}
