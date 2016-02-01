using UnityEngine;
using System.Collections;
using System;

public class Stady_2 : Stady {

    public override double Virt(float k)
    {
        // base.Virt(6);
        double j = 50 /k;
        //Debug.Log("j = " + j);
        return j;
    }
    // Use this for initialization
    void Start () {
      //  Debug.Log(Fact(7));
        Debug.Log("base "+base.Virt(2));
       Debug.Log("over "+ Virt(8));
    }
	
	// Update is called once per frame
	void Update () {
        
        //Metod(23, 1);
	}
}
