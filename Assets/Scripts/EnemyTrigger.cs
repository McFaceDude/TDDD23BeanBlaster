using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour {

	EnemyMovement enemyMovement;
	void Start () {
		enemyMovement = transform.GetComponentInParent<EnemyMovement>();
	}

	void OnTriggerExit2D(Collider2D collider2D){
		if (collider2D.transform.GetComponentsInParent<PlayerMovement>().Length > 0){
			enemyMovement.followPLayer = false;
		}
	}
}
