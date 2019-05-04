using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferOrbManager : MonoBehaviour
{
    Animator _anim;
    Animator anim { get { if (!_anim) _anim = GetComponent<Animator>(); return _anim; } }

    public BufferOrb[] bufferOrbs;
    public BufferCollection buffers;

    int indexOfCenter = 0;
    int indexOfLeft = 1;
    int indexOfHiddenLeft = 2;
    int indexOfHiddenRight = 3;
    int indexOfRight = 4;

    void Start()
    {
        bufferOrbs[0].buffer.Value = buffers.red;  // set CENTER ORB to track RED
        bufferOrbs[1].buffer.Value = buffers.blue;  // set LEFT ORB to track BLUE
        bufferOrbs[4].buffer.Value = buffers.green;  // set RIGHT ORB to track GREEN
    }

    public void Appear()
    {
        anim.SetTrigger("Appear");
    }

    public void Disappear()
    {
        anim.SetTrigger("Disappear");
    }

    public void UpdateInstant()
    {
        foreach (BufferOrb orb in bufferOrbs)
        {
            orb.UpdateInstant();
        }
    }

    public void RotateLeft()
    {
        foreach (BufferOrb orb in bufferOrbs)
        {
            orb.RotateRight();
        }
        bufferOrbs[indexOfHiddenLeft].buffer.Value = bufferOrbs[indexOfRight].buffer.Value;
        CycleIndicesRight();
    }

    public void RotateRight()
    {
        foreach (BufferOrb orb in bufferOrbs)
        {
            orb.RotateLeft();
        }
        bufferOrbs[indexOfHiddenRight].buffer.Value = bufferOrbs[indexOfLeft].buffer.Value;
        CycleIndicesLeft();
    }

    void CycleIndicesLeft()
    {
        HelperMethods.CyclicalDecrement(ref indexOfCenter, bufferOrbs.Length);
        HelperMethods.CyclicalDecrement(ref indexOfLeft, bufferOrbs.Length);
        HelperMethods.CyclicalDecrement(ref indexOfHiddenLeft, bufferOrbs.Length);
        HelperMethods.CyclicalDecrement(ref indexOfHiddenRight, bufferOrbs.Length);
        HelperMethods.CyclicalDecrement(ref indexOfRight, bufferOrbs.Length);
    }

    void CycleIndicesRight()
    {
        HelperMethods.CyclicalIncrement(ref indexOfCenter, bufferOrbs.Length);
        HelperMethods.CyclicalIncrement(ref indexOfLeft, bufferOrbs.Length);
        HelperMethods.CyclicalIncrement(ref indexOfHiddenLeft, bufferOrbs.Length);
        HelperMethods.CyclicalIncrement(ref indexOfHiddenRight, bufferOrbs.Length);
        HelperMethods.CyclicalIncrement(ref indexOfRight, bufferOrbs.Length);
    }
}
