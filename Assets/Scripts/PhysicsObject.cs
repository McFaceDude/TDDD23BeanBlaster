using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsObject : MonoBehaviour {
	public LayerMask RayMask;

	Vector2 vectorToPlanet; 
	float planetDistane { get {return vectorToPlanet.magnitude;}}
	public Vector2 PlanetDirection { get {return vectorToPlanet.normalized;}}
	Vector2 PlanetTangentLeft { 
		get {
				Vector2 dir = PlanetDirection;
				return new Vector2(dir.y, -dir.x);
			}
		}
	Vector2 PlanetTangentRight{ get {return -PlanetTangentLeft;}}
	
	float objectRadius;
	
	Vector2 velocity = Vector2.zero;
	float gravity;
	float planetFriction;
	float atmosphereFriction;
	float planetRadius;
	public bool IsGrounded { get; private set; }
	public Transform TargetPlanet;

	public UnityEvent HitEvenet;

	public float FrictionMultiplier = 1;

	public bool InGravField { get {return TargetPlanet != null; }}
	Rigidbody2D body; 
	// Use this for initialization
	void Awake () {
		body = GetComponent<Rigidbody2D>();
		float objectColliderRadius = transform.GetComponent<CircleCollider2D>().radius;
		float objectScale = transform.localScale.x;
		objectRadius = objectScale * objectColliderRadius;
		HitEvenet = new UnityEvent();
	}
	
	// UpdatePhysics is called by PlayerMovement after all the player input is done at the end of every FixedUpdate 
	public void UpdateVelocity() {
		if(InGravField){

			vectorToPlanet = TargetPlanet.position - transform.position;
			velocity += PlanetDirection * gravity * Time.deltaTime;
			Debug.DrawRay(transform.position, PlanetDirection * gravity * Time.deltaTime, Color.yellow, 2f);

			Vector2 xVector = Vector2.Dot(velocity, PlanetTangentRight)*PlanetTangentRight; 
			Vector2 yVector = velocity - xVector; 

			float raycastDistance = 0f;
			if(Vector2.Dot(yVector, PlanetDirection) >= 0){
				raycastDistance = objectRadius + yVector.magnitude * Time.deltaTime;
			}

			//Collison detection with the planet.
			RaycastHit2D hit = Physics2D.Raycast(transform.position, PlanetDirection, raycastDistance, RayMask);
			IsGrounded = hit;
			
			if(hit){
				
				HitEvenet.Invoke();
				velocity = xVector * (1 - planetFriction * FrictionMultiplier) + (hit.distance - objectRadius) * PlanetDirection;
			}
			else{	
				velocity = xVector * (1 - atmosphereFriction * FrictionMultiplier)  + yVector;
			}
		}	
		body.velocity = velocity;
	}

	public void UpdateRotation(string type){
		if(type == "standard"){
			float groundDistance = planetDistane - planetRadius - objectRadius;
			float zeroToOne = Mathf.Clamp(1 - (groundDistance - (groundDistance * 0.935f)), 0, 1);
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(PlanetDirection.y, PlanetDirection.x) + 90), zeroToOne);
		}
		else if(type == "projectile"){
			float groundDistance = planetDistane - planetRadius - objectRadius;
			float zeroToOne = Mathf.Clamp(1 - (groundDistance - (groundDistance * 0.935f)), 0, 1);
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(PlanetDirection.y, PlanetDirection.x)), zeroToOne) ;
		}
	}

	public void addVelocityLeft(float velocity){
		this.velocity += velocity * PlanetTangentLeft * Time.deltaTime;
		UpdateVelocity();
	}
	public void addVelocityRight(float velocity){
		
		this.velocity += velocity * PlanetTangentRight * Time.deltaTime;
		UpdateVelocity();
	}
	public void addVelocityUp(float velocity){
		this.velocity += velocity * -PlanetDirection * Time.deltaTime;
		UpdateVelocity();
	}

	public void addVelocityVector(Vector2 velocityVector){
		this.velocity += velocityVector;
		UpdateVelocity();
	}
	public void SetTargetPlanet(GravField gravField ){

		if(gravField != null){
			TargetPlanet = gravField.transform.parent;
			this.gravity = gravField.gravity;
			this.planetFriction = gravField.planetFriction;
			this.atmosphereFriction = gravField.atmosphereFriction;
			planetRadius = TargetPlanet.transform.localScale.x * TargetPlanet.GetComponent<CircleCollider2D>().radius;
		}
	}
}
