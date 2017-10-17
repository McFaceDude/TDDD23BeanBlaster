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
	float timer = 2;
	bool beanify = false;
	public bool PlanetBeanified = false;

	void Awake(){
	
	}
	void OnTriggerEnter2D(Collider2D collider2D){
		if(collider2D.transform.GetComponentInParent<EnemyMovement>() != null ){
			amountOfenemies += 1;
		}
		if(collider2D.transform.GetComponentInParent<PlayerMovement>() != null ){
			playerMovement =  collider2D.transform.GetComponentInParent<PlayerMovement>();
		}
	}
	
	public void EnemyDied(){
		amountOfenemies -= 1;
		if(amountOfenemies == 0){
			PlanetBeanified = true;
			timer = 4;
			planetBeanify();
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

	void FixedUpdate(){
		
		if(beanify){
			timer -= Time.deltaTime;
			if (timer < 0 ){
				Instantiate(TrampolinePrefab,  new Vector3(7, -0.7f, -1) * 1.1f, new Quaternion(0f,0f,0f,1f)).GetComponent<Trampoline>().gravfieldTf = transform;
				beanify = false;
			}
		}
	}

	void planetBeanify(){
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		camera.transform.GetComponent<CameraMovementOnPlayer>().ZoomOutForBeanification(transform.position);
		transform.GetComponentInParent<SpriteRenderer>().sprite = BeanifiedPLanet;
		beanify = true;
	}
}
