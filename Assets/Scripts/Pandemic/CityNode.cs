using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityNode : MonoBehaviour {

	public GameObject aCity;
	public Dictionary<Colour, int> aDiseases = new Dictionary<Colour, int>();
	public bool hasResearchStation;
	public List<GameObject> aConnections;
	public List<GameObject> aPawns;
    

    // Use this for initialization
    void Start () {
		hasResearchStation = false;
        aDiseases.Add(Colour.BLACK, 0);
        aDiseases.Add(Colour.BLUE, 0);
        aDiseases.Add(Colour.RED, 0);
        aDiseases.Add(Colour.YELLOW, 0);
        aDiseases.Add(Colour.PURPLE, 0);
        posi = this.transform.position;
       // Debug.Log(aCity.name + " " + posi);

    }
	
	// Update is called once per frame
	void Update () {

	}


    public string textToShow = "Test";
    public Vector2 posi;
  /*  private void OnGUI()
    {

        Vector3 getPixelPos = Camera.main.WorldToScreenPoint(this.transform.position);
        getPixelPos.y = Screen.height - getPixelPos.y;
        GUI.Label(new Rect(getPixelPos.x, getPixelPos.y, 200f, 100f), "It's Me, Mario! :D");
    }
*/

	public GameObject getCity(){
		return this.aCity;
	}

}
