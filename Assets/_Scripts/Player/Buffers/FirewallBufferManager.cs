using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirewallBufferManager : MonoBehaviour
{
    public FirewallBufferVariable currentBuffer;

    public BufferCollection buffers;
    public IntVariableSO lives;

    void Start()
    {
        currentBuffer.RegisterPrechangeEvent(SwitchOutOf);
        currentBuffer.RegisterPostchangeEvent(SwitchInto);
        currentBuffer.Value = buffers.red;
        SwitchInto();
    }

    public void Buffer(FirewallBuffer buffer)
    {
        buffer.Buff();
        if (buffer.bufferCount.Value <= 0)
        {
            lives.Value--;
        }
    }

    public void ResetAll()
    {
        buffers.ResetAll();
    }

    public void FreezeAll(bool frozen)
    {
        buffers.FreezeAll(frozen);
    }

    void SwitchOutOf()
    {
        if (currentBuffer.Value != null)
        {
            currentBuffer.Value.StartDelayTimer();
        }
    }

    void SwitchInto()
    {
        if (currentBuffer.Value != null)
        {
            currentBuffer.Value.ResetDelayTimer();
        }
    }
}
