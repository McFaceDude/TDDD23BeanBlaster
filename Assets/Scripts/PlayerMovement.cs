using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//Handles the input by the user
public class PlayerMovement : MonoBehaviour {

	public bool didJump = false;
	public bool leftPressed = false;
	public bool rightPressed = false;
	bool didShoot = false;

	float groundMoveVelocity = 80f;
	float airMoveVelocity = 30f;
	float jumpVelocity = 600f;	
	PhysicsObject physicsObject;
	public GameObject ProjectilePrefab;
	float hp = 3;


	SpriteRenderer spriteRenderer;
	float collisionPushback = 15;
	ProjectileMovement projectileMovement;
	float projectileRadius;
	float playerRadius;
	public UnityEvent PlayerCollisionEvent = new UnityEvent();

	bool facingRight{get {return spriteRenderer.flipX; }}
	// Use this for initialization
	void Start () {
		physicsObject = GetComponent<PhysicsObject>();
		spriteRenderer = GetComponent<SpriteRenderer>(); 
		projectileMovement = GameObject.FindGameObjectWithTag("Projectile").GetComponent<ProjectileMovement>();
		projectileRadius = projectileMovement.transform.localScale.x * projectileMovement.transform.GetComponent<CircleCollider2D>().radius;
		playerRadius = transform.localScale.x * transform.GetComponent<CircleCollider2D>().radius;
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
			Instantiate(ProjectilePrefab, transform.position + projectileStartingPos() , Quaternion.identity).GetComponent<ProjectileMovement>().SetDirection(transform, facingRight);
		}
		
		physicsObject.UpdateVelocity();
		physicsObject.UpdateRotation("standard");
	}	

	Vector3 projectileStartingPos(){
		
		if(facingRight){
			Vector2 startingPos = physicsObject.PlanetTangentRight.normalized * (projectileRadius + playerRadius) * 1.00001f;
			return new Vector3(startingPos.x, startingPos.y, 0);
		}
		else{
			Vector2 startingPos = physicsObject.PlanetTangentLeft.normalized * (projectileRadius + playerRadius) * 1.00001f;
			return new Vector3(startingPos.x, startingPos.y, 0);
		}
	}

	Vector2 vectorFromPosition(Transform fromTransform){
		return new Vector2(transform.position.x - fromTransform.position.x, transform.position.y - fromTransform.position.y);
	}

	void collidedWithHostile(Collider2D collider2D){
		hp -= 1;
		if(hp == 0){
			print("DEAAAD!");
			Destroy(gameObject);
			
		}
		Vector2 collisionVector = vectorFromPosition(collider2D.transform);
		physicsObject.addVelocityVector(collisionVector * collisionPushback);
	}

	void OnTriggerEnter2D(Collider2D collider2D){
		if (collider2D.name == "CollisionObject"){
			if(collider2D.transform.GetComponentsInParent<EnemyMovement>().Length > 0){
				collidedWithHostile(collider2D);
				
			}
		}
	}
}
