using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics2d : MonoBehaviour {

	private Rigidbody2D body;

	Vector2 velocity = new Vector2(0,0);
	Vector2 position = new Vector2(1,1);
	public Vector2 gravity;
	

	// Use this for initialization
	void Awake () {
		body = GetComponent<Rigidbody2D>();
		
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(body.didJump){
			
		}
	
		body.velocity += gravity * Time.deltaTime;
		position += velocity * Time.deltaTime;
	}
}
