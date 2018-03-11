using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Pawn : MonoBehaviour {

	public Role aRole;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public Role getRole(){
		return this.aRole;
	}

}
