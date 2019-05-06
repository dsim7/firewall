using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stretch
{
    short[][] layout;
    int indexOfCurrent;

    public Stretch(short[][] layout)
    {
        this.layout = layout;
    }

    public short[] GetNextLayout()
    {
        short[] result = layout[indexOfCurrent];
        HelperMethods.CyclicalIncrement(ref indexOfCurrent, layout.Length);
        return result;
    }

    public void Reset()
    {
        indexOfCurrent = 0;
    }

    public bool IsNew()
    {
        return indexOfCurrent == 0;
    }
}
