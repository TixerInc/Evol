using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

	public float maxSpeed = 1f;
	public Transform target;
    public bool randomWalk;
    public bool RunAway;
	bool hunting;
   // Hunting hunt;
	public void setTarget(Transform value) {
		target = value;
	}
	
	Vector3 targetVector;

	void Start () {
		targetVector = new Vector3 (Random.Range (-8, 8), Random.Range (-8, 8));
        //hunt = GetComponent<Hunting>();
	}

    public void RunAvay(Vector2 direction) {
        if (RunAway)
        {
           // Vector3 targetPos;
            Vector2 dir = direction - (Vector2)transform.position;
            //  dir.Normalize();
            //  float zAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            //  Quaternion desRot = Quaternion.Euler(0, 0, zAngle);
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, desRot, 720 * Time.deltaTime);
           // GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x, dir.y);
            Vector3 pos = transform.position;
            Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);
            pos += transform.rotation * velocity * 2;
          //  transform.position = pos;
        }
    }
    public bool special;
    void Update() {
        if (RunAway)
            return;

        Vector3 targetPos;
        if (target != null && !randomWalk)
        {
            targetPos = target.position;
            hunting = true;
        } else {
            if (Vector3.Distance(targetVector, transform.position) < 15f)
            targetVector = new Vector3(transform.position.x + Random.Range(-30f, 30f), transform.position.y + Random.Range(-30f, 30f));
            if (special)
                Debug.Log(targetVector);
            targetPos = targetVector;
            hunting = false;
        }

        Vector2 dir = targetPos - transform.position;
        dir.Normalize();
        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Quaternion desRot = Quaternion.Euler(0, 0, zAngle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desRot, 720 * maxSpeed * Time.deltaTime);

        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);
        if (hunting) { 
        pos += transform.rotation * velocity * 2;
    }
		else 
			pos += transform.rotation * velocity;
		transform.position = pos;
	}
}
