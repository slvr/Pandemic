using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionCard : PlayerCard {

	public GameObject aCityNode;
	//private static List<GameObject> CityMap;
	public static GameObject[] InfectionCards;



	// Use this for initialization
	void Start () {
		//CityMap = GameObject.Find ("CityHub").GetComponent<CityHubClass> ().CityNodeList;

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public GameObject getCityNode(){
		return this.aCityNode;
	}

	public static void initialize(List<GameObject> CityMap){
		InfectionCards = new GameObject[CityMap.Count];
		foreach (GameObject City in CityMap) {
			GameObject newInfectionCard = new GameObject(City.name + "IC");
			newInfectionCard.AddComponent<CityCard> ();
			newInfectionCard.GetComponent<CityCard> ().aCityNode = City;

			InfectionCards [CityMap.IndexOf (City)] = newInfectionCard;
		}


	}
		
	public static GameObject get(GameObject pCityNode)
	{
		
		return InfectionCards[GameObject.Find ("CityHub").GetComponent<CityHub> ().CityNodeList.IndexOf (pCityNode)];
	}

}
