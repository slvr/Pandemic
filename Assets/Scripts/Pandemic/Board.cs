using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {


	public int cardsToDraw;
	public GameObject[] aPawns;
	public GameObject aDiseaseHub;
	public GameObject aCardHub;
	public GameObject aCityHub;
	public GameObject aEventCardHub;
	public List<GameObject> aCurrentEvents;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject getCityHub(){
		return this.aCityHub;
	}

	public GameObject getCardHub(){
		return this.aCardHub;
	}

    public void GenerateCities() {

    }

    public void GenerateConnections() {

    }

    public IEnumerator highlightCity(int senderPlayer, bool turnOn, CityNode c) {
        //highlight ...
        while (EventTransferManager.instance.waitingForPlayer)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator highlightConnection(int senderPlayer, bool turnOn, Connection c){
        //highlight ...
        while (EventTransferManager.instance.waitingForPlayer)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public void EnableSave(){
        uiManager.saveButton.gameObject.SetActive(true);
    }



}
