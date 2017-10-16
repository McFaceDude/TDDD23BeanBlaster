using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravField : MonoBehaviour {

	public float gravity = 20f;
	public float planetFriction = 0.05f; 
	public float atmosphereFriction = 0.01f;
	 
	public Sprite BeanifiedPLanet;

	
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
	}
	
	public void EnemyDied(){
		amountOfenemies -= 1;
		if(amountOfenemies == 0){
			planetBeanified();
		}
	}

	void planetBeanified(){
		transform.GetComponentInParent<SpriteRenderer>().sprite = BeanifiedPLanet;
	}

}
