using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour {
	//Handles the user input and the movements that are specific to the player like fiering.
	//Also handles the behavior of the player when is collides with different game objects.
	bool coolDown = false;
	public bool didJump = false;
	public bool leftPressed = false;
	public bool rightPressed = false;
	bool didShoot = false;

	float groundMoveVelocity = 40f;
	float airMoveVelocity = 30f;
	float jumpVelocity = 600f;	
	PhysicsObject physicsObject;
	public GameObject ProjectilePrefab;
	float hp = 3;
	float coolDownTimer = 1.5f;

	SpriteRenderer spriteRenderer;
	float collisionPushback = 25;
	ProjectileMovement projectileMovement;
	float projectileRadius;
	float playerRadius;
	public UnityEvent PlayerCollisionEvent = new UnityEvent();
	HpController hpController;
	
	bool facingRight{get {return spriteRenderer.flipX; }}

	void Start () {	
		hpController = GameObject.FindGameObjectWithTag("HpController").transform.GetComponent<HpController>();
		physicsObject = GetComponent<PhysicsObject>();
		spriteRenderer = GetComponent<SpriteRenderer>(); 
		projectileMovement = ProjectilePrefab.GetComponent<ProjectileMovement>();
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
		if(Input.GetKeyDown(KeyCode.X)){
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
		//Cooldown for when the player can fire.
		if(coolDown){
			coolDownTimer -= Time.deltaTime;
			didShoot = false;
			if (coolDownTimer < 0 ){
				coolDown = false;
				coolDownTimer = 1.5f;
			}
		}
		if(didShoot && coolDown == false){
			didShoot = false;
			coolDown = true;
			Instantiate(ProjectilePrefab, transform.position + projectileStartingPos() , Quaternion.identity).GetComponent<ProjectileMovement>().SetDirection(transform, facingRight);
		}
		
		physicsObject.UpdateVelocity();
		physicsObject.UpdateRotation("standard");
	}	

	Vector3 projectileStartingPos(){
		//Return the staring vector for the projectile realtive to the player postion.
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
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		hp -= 1;
		hpController.removeHp();

		if(hp == 0){
			//Set the the gameOver boll in camera when the player dies.
			camera = GameObject.FindGameObjectWithTag("MainCamera");
			camera.transform.GetComponent<CameraMovement>().GameOver = true;
			Destroy(gameObject);
		}
		//Camera shake when the player collides with enemies.
		camera.transform.GetComponent<CameraMovement>().Shake();
		//Add a pushback to the player.
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
