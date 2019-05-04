using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirewallLaneManager : MonoBehaviour
{
    public const int NUM_LANES = 9;
    
    public FirewallLaneVariable currentLane;
    public FirewallBufferVariable currentBuffer;
    public BufferOrbManager orbManager;
    public BufferCollection buffers;

    FirewallLane[] lanes;
    int currentLaneIndex;
    
    void Start()
    {
        currentLaneIndex = 0;
        lanes = new FirewallLane[NUM_LANES];
        lanes[0] = new FirewallLane(buffers.red);
        lanes[1] = new FirewallLane(buffers.red);
        lanes[2] = new FirewallLane(buffers.green);
        lanes[3] = new FirewallLane(buffers.green);
        lanes[4] = new FirewallLane(buffers.green);
        lanes[5] = new FirewallLane(buffers.blue);
        lanes[6] = new FirewallLane(buffers.blue);
        lanes[7] = new FirewallLane(buffers.blue);
        lanes[8] = new FirewallLane(buffers.red);

        currentLane.Value = lanes[0];
    }

    public void SwitchLanesRight()
    {
        FirewallBuffer oldBuffer = currentLane.Value.buffer;

        HelperMethods.CyclicalIncrement(ref currentLaneIndex, NUM_LANES);
        currentLane.Value = lanes[currentLaneIndex];

        if (HasSwitchedBuffers(oldBuffer))
        {
            currentBuffer.Value = currentLane.Value.buffer;
            orbManager.RotateRight();
        }
    }

    public void SwitchLanesLeft()
    {
        FirewallBuffer oldBuffer = currentLane.Value.buffer;

        HelperMethods.CyclicalDecrement(ref currentLaneIndex, NUM_LANES);
        currentLane.Value = lanes[currentLaneIndex];

        if (HasSwitchedBuffers(oldBuffer))
        {
            currentBuffer.Value = currentLane.Value.buffer;
            orbManager.RotateLeft();
        }
    }

    bool HasSwitchedBuffers(FirewallBuffer oldBuffer)
    {
        return oldBuffer != currentLane.Value.buffer;
    }
}
