using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityHub : MonoBehaviour {


	public List<GameObject> CityNodeList;
	public int aNumberResearchStations;

	// Use this for initialization
	void Start () {

		CityNodeList.AddRange (GameObject.FindGameObjectsWithTag("CityNode"));

		
	}
	
	GameObject getCity(string pCity){
		
		GameObject bCity =  CityNodeList.Find (x => Equals(x.GetComponent<CityNode>().aCity.GetComponent<City>().getName(), pCity));
		return bCity;
	}
	GameObject getCity(GameObject pPawn){
		
		GameObject bCity = CityNodeList.Find (x => x.GetComponent<CityNode>().aPawns.Contains(pPawn));
		return bCity;
	}


	public void movePawn(GameObject pPawn, GameObject pCity){
		getCity(pPawn).GetComponent<CityNode>().aPawns.Remove(pPawn);
		pPawn.GetComponent<Transform> ().position = pCity.GetComponent<Transform>().position;
		getCity (pCity.GetComponent<City>().getName()).GetComponent<CityNode> ().aPawns.Add (pPawn);
	}

	public void dummyMove(){
		 GameObject p1 = GameObject.FindGameObjectWithTag("Player");
		GameObject d1 = getCity("Paris");

		movePawn (p1, d1);
	}

	public void dummyInitialize(){
		CityCard.initialize (CityNodeList);
		InfectionCard.initialize (CityNodeList);
		EpidemicCard.initialize (3);
	}

	public void dummyGet(){
		CityCard.get (CityNodeList [4]);
	}
		
}
