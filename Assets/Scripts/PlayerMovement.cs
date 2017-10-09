using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Handles the input by the user
public class PlayerMovement : MonoBehaviour {
	bool inGravField;
	public Transform TargetPlanet;
	public bool didJump = false;
	public bool leftPressed = false;
	public bool rightPressed = false;
	float groundMoveVelocity = 0.8f;
	float airMoveVelocity = 0.8f;
	float jumpVelocity = 18.0f;	
	float playerRadius;

	PhysicsObject physicsObject;
	// Use this for initialization
	void Start () {
		//Starting inside gravField
		
		physicsObject = GetComponentInChildren<PhysicsObject>();
		float playerColliderRadius = transform.GetComponent<CircleCollider2D>().radius;
		float playerScale = transform.localScale.x;
		playerRadius = playerScale * playerColliderRadius;
	}
	// Update is called once per frame
	void Update () {
		//Check for user input
		if(Input.GetKeyDown(KeyCode.Space)){
			didJump = true;
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			leftPressed = true;
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			rightPressed = true;
		}
	}
	void FixedUpdate(){

		if(physicsObject.IsGrounded){	
			//Change the volecity if the user has pressed a key to move the player
			if(didJump){
				didJump = false;
				//Only jump when player is on the ground
				if (physicsObject.IsGrounded){
					physicsObject.addVelocityUp(jumpVelocity);
				}
			}	
			
			if(leftPressed){
				leftPressed = false;
				if (physicsObject.IsGrounded){
					physicsObject.addVelocityLeft(groundMoveVelocity);
				}
				else{
					physicsObject.addVelocityLeft(airMoveVelocity);
				}
			}
			if(rightPressed){
				rightPressed = false;
				if (physicsObject.IsGrounded){
					physicsObject.addVelocityRight(groundMoveVelocity);
				}
				else{
					physicsObject.addVelocityRight(airMoveVelocity);
				}
			}
		}
		//If not in gravField, we are in open space
		else{
			if(leftPressed){
				leftPressed = false;	
				physicsObject.addVelocityLeft(airMoveVelocity);
			}

			if(rightPressed){
				rightPressed = false;
				physicsObject.addVelocityRight(airMoveVelocity);
			}		
		}
	}	
	


}
