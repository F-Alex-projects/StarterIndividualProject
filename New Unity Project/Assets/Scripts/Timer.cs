using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool finished;

    void Start () {
        finished = false;
        startTime = Time.time;
    }

    void Update () {
        if(finished)
            return;

        float t = Time.time - startTime;

        string seconds = (t % 60).ToString("f2");

        timerText.text = seconds;
        if (t >= 12)
            Finish();
    }
    public void Finish()
    {
        finished = true;
        timerText.color = Color.yellow;
        
    }
}
