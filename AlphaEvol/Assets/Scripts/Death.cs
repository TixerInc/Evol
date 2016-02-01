using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    //float energy = 15;
    bool carrion;
    float bodyCorruption = 40;
    // Use this for initialization
    void Start() {
        gameObject.layer = 11;
        gameObject.tag = "carrion";
        gameObject.name = "dead (" + name + ")";
        carrion = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
    }
    void Update()
    {
        bodyCorruption -= Time.deltaTime;
        if (bodyCorruption <= 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (carrion && other.gameObject.layer == 9)
            Destroy(gameObject);
    }
}
