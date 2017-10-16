using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravField : MonoBehaviour {

	public float gravity = 20f;
	public float planetFriction = 0.05f; 
	public float atmosphereFriction = 0.01f;
	 
	public Sprite BeanifiedPLanet;

	PlayerMovement playerMovement;

	public GameObject TrampolinePrefab;
	List<EnemyMovement> enemiesInField = new List<EnemyMovement>();
	float amountOfenemies = 0;

	void Awake(){
	
	}
	void OnTriggerEnter2D(Collider2D collider2D){
		//print("Collided with " + collider2D.name + " parent: " + collider2D.transform.GetComponentsInParent<EnemyMovement>());
		if(collider2D.transform.GetComponentInParent<EnemyMovement>() != null ){
			//print("enemy entered! amoun in = " + amountOfenemies);
			amountOfenemies += 1;
		}
		print("Grav field collided with: " + collider2D.name);
		if(collider2D.transform.GetComponentInParent<PlayerMovement>() != null ){
			//print("enemy entered! amoun in = " + amountOfenemies);
			playerMovement =  collider2D.transform.GetComponentInParent<PlayerMovement>();
		}
	}
	
	public void EnemyDied(){
		amountOfenemies -= 1;
		if(amountOfenemies == 0){
			planetBeanified();
		}
	}

	Vector3 trampolineStartingPos(){
		float planetRadius =  playerMovement.transform.GetComponent<PhysicsObject>().planetRadius;
		Vector3 startingPos = new Vector3(7, -0.2f, 0) - transform.position;
		Debug.DrawRay(transform.position, startingPos.normalized * planetRadius , Color.red, 10f );
		return (startingPos.normalized * planetRadius);
	}

	IEnumerator wait(float waitTime){

		
		 yield return new WaitForSecondsRealtime(waitTime);
		 
         
	}

	void planetBeanified(){
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		camera.transform.GetComponent<CameraMovementOnPlayer>().ZoomOutForBeanification(transform.position);
		print("time before = " + Time.time);
		wait(200.0f);
		print("time after= " + Time.time);
		
		
		transform.GetComponentInParent<SpriteRenderer>().sprite = BeanifiedPLanet;
		//trampolineStartingPos();
		print("POSITION = " + transform.rotation);
		Instantiate(TrampolinePrefab,  new Vector3(7, -0.7f, -1) * 1.1f, new Quaternion(0f,0f,0f,1f)).GetComponent<Trampoline>().gravfieldTf = transform;
	}

}
