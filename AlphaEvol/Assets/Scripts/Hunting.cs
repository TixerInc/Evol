using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Hunting : MonoBehaviour
{


    public float lifeForthes = 50;
    public float decreser;
    GameObject victim;
    MoveForward mooving;
    public GameObject clone;
    public bool special;
    bool hunting;
    Rigidbody2D rig;
    float birthEnergy;
    public float maturation = 80;
    public float rest;
    public bool tired;
    GameObject carriotFood;

    public Vector3 RandPos()
    {
        return new Vector3(transform.position.x + Random.Range(-15f, 15f), transform.position.y + Random.Range(-15f, 15f));
    }



    void Start()
    {
        mooving = GetComponent<MoveForward>();
        tired = true;
        rest = 2.5f;
        rig = GetComponent<Rigidbody2D>();
        decreser = mooving.maxSpeed / 2;
        birthEnergy = lifeForthes;
        if (decreser > 5)
            GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
    }

    void GetDistanses()
    {


        for (int i = 0; i < meal1.Count; i++)
        {
            if (meal1[i] != null && Vector2.Distance(transform.position, meal1[i].transform.position) < minDist)
            {
                victim = meal1[i];
                minDist = Vector2.Distance(transform.position, meal1[i].transform.position);
            }

            if (meal1[i] == null)
                meal1.Remove(meal1[i]);
        }

        for (int i = 0; i < competitors.Count; i++)
        {

            if (competitors[i] != null && (competitors[i].transform.localScale.x * 1.3f) <= transform.localScale.x && Vector2.Distance(transform.position, competitors[i].transform.position) < battleDist)
            {
                battleDist = Vector2.Distance(transform.position, competitors[i].transform.position);
                if (!tired)
                {
                    // combat = true;
                    opponent = competitors[i];
                }

                if (opponent != null)
                {
                        if ((opponent.transform.localScale.x * 1.3f) > transform.localScale.x)
                        {
                            //Debug.Log ("inHEar");
                            //combat = false;
                            battleDist = Mathf.Infinity;
                            opponent = null;
                        }
                }

            }
            if (competitors[i] == null)
                competitors.Remove(competitors[i]);
        }

        for (int i = 0; i < carriot.Count; i++)
        {
            if (carriot[i] != null && Vector2.Distance(carriot[i].transform.position, transform.position) < scavDist)
            {
                scavDist = Vector2.Distance(carriot[i].transform.position, transform.position);
                carriotFood = carriot[i];
            }
            if (carriot[i] == null)
                carriot.Remove(carriot[i]);
        }

    }


    int CompareFloat(float i1, float i2)
    {
        if (i1 > i2)
            return 1;
        else if (i1 < i2)
            return -1;
        else
            return 0;
    }

    int Comparator(GameObject c1, GameObject c2)
    {
        float dist1 = Vector2.Distance(transform.position, c1.transform.position);
        float dist2 = Vector2.Distance(transform.position, c2.transform.position);
        return dist1.CompareTo(dist2);
    }

    void Groving()
    {
        transform.localScale = new Vector3(lifeForthes * .001f, lifeForthes * .001f, 0) * 10;
        rig.mass = lifeForthes * .1f;
        if (transform.localScale.x <= .3f)
        {
            transform.localScale = new Vector3(.3f, .3f, 0);
            rig.mass = 3f;
        }
    }

    float battleDist = Mathf.Infinity;
    bool combat;
    GameObject opponent;

    float scavDist = Mathf.Infinity;


    void SetTarget()
    {
        if (victim != null && minDist < battleDist && minDist < scavDist)
        {
            mooving.randomWalk = false;
            mooving.setTarget(victim.transform);
            hunting = true;
            combat = false;
            battleDist = Mathf.Infinity;
            scavDist = Mathf.Infinity;
        }

        if (opponent != null && battleDist < minDist && battleDist < scavDist)
        {
            mooving.randomWalk = false;
            mooving.setTarget(opponent.transform);
            combat = true;
            hunting = false;
            minDist = Mathf.Infinity;
            scavDist = Mathf.Infinity;
        }

        if (carriotFood != null && scavDist < minDist && scavDist < battleDist)
        {
            mooving.randomWalk = false;
            mooving.setTarget(carriotFood.transform);
            combat = false;
            hunting = true;
            minDist = Mathf.Infinity;
            battleDist = Mathf.Infinity;
        }

    }

    void Check()
    {
        if (victim == null)
        {
            minDist = Mathf.Infinity;
            if (carriotFood == null)
                hunting = false;
        }

        if (opponent == null)
        {
            battleDist = Mathf.Infinity;
            combat = false;
        }
        else if (opponent.transform.localScale.x * 1.3f >= transform.localScale.x)
        {
            opponent = null;
            battleDist = Mathf.Infinity;
            mooving.randomWalk = true;
            combat = false;
        }

        if (carriotFood == null)
        {
            scavDist = Mathf.Infinity;
            if (victim == null)
                hunting = false;
        }

    }

    static bool fast;
    static bool done;
    static int kiiledOnes;

    //void OnGUI(){
    //GUI.Label (new Rect (20, 20, 250, 250), "kiiledOnes "+kiiledOnes);
    //}

    GameObject nearestEnemy;
    float warDist = Mathf.Infinity;
    void RunDesision()
    {
        float angleSum = 0;
        int enemyCount = 0;

        for (int i = 0; i < predators.Count; i++)
        {
            if (predators[i] != null && Vector2.Distance(predators[i].transform.position, transform.position) <= saveDist)
            {
                enemyCount++;

                if (Vector2.Distance(transform.position, predators[i].transform.position) < warDist)
                {
                    warDist = Vector2.Distance(transform.position, predators[i].transform.position);
                    nearestEnemy = predators[i];
                }

                float subSum = Mathf.Atan2(-predators[i].transform.position.y + transform.position.y, -predators[i].transform.position.x + transform.position.x) * Mathf.Rad2Deg;
                if (subSum > 0)
                {
                    angleSum += subSum;
                }
                else
                {
                    angleSum += subSum + 360;
                }

                mooving.randomWalk = false;
                mooving.setTarget(null);
                mooving.RunAway = true;

                if (nearestEnemy != null)
                {
                    if (Vector2.Distance(nearestEnemy.transform.position, transform.position) > saveDist || predators.Count <= 0)
                    {
                        mooving.randomWalk = true;
                        mooving.RunAway = false;
                        warDist = Mathf.Infinity;
                    }
                }
            }


            if (predators[i] == null || predators[i].transform.localScale.x < transform.localScale.x * 1.2f)
                predators.Remove(predators[i]);

        }
        if (enemyCount > 0)
        {
            float rad = (angleSum / enemyCount) * Mathf.Deg2Rad;
            dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            Debug.Log("dir " + dir);
            mooving.RunAvay(dir);
        }
    }


    Vector2 dir;
    void Update()
    {
        if (competitors.Count <= 0)
            combat = false;
        if (meal1.Count <= 0)
            hunting = false;
        //if (special)
        //Debug.Log(RandPos());

        SetTarget();
        Groving();
        Check();
        GetDistanses();
        if (special)
            RunDesision();

        if (tired)
        {
            rest -= Time.deltaTime;
            if (rest <= 0)
            {
                tired = false;
                rest = 1.5f;
            }
        }


        if (Eaten && !special)
            Destroy(gameObject);



        if (hunting || combat)
        {
            lifeForthes -= (Time.deltaTime) * decreser * (rig.mass * .3f);
        }
        else
            lifeForthes -= (Time.deltaTime) * (rig.mass * .3f);


        if (lifeForthes >= maturation)
            Reproduction();

        if (lifeForthes <= 0 && !special)
        {
            GameObject[] survivers = GameObject.FindGameObjectsWithTag("predator");
            if (survivers.Length <= 1)
            {
                Debug.Log("lastOne " + survivers[0].name);
                Time.timeScale = 0;
            }
            //Destroy (gameObject);
            GetComponent<Death>().enabled = true;
            mooving.enabled = false;
            this.enabled = false;
        }
    }

    //GameObject conk;
    public bool Eaten;
    float carriotEnergy = 10;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 10)
        {
            lifeForthes += 10;
            victim = null;
            hunting = false;
            minDist = Mathf.Infinity;
        }

        if (coll.gameObject.layer == 11)
        {
            lifeForthes += carriotEnergy;
            carriotFood = null;
            hunting = false;
            scavDist = Mathf.Infinity;
        }
        if (coll.gameObject.layer == 9 && coll.transform.localScale.x <= transform.localScale.x)
        {
            // if (special)
            //  Debug.Log("in " + (coll.transform.localScale.x * 1.2f));
            if (combat)
            {
                //   if (special)
                //   Debug.Log("in2 " + combat);
                Hunting hunt = coll.gameObject.GetComponent<Hunting>();
                lifeForthes += (hunt.lifeForthes * .5f);
                hunt.Eaten = true;
                combat = false;
                opponent = null;
                //  if (special)
                // Debug.Log("Killed "+name);
                battleDist = Mathf.Infinity;
                mooving.randomWalk = true;
                rest = hunt.lifeForthes * .05f;
                //   Debug.Log("rest "+rest);
                tired = true;
                kiiledOnes++;
            }
        }
    }

    List<GameObject> meal1 = new List<GameObject>();
    List<GameObject> competitors = new List<GameObject>();
    List<GameObject> carriot = new List<GameObject>();
    List<GameObject> predators = new List<GameObject>();
    public float saveDist = 5;

    float minDist = Mathf.Infinity;
    public bool Special2;

    void OnTriggerEnter2D(Collider2D other)
    {
        //		Debug.Log ("IN");
        if (other.tag == "plancton")
        {

            meal1.Add(other.gameObject);
        }

        if (other.tag == "predator")
        {
            competitors.Add(other.gameObject);
            //}
        }

        if (other.gameObject.layer == 11)
        {
            carriot.Add(other.gameObject);
        }

        if (other.gameObject.layer == 9 && other.transform.localScale.x >= transform.localScale.x * 1.2f)
        {
            //   if (special)
            //  Debug.Log("other "+other.transform.localScale.x * 1.2f+" name "+other.name+"  scale "+transform.localScale.x * 1.2f+" my name "+name);
            //  bool firstCvins = false;
            for (int i = 0; i < predators.Count; i++)
            {
                if (predators[i] != null && predators[i].name == other.name)
                    predators.Remove(predators[i]);
            }
            predators.Add(other.gameObject);
            // predPos.Add(other.transform.position);

        }
        // if (other.gameObject.layer == 9 && other.transform.localScale.x  < transform.localScale.x * 1.2f)
        //  {
        // if (special)
        //  Debug.Log("to small " + (transform.localScale.x * 1.2f / other.transform.localScale.x));
        //   predators.Remove(other.gameObject);
        // predPos.Remove(other.transform.position);
        // }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        for (int i = 0; i < meal1.Count; i++)
        {
            if (other.gameObject == meal1[i].gameObject)
                meal1.Remove(meal1[i]);
        }
        for (int i = 0; i < competitors.Count; i++)
        {
            if (other.gameObject == competitors[i].gameObject)
                competitors.Remove(competitors[i]);
        }

        for (int i = 0; i < carriot.Count; i++)
        {
            if (other.gameObject == carriot[i].gameObject)
                carriot.Remove(carriot[i]);
        }

        for (int i = 0; i < predators.Count; i++)
        {
            if (predators[i] != null && other.gameObject.name == predators[i].gameObject.name)
            {
                //  if (special)
                // {
                //  Debug.Log(predators[i].transform.position+"  "+predators[i].name);
                //  if (predators[i] != null)
                predators.Remove(predators[i]);
                //   predPos.Remove(predators[i].transform.position);
                // }
            }
        }
        // if (special)
        //   Debug.Log("Exit " + other.gameObject.name);
    }

    static int counter = 2;

    void Reproduction()
    {
        GameObject child = (GameObject)Instantiate(clone, new Vector3(transform.position.x + 2, transform.position.y), Quaternion.identity);
        Hunting hunt = child.GetComponent<Hunting>();
        hunt.lifeForthes = lifeForthes / 2;
        lifeForthes /= 2;
        rest = 2.5f;
        tired = true;
        //hunt.decreser = decreser + Random.Range (-.5f, .5f);
        hunt.maturation = maturation + Random.Range(-5, 5);
        if (hunt.maturation <= 10)
        {
            hunt.maturation = 10;
        }
        MoveForward move = child.GetComponent<MoveForward>();
        move.maxSpeed = (decreser + Random.Range(-.5f, .5f)) * 2;
        if (move.maxSpeed < 1f)
            move.maxSpeed = 1f;
        if (decreser <= 2)
            child.name = "bactetia " + counter;
        else
            child.name = "bacteriaFast " + counter;
        counter++;
    }

}
