using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionField : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D collider2D){
		print("Collided with: " + collider2D.name);
		/* if (collider2D.name != "GravField"){
			//print("collided with something");
			
		} */
		
	}
}
