using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sirching : MonoBehaviour
{

    public float plancDist = Mathf.Infinity;
    public float victimDist = Mathf.Infinity;
    public float carrionDist = Mathf.Infinity;

    public List<GameObject> plancton = new List<GameObject>();
    public List<GameObject> victims = new List<GameObject>();
    public List<GameObject> carriot = new List<GameObject>();
    public List<GameObject> predators = new List<GameObject>();

    public float ScaleDiference = 1.2f;


  //  float timeToComeHome = 10;

    GameObject victim;
    GameObject carionnFood;
    GameObject planctonFood;
    //Transform foodSpawner;

    Surviving surv;
    MoveForward mooving;
    // Use this for initialization
    void Start()
    {
        surv = GetComponent<Surviving>();
        mooving = GetComponent<MoveForward>();
      //  foodSpawner = GameObject.Find("FoodSpawner").transform;
    }

    // Update is called once per frame
    void Update()
    {

        AnalysingData();
        Cheking();
       // ReturHome();
    }

  /*  void ReturHome()
    {
        if (plancton.Count == 0 && victims.Count == 0 && carriot.Count == 0 && predators.Count == 0)
        {
            mooving.randomWalk = true;
            if (Vector2.Distance(transform.position, foodSpawner.position) >= 20)
            {
                timeToComeHome -= Time.deltaTime;
                if (timeToComeHome <= 0)
                {
                    mooving.setTarget(foodSpawner);

                }
            }
        }
        else
        {
            mooving.randomWalk = false;
            timeToComeHome = 10;
        }
    }*/

    void Cheking()
    {

        if (planctonFood == null)
            plancDist = Mathf.Infinity;

        if (carionnFood == null)
            carrionDist = Mathf.Infinity;

        if (victim == null)
        {
            victimDist = Mathf.Infinity;
        }
        else if (victim.transform.localScale.x * ScaleDiference >= transform.localScale.x)
        {
            victim = null;
        }
    }

    void AnalysingData()
    {
        for (int i = 0; i < predators.Count; i++)
        {
            if (predators[i] == null )
            {
                predators.Remove(predators[i]);
                continue;
            }

            if (predators[i].tag != "predator")
            {
                predators.Remove(predators[i].gameObject);
                continue;
            }
                if (predators[i].transform.localScale.x <= transform.localScale.x * ScaleDiference)
            {
                predators.Remove(predators[i]);
                continue;
            }

            if (Vector2.Distance(predators[i].transform.position, transform.position) <= surv.saveDist)
            {

                GameObject[] dangerEnemy = new GameObject[predators.Count];
                for (int k = 0; k < dangerEnemy.Length; k++)
                {
                    dangerEnemy[k] = predators[k];
                }
              //  surv.RunAvay(dangerEnemy);
            }
        }

        for (int i = 0; i < victims.Count; i++)
        {
            if (victims[i] == null || victims[i].tag != "predator")
            {
                victims.Remove(victims[i]);
                continue;
            }

            if (victims[i].transform.localScale.x * ScaleDiference >= transform.localScale.x)
            {
                victims.Remove(victims[i]);
                mooving.setTarget(null);
                // Debug.Log("out");
                continue;
            }

            if (Vector2.Distance(victims[i].transform.position, transform.position) < victimDist)
            {
                victimDist = Vector2.Distance(victims[i].transform.position, transform.position);

                victim = victims[i];
                //  Debug.Log("dif " + victims[i].transform.localScale.x * ScaleDiference);

            }

            if (victim != null)
                victimDist = Vector2.Distance(victim.transform.position, transform.position);

        }

        for (int i = 0; i < plancton.Count; i++)
        {
            if (plancton[i] == null)
            {
                plancton.Remove(plancton[i]);
                continue;
            }

            if (Vector2.Distance(plancton[i].transform.position, transform.position) < plancDist)
            {
                plancDist = Vector2.Distance(transform.position, plancton[i].transform.position);
                planctonFood = plancton[i];
            }
        }

        for (int i = 0; i < carriot.Count; i++)
        {
            if (carriot[i] == null)
            {
                carriot.Remove(carriot[i]);
                continue;
            }

            if (Vector2.Distance(carriot[i].transform.position, transform.position) < carrionDist)
            {
                carrionDist = Vector2.Distance(carriot[i].transform.position, transform.position);
                carionnFood = carriot[i];
            }
        }

        surv.SetTarget(plancDist, victimDist, carrionDist, planctonFood, victim, carionnFood);
    }



    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "predator")
        {
            if (other.transform.localScale.x > transform.localScale.x * ScaleDiference)
            {
                for (int i = 0; i < predators.Count; i++)
                {
                    if (predators[i] != null && other.gameObject == predators[i].gameObject)
                        predators.Remove(predators[i]);

                }
                predators.Add(other.gameObject);
            }

            if (other.transform.localScale.x * ScaleDiference < transform.localScale.x)
            {
                for (int i = 0; i < victims.Count; i++)
                {
                    if (victims[i] != null && other.gameObject == victims[i].gameObject)
                        victims.Remove(victims[i]);
                }
                victims.Add(other.gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
            Debug.Log("IN");
        if (other.tag == "plancton")
        {
            for (int i = 0; i < plancton.Count; i++)
            {
                if (plancton[i] != null && other.gameObject == plancton[i].gameObject)
                    plancton.Remove(plancton[i]);
            }
            plancton.Add(other.gameObject);
        }

        if (other.gameObject.layer == 11)
        {
            for (int i = 0; i < carriot.Count; i++)
            {
                if (other.gameObject == carriot[i].gameObject)
                    carriot.Remove(carriot[i]);
            }
            carriot.Add(other.gameObject);
            // Debug.Log("carList " + carriot);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "plancton")
        {
            plancton.Remove(other.gameObject);
        }

        if (other.tag == "carrion")
            carriot.Remove(other.gameObject);

        if (other.tag == "predator")
        {
            for (int i = 0; i < predators.Count; i++)
            {
                if (other.gameObject == predators[i])
                    predators.Remove(other.gameObject);
            }

            for (int i = 0; i < victims.Count; i++)
            {
                if (other.gameObject == victims[i])
                    victims.Remove(other.gameObject);
            }
        }
    }
}
