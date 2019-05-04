using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferBurst : MonoBehaviour
{
    public ParticleSystem burstSFX;

    ParticleSystem.MainModule main;

    void Start()
    {
        main = burstSFX.main;
    }

    public void Burst(Color color1, Color color2)
    {
        main.startColor = new ParticleSystem.MinMaxGradient(color1, color2);
        burstSFX.Play();
    }
}
