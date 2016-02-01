using UnityEngine;
using System.Collections;
using System;



public class Stady : MonoBehaviour {


    protected int i;

    // Use this for initialization
    void Start() {
    }
    public int this [int index]
    {
        get {
            if (true) {

            }
            return 0;
        }

        set { }
    }

    public virtual double Virt(float k) {
        float i = 20 + k;
       // Debug.Log("i " + i);
        return i;
    }

        int result;
    string st = "tixer";

    public int Fact (int i)
    {
        if (i == 1)
        {
            Debug.Log("last result " + result);
            return 1;
        }
        result = Fact(i - 1) * i;
        Debug.Log("i " + i + "result "+result);
        return result;
    }

    public void Metod(int i, float d = 20) { }

    int property;

    int _property
    { 
        get
        {
            return property;
        }

        set
        {
            if (value <= 10)
                property = value;
        }
    }




    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            _property = 20;
            Debug.Log(" " + property);
            
        }

        if (Input.GetMouseButtonUp(1))
        {
            _property = 2;
            Debug.Log(" " + property);

        }

      //  Debug.Log("one " + test.one +" two "+test.two);

    }
}


