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
	float airMoveVelocity = 30f;
	float jumpVelocity = 500f;	
	PhysicsObject physicsObject;
	public GameObject ProjectilePrefab;
	SpriteRenderer spriteRenderer;

	bool facingRight{get {return spriteRenderer.flipX; }}
	// Use this for initialization
	void Start () {
		physicsObject = GetComponent<PhysicsObject>();
		spriteRenderer = GetComponent<SpriteRenderer>(); 
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
		if(Input.GetKeyDown(KeyCode.Space)){
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
			spriteRenderer.flipX = false;
			if (physicsObject.IsGrounded){
				physicsObject.addVelocityLeft(groundMoveVelocity);
			}
			else{
				physicsObject.addVelocityLeft(airMoveVelocity);
			}
		}
		if(rightPressed){
			rightPressed = false;
			spriteRenderer.flipX = true;
			if (physicsObject.IsGrounded){
				physicsObject.addVelocityRight(groundMoveVelocity);
			}
			else{
				physicsObject.addVelocityRight(airMoveVelocity);
			}
		}
		if(didShoot){
			didShoot = false;
			Instantiate(ProjectilePrefab, transform.position, Quaternion.identity).GetComponent<ProjectileMovement>().SetDirection(transform, facingRight);
		}
		
		physicsObject.UpdateVelocity();
		physicsObject.UpdateRotation("standard");
	}	
}
