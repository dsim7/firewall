using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferCollection : MonoBehaviour
{
    public FirewallBuffer red, green, blue;

    public FirewallBuffer GetBuffer(FirewallColor color)
    {
        switch (color)
        {
            case FirewallColor.Red: return red;
            case FirewallColor.Green: return green;
            default: return blue;
        }
    }

    public void ResetAll()
    {
        red.Reset();
        blue.Reset();
        green.Reset();
    }

    public void FreezeAll(bool frozen)
    {
        red.frozen = frozen;
        blue.frozen = frozen;
        green.frozen = frozen;
    }
}
