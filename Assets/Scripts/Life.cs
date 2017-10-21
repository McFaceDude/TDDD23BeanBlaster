using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {
	GameObject camera;
	Vector3 cameraPosition;
	float offset;
	float pos_offset;
	float relativeScale;
	float heartRadius;

	// Use this for initialization
	void Awake () {
		heartRadius = transform.localScale.x * transform.GetComponent<CircleCollider2D>().radius;
		relativeScale = Camera.main.orthographicSize * 0.15f;
		offset = heartRadius * 25;
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
		heartRadius = transform.localScale.x * transform.GetComponent<CircleCollider2D>().radius;
		relativeScale = Camera.main.orthographicSize * 0.15f;
		offset = heartRadius * 25;
		
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(pos_offset, Screen.height - Screen.height/13, cameraPosition.z + 15));
		print("offset: " +offset + "height: "+ Screen.height + " width: " + Screen.width + " relation: " + ((float)Screen.width/(float)Screen.height));
		print("relativeScale: " + relativeScale);
		print(gameObject.name + "s position, x: " + pos_offset +" y: " +  (Screen.height - offset));
		transform.rotation = Camera.main.transform.rotation;
		transform.localScale = new Vector3(1,1,1) * relativeScale;
	}
}
