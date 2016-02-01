using UnityEngine;
using System.Collections;

public class HolyIntervention : MonoBehaviour {

   
    float midMaturetion;
    float midSpeed;
    float midScaleDifference;
    float midSaveDist;
    int population;
    // Use this for initialization
    void Start() {
       
    }

    // Update is called once per frame
    void Update() {

        TimeSettings();

        if (Input.GetKeyUp(KeyCode.D))
        {
            ShowResalts();
        }
}

    void TimeSettings()
    {
        if (Input.GetKeyUp(KeyCode.F))
            Time.timeScale = 5;
        if (Input.GetKeyUp(KeyCode.S))
            Time.timeScale = 1;
    }

    void ShowResalts()
    {
        midMaturetion = 0;
        midSaveDist = 0;
        midScaleDifference = 0;
        midSpeed = 0;
        GameObject[] samples = GameObject.FindGameObjectsWithTag("predator");
        population = samples.Length;
        for (int i = 0; i < samples.Length; i++)
        {
            midMaturetion += samples[i].GetComponent<LifeActivity>().maturation;
            midSaveDist += samples[i].GetComponent<Surviving>().saveDist;
            midScaleDifference += samples[i].GetComponent<Sirching>().ScaleDiference;
            midSpeed += samples[i].GetComponent<MoveForward>().maxSpeed;
            if (i == samples.Length -1)
            {
                midMaturetion /= samples.Length;
                midSaveDist /= samples.Length;
                midScaleDifference /= samples.Length;
                midSpeed /= samples.Length;
            }
        }
    }

    void OnGUI(){
        GUI.Label(new Rect (20, 20, 250, 250), "midMaturetion "+ midMaturetion);
        GUI.Label(new Rect(20, 40, 250, 250), "midSaveDist " + midSaveDist);
        GUI.Label(new Rect(20, 60, 250, 250), "midScaleDifference " + midScaleDifference);
        GUI.Label(new Rect(20, 80, 250, 250), "midSpeed " + midSpeed);
        GUI.Label(new Rect(20, 100, 250, 250), "population " + population);
        GUI.Label(new Rect(500, 20, 250, 250), "Starved " + LifeActivity.Starved);
        GUI.Label(new Rect(500, 40, 250, 250), "Killed " + Surviving.Killed);
    }
}
