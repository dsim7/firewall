using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirewallBuffer : MonoBehaviour
{
    public Color color, color2;
    public Material floorMaterial;
    [Space]
    public int bufferMax;
    public IntVariable bufferCount;
    public BoolVariable delayed;
    public float delayInterval;
    public float recupInterval;
    public bool frozen;
    [Space]
    public UnityEvent OnReset;

    float delayTimer;
    float recupTimer;
    
    public bool IsRecuperating { get { return delayTimer == 0; } }
    public float DelayTimerPercent { get { return 1 - (delayTimer / delayInterval); } }
    public float RecupTimerPercent { get { return 1 - (recupTimer / recupInterval); } }

    void Start()
    {
        Reset();
    }

    void Update()
    {
        if (!frozen)
        {
            Countdown();
        }
    }

    public void Buff()
    {
        bufferCount.Value--;
    }

    public void ResetDelayTimer()
    {
        delayTimer = delayInterval;
        delayed.Value = true;
    }

    public void StartDelayTimer()
    {
        delayed.Value = false;
    }

    public void Reset()
    {
        bufferCount.Value = bufferMax;
        recupTimer = 0;
        delayTimer = 0;
        OnReset.Invoke();
    }

    void Countdown()
    {
        if (!delayed.Value)
        {
            CountdownDelayTimer();
            if (delayTimer == 0)
            {
                CountdownRecupTimer();
            }
        }
    }

    void CountdownDelayTimer()
    {
        delayTimer = Mathf.Max(delayTimer - Time.deltaTime, 0);
    }

    void CountdownRecupTimer()
    {
        recupTimer -= Time.deltaTime;
        while (recupTimer <= 0)
        {
            recupTimer += recupInterval;
            bufferCount.Value = Mathf.Min(bufferCount.Value + 1, bufferMax);
        }
    }
}
