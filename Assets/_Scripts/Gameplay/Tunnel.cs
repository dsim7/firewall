using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    public TunnelSegment segmentPrefab;
    [Space]
    public int poolSize;
    public float startingSpeed;
    public float speedIncreasePer10s;
    [Space]
    public BoolVariableSO playing;
    public BoolVariableSO inTutorial;
    public BoolVariableSO tunnelCleared;
    public TunnelSpawner spawner;
    public BufferCollection buffers;
    
    float segmentLength;
    [SerializeField]
    float currentSpeed;
    float timerToNextBuild;
    int indexOfFirst, indexOfLast;
    TunnelSegment lastSegment { get { return segmentPool[indexOfLast]; } }
    TunnelSegment firstSegment { get { return segmentPool[indexOfFirst]; } }
    List<TunnelSegment> segmentPool = new List<TunnelSegment>();

    void Start()
    {
        InitSegments();

        // When play starts, tunnel is not clear
        playing.RegisterPostchangeEvent(() =>
        {
            if (playing.Value)
            {
                tunnelCleared.Value = false;
            }
        });

        currentSpeed = startingSpeed;
    }
    
    void Update()
    {
        MoveSegments();
        TickTimers();
        IncreaseSpeed();
    }

    public void ResetSpeed()
    {
        currentSpeed = startingSpeed;
    }

    void InitSegments()
    {
        segmentLength = segmentPrefab.floorPrefab.firewall.transform.localScale.z;

        for (int i = 0; i < poolSize; i++)
        {
            TunnelSegment newSegment = Instantiate(segmentPrefab, transform);
            newSegment.buffers = buffers;
            newSegment.transform.position += new Vector3(0, 0, segmentLength * i);
            segmentPool.Add(newSegment);
        }
        indexOfFirst = 0;
        indexOfLast = segmentPool.Count - 1;
    }

    void MoveSegments()
    {
        foreach (Transform child in transform)
        {
            child.transform.position = child.transform.position + Vector3.back * segmentLength * currentSpeed * Time.deltaTime;
        }
    }

    void TickTimers()
    {
        HelperMethods.UpdateTimer(ref timerToNextBuild, 1 / currentSpeed, Build);
    }

    void IncreaseSpeed()
    {
        if (playing.Value && !inTutorial.Value)
        {
            currentSpeed += (speedIncreasePer10s / 10) * Time.deltaTime;
        }
    }

    void Build()
    {
        CheckFirstSegmentIsLastOfGame();
        firstSegment.transform.position = new Vector3(0, 0, lastSegment.transform.position.z + segmentLength);

        spawner.Spawn(firstSegment);

        HelperMethods.CyclicalIncrement(ref indexOfFirst, segmentPool.Count);
        HelperMethods.CyclicalIncrement(ref indexOfLast, segmentPool.Count);
    }

    // When a segment flagged as last segment passes, the tunnel is cleared
    void CheckFirstSegmentIsLastOfGame()
    {
        if (firstSegment.lastSegmentOfGame)
        {
            tunnelCleared.Value = true;
            firstSegment.lastSegmentOfGame = false;
        }
    }
}
