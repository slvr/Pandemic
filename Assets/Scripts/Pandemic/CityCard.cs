using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCard : PlayerCard {

	public GameObject aCityNode;
    private static List<GameObject> CityMap;
	public static GameObject[] CityCards;

	// Use this for initialization
	void Start () {
		CityMap = GameObject.Find ("CityHub").GetComponent<CityHub> ().CityNodeList;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject getCityNode(){
		return this.aCityNode;
	}

	public static void initialize(List<GameObject> CityMap){
		CityCards = new GameObject[CityMap.Count];

		foreach (GameObject City in CityMap) {
			GameObject newCityCard = new GameObject(City.name + "CC");
			newCityCard.AddComponent<CityCard> ();
			newCityCard.GetComponent<CityCard> ().aCityNode = City;

			CityCards [CityMap.IndexOf (City)] = newCityCard;
		}


	}
		

	public static GameObject get(GameObject pCityNode)
	{
		
		return CityCards[CityMap.IndexOf(pCityNode)];
	}
		

}
