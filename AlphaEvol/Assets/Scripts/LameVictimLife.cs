using UnityEngine;
using System.Collections;

public class LameVictimLife : MonoBehaviour {

	public GameObject clone;
	float growing = 20f;
	Vector3 pos;
	Vector3 delta;
	FoodSpawn fs;
	void Start (){
		delta = new Vector3 (1, 1);
		pos = transform.position + delta;
		fs = GameObject.Find ("FoodSpawner").GetComponent <FoodSpawn> ();
		
	
	}

	void Update (){

		growing -= Time.deltaTime;
		if (growing <= 0) {
			growing = 20;
			GameObject ob = (GameObject)Instantiate (clone, pos, Quaternion.identity);
		//	ob.name = "food";
			//Debug.Log ("ob "+ob+" fs ");
			ob.transform.parent = fs.transform;
		}
	}

	void OnCollisionEnter2D (Collision2D coll) {

		if (coll.gameObject.layer == 9)
			Destroy (gameObject);
	}
}
