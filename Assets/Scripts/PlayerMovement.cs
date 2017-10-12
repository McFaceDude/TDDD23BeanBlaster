using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Handles the input by the user
public class PlayerMovement : MonoBehaviour {

	public bool didJump = false;
	public bool leftPressed = false;
	public bool rightPressed = false;
	float groundMoveVelocity = 30f;
	float airMoveVelocity = 30f;
	float jumpVelocity = 200f;	
	PhysicsObject physicsObject;
	// Use this for initialization
	void Start () {
		physicsObject = GetComponentInChildren<PhysicsObject>();
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
		//Call the UpdatePhysics method with any velocity from user input
		physicsObject.UpdateVelocity();
		physicsObject.UpdateRotation("player");
	}	
}
