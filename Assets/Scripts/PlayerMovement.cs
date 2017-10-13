using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Handles the input by the user
public class PlayerMovement : MonoBehaviour {

	public bool didJump = false;
	public bool leftPressed = false;
	public bool rightPressed = false;
	bool didShoot = false;

	float groundMoveVelocity = 80f;
	float airMoveVelocity = 80f;
	float jumpVelocity = 200f;	
	PhysicsObject physicsObject;
	public GameObject ProjectilePrefab;
	// Use this for initialization
	void Start () {
		physicsObject = GetComponentInChildren<PhysicsObject>();
	}
	// Update is called once per frame
	void Update () {
		//Check for user input
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			didJump = true;
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			leftPressed = true;
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			rightPressed = true; 
		}
		if(Input.GetKey(KeyCode.Space)){
			didShoot = true;
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
		if(didShoot){
			didShoot = false;
			
			Instantiate(ProjectilePrefab, transform.position, Quaternion.identity).GetComponent<ProjectileMovement>().SetDirection(transform);
		}
		
		//Call the UpdatePhysics method with any velocity from user input
		physicsObject.UpdateVelocity();
		physicsObject.UpdateRotation("player");
	}	
}
