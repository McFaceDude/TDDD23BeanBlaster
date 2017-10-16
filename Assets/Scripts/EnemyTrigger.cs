using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour {

	// Use this for initialization

	EnemyMovement enemyMovement;
	void Start () {
		enemyMovement = transform.GetComponentInParent<EnemyMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit2D(Collider2D collider2D){
		if (collider2D.transform.GetComponentsInParent<PlayerMovement>().Length > 0){
			print("player exited enemy field");
			enemyMovement.followPLayer = false;
			
		}
	}
}
