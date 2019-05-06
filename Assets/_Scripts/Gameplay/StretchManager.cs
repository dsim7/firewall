using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchManager : MonoBehaviour
{
    public TextAsset csv;
    public TextAsset tutorialCsv;

    public List<Stretch> stretches { get; private set; }
    public Stretch tutorialStretch { get; private set; }

    void Awake()
    {
        stretches = new List<Stretch>();
        ReadTutorialStretch();
        ReadStretchData();
    }

    void ReadTutorialStretch()
    {
        short[][] tutorialLayout = GetStretchLayouts(tutorialCsv.text)[0];
        tutorialStretch = new Stretch(tutorialLayout);
    }

    void ReadStretchData()
    {
        List<short[][]> layouts = GetStretchLayouts(csv.text);
        foreach (short[][] layout in layouts)
        {
            Stretch newStretch = new Stretch(layout);
            stretches.Add(newStretch);
        }
    }

    public Stretch GetRandomStretch()
    {
        int rand = UnityEngine.Random.Range(0, stretches.Count);
        return stretches[rand];
    }

    List<short[][]> GetStretchLayouts(string data)
    {
        List<short[][]> result = new List<short[][]>();

        // split all stretches
        string[] stretches = data.Split(new char[] { '-' }, StringSplitOptions.None);
        foreach (string stretch in stretches)
        {
            List<short[]> segmentList = new List<short[]>();

            string trimmedStretch = stretch.Trim(new char[] { '\n', '\r', ' ' });

            // for each stretch, split all segments
            string[] segments = trimmedStretch.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string segment in segments)
            {
                // for each segment, split all tiles
                string[] tiles = segment.Split(new char[] { ',' }, StringSplitOptions.None);
                
                // convert to short
                short[] tilesData = Array.ConvertAll(tiles, Convert.ToInt16);

                // collect tiles arrays to segment array
                segmentList.Add(tilesData);
            }
            // collect segment arrays to stretch array
            result.Add(segmentList.ToArray());
        }

        return result;
    }
}

public enum FloorType
{
    None = 0,
    Bad = 1,
    HasCube = 2
}
