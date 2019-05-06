using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelSpawner : MonoBehaviour
{
    public BoolVariableSO playing;
    [Space]
    public float probabilityFirewall;
    public float probabilityCube;
    [Space]
    public StretchManager stretchManager;
    public float stretchesInterval;

    float timerToNextStretch;
    Stretch currentStretch;
    int stretchRotation;
    short[] nextStretchLayout;
    bool buildingStretch;
    bool nextSegmentIsLastOfGame;

    void Start()
    {
        playing.RegisterPostchangeEvent(() =>
        {
            timerToNextStretch = 0;

            // when play stops, the next segment to spawn is the last one
            if (!playing.Value)
            {
                nextSegmentIsLastOfGame = true;
            }
        });

        currentStretch = GetNewStretch();
    }
    
    void Update()
    {
        TickStretchTimer();
    }
    
    public void Spawn(TunnelSegment segment)
    {
        if (playing.Value) {
            if (buildingStretch)
            {
                SpawnStretchLayout(segment, stretchRotation);
            }
            else
            {
                SpawnRandomLayout(segment);
            }
        }
        else
        {
            SpawnBlank(segment);
            
            // if next segment is the last, flag the spawning segment
            if (nextSegmentIsLastOfGame)
            {
                segment.lastSegmentOfGame = true;
                nextSegmentIsLastOfGame = false;
            }
        }
    }

    public void SpawnTutorial()
    {
        currentStretch = stretchManager.tutorialStretch;
        currentStretch.Reset();
        buildingStretch = true;
    }

    void TickStretchTimer()
    {
        if (playing.Value && !buildingStretch)
        {
            HelperMethods.UpdateTimer(ref timerToNextStretch, stretchesInterval, () => buildingStretch = true);
        }
    }

    void SpawnBlank(TunnelSegment segment)
    {
        segment.RemakeBlank();
    }

    void SpawnStretchLayout(TunnelSegment segment, int startRotation)
    {
        nextStretchLayout = currentStretch.GetNextLayout();

        segment.Remake(nextStretchLayout, startRotation);

        if (currentStretch.IsNew())
        {
            currentStretch = GetNewStretch();
            buildingStretch = false;
        }
    }

    void SpawnRandomLayout(TunnelSegment segment)
    {
        segment.Remake(probabilityFirewall, probabilityCube);
    }

    Stretch GetNewStretch()
    {
        stretchRotation = Random.Range(0, FirewallLaneManager.NUM_LANES);
        return stretchManager.GetRandomStretch();
    }
}
