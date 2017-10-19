using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconPreview : MonoBehaviour {
	public Sprite[] icons;
	GameObject icon;


	// Use this for initialization
	void Awake () {
		for (int i = 0; i < icons.Length; i++) {
			icon = new GameObject ("icon" + i);
			icon.transform.SetParent(this.gameObject.transform);
			icon.AddComponent<RectTransform> ();
			icon.AddComponent<Image> ();
			icon.GetComponent<Image> ().sprite = icons [i];
//			Image newImage = icon.AddComponent<Image> () as Sprite;
//			icon.AddComponent<Sprite>();
		}

//		foreach (var item in icons) {
////			Generate child image
//			icon = new GameObject("icon"+i);
//		
//		
//		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
