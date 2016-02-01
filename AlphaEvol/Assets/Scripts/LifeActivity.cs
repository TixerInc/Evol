using UnityEngine;
using System.Collections;

public class LifeActivity : MonoBehaviour
{

    Surviving sur;
    Sirching sirch;
    MoveForward mooving;
    //Rigidbody2D rig;
    GameObject graveyard;


    public float LifeForces = 50;
    public float Decreser;
    public float maturation = 80;
    public float rest = 2.5f;
    public bool tired;
    

    // Use this for initialization
    void Start()
    {
        graveyard = GameObject.Find("Graveyard");
        //rig = GetComponent<Rigidbody2D>();
        sur = GetComponent<Surviving>();
        sirch = GetComponent<Sirching>();
        mooving = GetComponent<MoveForward>();
        Decreser = mooving.maxSpeed / 2;
        tired = true;
    }


    void Groving()
    {
        transform.localScale = new Vector3(LifeForces * .01f, LifeForces * .01f, 0);
      //  rig.mass = LifeForces * .1f;
        if (transform.localScale.x <= .3f)
        {
            transform.localScale = new Vector3(.3f, .3f, 0);
           // rig.mass = 3f;
        }
    }


    bool done;
    bool fast;
    void TimeSettings()
    {
        if (Input.GetKeyUp(KeyCode.F) && !done)
        {
            fast = true;
            done = true;
            //Debug.Log ("IN");
        }

        if (Input.GetKeyUp(KeyCode.S) && done)
        {
            fast = false;
            done = false;
        }
        if (fast)
            Time.timeScale = 5.0f;
        else
            Time.timeScale = 1.0f;

    }



    public void BeenAtaked() {
        Destroy(gameObject);
    }

    void Update()
    {
       // TimeSettings();
        Groving();

        if (tired)
        {
            rest -= Time.deltaTime;
            if (rest <= 0)
            {
                tired = false;
                rest = 1.5f;
            }
        }

        if (sur.hunting || sur.combat)
        {
            LifeForces -= (Time.deltaTime) * Decreser * (.3f); //была учтена масса
            //Debug.Log("run");
        }
        else
        {
            LifeForces -= (Time.deltaTime) * (.3f); //была учтена масса
           // Debug.Log("walk");
        }

        if (LifeForces >= maturation)
            Reproduction();

        if (LifeForces <= 0)
        {
            GameObject[] survivers = GameObject.FindGameObjectsWithTag("predator");
            if (survivers.Length <= 1)
            {
                Debug.Log("lastOne " + survivers[0].name);
                Time.timeScale = 0;
            }
            GetComponent<Death>().enabled = true;
            mooving.enabled = false;
            sur.enabled = false;
            GetComponent<Sirching>().enabled = false;
            transform.parent = graveyard.transform;
            Starved++;
            this.enabled = false;
        }
    }

    public static int Starved;
    static int counter = 2;
    public GameObject clone;

    void Reproduction()
    {
        GameObject child = (GameObject)Instantiate(clone, new Vector3(transform.position.x + 2, transform.position.y), Quaternion.identity);
        tired = true;
        rest = 2.5f;
        LifeForces /= 2;
        LifeActivity life = child.GetComponent<LifeActivity>();
        life.LifeForces /= 2;
        life.maturation = maturation + Random.Range(-2f, 2f);
        child.GetComponent<MoveForward>().maxSpeed = mooving.maxSpeed + Random.Range(-.3f, .3f);
        child.GetComponent<Sirching>().ScaleDiference = sirch.ScaleDiference + Random.Range(-.05f, .05f);
        child.GetComponent<Surviving>().saveDist = sur.saveDist + Random.Range(-1f, 1f);
        child.name = "bacteria " + counter;
        counter++;
    }
}
