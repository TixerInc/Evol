using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {
    public bool dense;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (dense && other.tag == "predator") {
                    // Debug.Log("IN");
        }
    }
}
