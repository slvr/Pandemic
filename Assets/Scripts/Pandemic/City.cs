using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {

	public string aName;
	public int aPopulation;
	public Colour aColor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string getName(){
		return (aName);
	}
	public int getPopulation(){
		return (aPopulation);
	}
	public Colour getColor(){
		return (aColor);
	}
}
