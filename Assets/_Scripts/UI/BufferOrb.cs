using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BufferOrb : MonoBehaviour
{
    Animator anim;
    Image image;

    public string startingPlace;
    public FirewallBufferVariable buffer;

    float targetFill;
    float fillRate = 0.8f;
    
    void Awake()
    {
        buffer.Value = null;

        image = GetComponent<Image>();
        anim = GetComponent<Animator>();
        anim.SetTrigger(startingPlace);

        buffer.RegisterPostchangeEvent(UpdateColor);
        buffer.RegisterPrechangeEvent(UnobserveOldBuffer);
        buffer.RegisterPostchangeEvent(ObserveNewBuffer);
    }

    void Update()
    {
        image.fillAmount = Mathf.MoveTowards(image.fillAmount, targetFill, fillRate * Time.deltaTime);
    }

    void UnobserveOldBuffer()
    {
        if (buffer.Value != null)
        {
            buffer.Value.OnReset.RemoveListener(UpdateInstant);
            buffer.Value.bufferCount.UnregisterPostchangeEvent(UpdateInfo);
        }
    }

    void ObserveNewBuffer()
    {
        if (buffer.Value != null)
        {
            buffer.Value.OnReset.AddListener(UpdateInstant);
            buffer.Value.bufferCount.RegisterPostchangeEvent(UpdateInfo);
        }
    }

    void UpdateInfo()
    {
        targetFill = (float) buffer.Value.bufferCount.Value / buffer.Value.bufferMax;
    }

    void UpdateColor()
    {
        if (buffer.Value != null)
        {
            UpdateInstant();
            image.color = buffer.Value.color;
        }
    }

    public void RotateRight()
    {
        anim.SetTrigger("Right");
    }

    public void RotateLeft()
    {
        anim.SetTrigger("Left");
    }

    public void UpdateInstant()
    {
        if (buffer.Value != null)
        {
            float fill = (float)buffer.Value.bufferCount.Value / buffer.Value.bufferMax;
            targetFill = fill;
            image.fillAmount = fill;
        }
    }
}
