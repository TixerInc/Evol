using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Running : MonoBehaviour {

    List <GameObject> predators = new List<GameObject>();
    public float saveDist = 5;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < predators.Count; i++)
        {
            if (Vector2.Distance(predators[i].transform.position, transform.position) < saveDist)
            { 
                
            }
        }
	
	}

   
}
