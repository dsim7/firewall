using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    Text text;

    int accumulatedTicks;
    float timer;
    float timeInterval = 1;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        accumulatedTicks++;
        if (timer >= timeInterval)
        {
            text.text = (accumulatedTicks / timer).ToString("##");
            accumulatedTicks = 0;
            timer = 0;
        }
    }
}
