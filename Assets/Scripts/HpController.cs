using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpController : MonoBehaviour {
	//Handles the player hp hearts.s
	public GameObject Heart_1;
	public GameObject Heart_2;
	public GameObject Heart_3;


	public void removeHp(){
		if (Heart_3 != null){
			Destroy(Heart_3);
		}
		else if (Heart_2 != null){
			Destroy(Heart_2);
		}
		else if (Heart_1 != null){
			Destroy(Heart_1);
		}
	}
}
