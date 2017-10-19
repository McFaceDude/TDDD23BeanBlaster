using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {
	GameObject camera;
	Vector3 cameraPosition;
	float offset = 35;
	float pos_offset;

	// Use this for initialization
	void Awake () {
		
		camera = GameObject.FindGameObjectWithTag("MainCamera");
		cameraPosition = camera.transform.position;
		if(gameObject.name == "Heart_1"){
			pos_offset = offset;
		}
		else if(gameObject.name == "Heart_2"){
			pos_offset = offset * 2;
		}
		else if(gameObject.name == "Heart_3"){
			pos_offset = offset * 3;
		}
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float relativeScale = Camera.main.orthographicSize * 0.15f;
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(cameraPosition.x + pos_offset, Screen.height - 30, cameraPosition.z + 15));
		transform.rotation = Camera.main.transform.rotation;
		transform.localScale = new Vector3(1,1,1) * relativeScale;
	}
}
