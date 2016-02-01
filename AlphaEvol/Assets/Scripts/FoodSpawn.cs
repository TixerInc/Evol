using UnityEngine;
using System.Collections;

public class FoodSpawn : MonoBehaviour {
    Stady st;
	public GameObject plancton;
	public float mealTimeMin = 1;
	public float mealTimeMax = 10;
    public int section = 1;
    //	public GameObject feeder;
    float dish;
	// Use this for initialization
	void Start () {
		dish = 0;
	}

	public static Vector3 RandomPosition(int section) {
        if (section == 1)
		    return new Vector3 (Random.Range (-70, 70), Random.Range (-70, 70));
        if (section == 2)
            return new Vector3 (Random.Range(-308, -112), Random.Range(-100, 100));
        if (section == 3)
            return new Vector3 (Random.Range(-308, -110), Random.Range(126, 322));
        if (section == 4)
            return new Vector3(Random.Range(-94, 102), Random.Range(123, 322));
        else
            return new Vector3(Random.Range(-100, 100), Random.Range(-100, 100));


    }
	
	// Update is called once per frame
	void Update () {
	
		dish -= Time.deltaTime;
		if (dish <= 0) {

			for (int i = 0; i <= 80; i++){
				GameObject ob = (GameObject) Instantiate (plancton, RandomPosition(section), Quaternion.identity);
				ob.transform.parent = transform;

			}

			dish = Random.Range (mealTimeMax, mealTimeMin);
		}
	}
}
