using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour {
	//Handles the tutorial icons and removes them after a set time.
	float timer = 11;
	
	// Update is called once per frame
	void FixedUpdate () {
		timer -= Time.deltaTime;
			if (timer < 0 ){
				Destroy(gameObject);
			}
	}
}
