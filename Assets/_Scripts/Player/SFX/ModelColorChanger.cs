﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelColorChanger : MonoBehaviour
{
    public FirewallBufferVariable currentBuffer;
    public ParticleSystem modelSFX;

    ParticleSystem.MainModule main;

    void Start()
    {
        main = modelSFX.main;

        currentBuffer.RegisterPostchangeEvent(ChangeColor);
    }

    public void ChangeColor()
    {
        main.startColor = currentBuffer.Value.color2;
    }
}
