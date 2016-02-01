using UnityEngine;
using System.Collections;

public class Surviving : MonoBehaviour
{
    Sirching sirch;
    MoveForward mooving;
    LifeActivity bio;

    public bool hunting;
    public bool combat;
    public float saveDist = 7;


    GameObject victimForShure;
    // Use this for initialization
    void Start()
    {
        bio = GetComponent<LifeActivity>();
        sirch = GetComponent<Sirching>();
        mooving = GetComponent<MoveForward>();
    }
    

   float warDist = Mathf.Infinity;
   GameObject nearestEnemy;

   public  void SetTarget(float minDist, float battleDist, float scavDist, GameObject planctonFood, GameObject victim, GameObject carriotFood)
    {
        if (planctonFood != null && minDist < battleDist && minDist < scavDist)
        {
            mooving.randomWalk = false;
            mooving.setTarget(planctonFood.transform);
            hunting = true;
            combat = false;
            sirch.victimDist = Mathf.Infinity;
            sirch.carrionDist = Mathf.Infinity;
            victimForShure = null;
          //  Debug.Log("plancton");
        }
        
        if (!bio.tired && victim != null && battleDist < minDist && battleDist < scavDist)
        {
            victimForShure = victim;
            mooving.randomWalk = false;
            mooving.setTarget(victimForShure.transform);
            hunting = false;
            combat = true;
            sirch.plancDist = Mathf.Infinity;
            sirch.carrionDist = Mathf.Infinity;
            offInChek = false;
            offInLost = false;
        }

        if (carriotFood != null && scavDist < minDist && scavDist < battleDist)
        {
            mooving.randomWalk = false;
            mooving.setTarget(carriotFood.transform);
            hunting = true;
            combat = false;
            sirch.plancDist = Mathf.Infinity;
            sirch.victimDist = Mathf.Infinity;
            victimForShure = null;
        }

        CheckTarget(planctonFood, victim, carriotFood);
    }

    Vector3 RandPos()
    {
        return new Vector3(transform.position.x + Random.Range(-15f, 15f), transform.position.y + Random.Range(-15f, 15f));
    }

    void CheckTarget (GameObject placton, GameObject victim, GameObject carionn)
    {
        if (carionn == null || placton == null || victim == null)
        {
            if (carionn == null && placton == null && victim == null)
            {
                hunting = false;
                mooving.setTarget(null);
            }
        }

        if (victim == null || bio.tired)
        {
            victimForShure = null;
            combat = false;
            offInChek = true;
        }
    }
    
    public bool offInChek;
    public bool offInLost;

float angleBetweenAngles (float angle_0, float angle_1) {
float angleToAdd;
float angleToDeacrease;
if (angle_0 > angle_1) {
angleToAdd = angle_0 - angle_1;
angleToDeacrease = (angle_1 + (360f - angle_0));
} else {
angleToAdd = (angle_0 + (360f - angle_1));//eulerAngles.z + currentEulerAngles.z;
angleToDeacrease = angle_1 - angle_0;
}
return (angleToAdd > angleToDeacrease ? angleToDeacrease : angleToAdd);
}

    public void RunAvay(GameObject [] predators) {

        float angleSum = 0;
        int enemyCount = 0;

        for (int i = 0; i < predators.Length; i++)
        {
            if (predators[i] == null)
                continue;

            if (Vector2.Distance(predators[i].transform.position, transform.position) < saveDist)
            {
                enemyCount++;
                if (Vector2.Distance (predators[i].transform.position, transform.position) < warDist) {
                    warDist = Vector2.Distance(predators[i].transform.position, transform.position);
                    nearestEnemy = predators[i];
                }

                if (i > 0)
                {
                    float subSum = Mathf.Atan2(-predators[i].transform.position.y + transform.position.y, -predators[i].transform.position.x + transform.position.x) * Mathf.Rad2Deg;
                    float subSum1 = Mathf.Atan2(-predators[i-1].transform.position.y + transform.position.y, -predators[i-1].transform.position.x + transform.position.x) * Mathf.Rad2Deg;
                    angleBetweenAngles(subSum1, subSum);
                   // Debug.Log(" angleBetweenAngles " + angleBetweenAngles (subSum1, subSum));
                }
                if (predators.Length < 2)
                {
                    float subSum = Mathf.Atan2(-predators[i].transform.position.y + transform.position.y, -predators[i].transform.position.x + transform.position.x) * Mathf.Rad2Deg;
                    angleBetweenAngles(subSum, 0);
                   // Debug.Log(" Single angleBetweenAngles " + angleBetweenAngles(0, subSum));
                }
               /*  if (subSum > 0)
                {
                    angleSum += subSum;
                }
                else
                {
                    angleSum += subSum + 360;
                }*/

                mooving.randomWalk = false;
                mooving.setTarget(null);
              //  mooving.RunAway = true;

                if (nearestEnemy != null)
                {
                    if (Vector2.Distance(nearestEnemy.transform.position, transform.position) > saveDist || predators.Length <= 0)
                    {
                        mooving.randomWalk = true;
                        mooving.RunAway = false;
                        warDist = Mathf.Infinity;
                    }
                }
            }
        }

        if (enemyCount > 0)
        {
            float rad = (angleSum / enemyCount) * Mathf.Deg2Rad;
            dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
           // Debug.Log("dir " + dir+ " rad "+angleSum/enemyCount);
         //   RunAway(dir);
           // mooving.RunAway = true;
            // mooving.RunAvay(dir);
        }
    }

    void RunAway(Vector2 pos) {
        GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x, dir.y);

    }

    Vector2 dir;
    public static int Killed;

    void OnCollisionEnter2D (Collision2D coll)
    {
        if (coll.gameObject.layer == 10)
        {
            bio.LifeForces += 10;
            sirch.plancDist = Mathf.Infinity;
            hunting = false;
        }

        if (coll.gameObject.layer == 11)
        {
            bio.LifeForces += 10;
            sirch.carrionDist = Mathf.Infinity;
            hunting = false;
        }

        if (coll.gameObject == victimForShure)
        {
            if (combat)
            {
                LifeActivity life = coll.gameObject.GetComponent<LifeActivity>();
                float vinPosibility = coll.transform.localScale.x / transform.localScale.x * 100;
               // float vinPosibility = deltaHP / coll.transform.localScale.x * 100;
                int BattleResult = Random.Range(1, 100);
                if ((int)vinPosibility >= BattleResult)
                {
                  //  Debug.Log("Won! P = " + vinPosibility + " R = " + BattleResult);
                    bio.LifeForces += life.LifeForces * (vinPosibility /100);
                    life.BeenAtaked();
                    Killed++;
                    offInLost = false;
                    offInChek = false;
                }
                else
                {
                    float decresHp = ((BattleResult - vinPosibility) * (vinPosibility / 100)) /2;
                   // Debug.Log("Loose P = " + vinPosibility + " R = " + BattleResult + " decresHp " + decresHp);
                    victimForShure = null;
                    mooving.setTarget(null);
                    bio.LifeForces -= decresHp;
                    life.LifeForces -= (decresHp + vinPosibility / 100);
                    offInLost = true;
                    offInChek = false;
                }
                bio.tired = true;
                combat = false;
                sirch.victimDist = Mathf.Infinity;
            }
        }
    }
 
}
