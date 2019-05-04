using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirewallLane
{
    public FirewallBuffer buffer { get; private set; }

    public FirewallLane(FirewallBuffer buffer)
    {
        this.buffer = buffer;
    }
}
