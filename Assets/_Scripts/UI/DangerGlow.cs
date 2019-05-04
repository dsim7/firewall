using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerGlow : MonoBehaviour
{
    Animator _anim;
    Animator anim { get { if (!_anim) _anim = GetComponent<Animator>(); return _anim; } }

    public FirewallBufferVariable observedBuffer;
    public GameObject glowObject;

    public int dangerGrowThreshold;

    void Start()
    {
        glowObject.SetActive(false);

        observedBuffer.RegisterPrechangeEvent(UnobserveBufferCount);
        observedBuffer.RegisterPostchangeEvent(ObserveBufferCount);
    }

    void ObserveBufferCount()
    {
        if (observedBuffer.Value != null)
        {
            observedBuffer.Value.bufferCount.RegisterPostchangeEvent(UpdateInfo);
            UpdateInfo();
        }
    }

    void UnobserveBufferCount()
    {
        if (observedBuffer.Value != null)
        {
            observedBuffer.Value.bufferCount.UnregisterPostchangeEvent(UpdateInfo);
        }
    }

    void UpdateInfo()
    {
        if (observedBuffer.Value.bufferCount.Value <= dangerGrowThreshold)
        {
            glowObject.SetActive(true);
        }
        else
        {
            glowObject.SetActive(false);
        }
    }
}
