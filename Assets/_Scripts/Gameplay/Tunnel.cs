using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    List<TunnelSegment> segmentPool = new List<TunnelSegment>();

    public BufferCollection buffers;
    public TunnelSegment segmentPrefab;
    public int poolSize;
    public float buildSpeed;

    int indexOfFirst, indexOfLast;
    float length;
    TunnelSegment lastSegment { get { return segmentPool[indexOfLast]; } }
    TunnelSegment firstSegment { get { return segmentPool[indexOfFirst]; } }

    float timerToNextBuild;

    void Start()
    {
        length = segmentPrefab.floorPrefab.firewall.transform.localScale.z;

        for (int i = 0; i < poolSize; i++)
        {
            TunnelSegment newSegment = Instantiate(segmentPrefab, transform);
            newSegment.buffers = buffers;
            newSegment.transform.position += new Vector3(0, 0, length * i);
            segmentPool.Add(newSegment);
        }
        indexOfFirst = 0;
        indexOfLast = segmentPool.Count - 1;
    }

    void Update()
    {
        MoveSegments();
        TickTimeToBuild();
    }

    void MoveSegments()
    {
        foreach (Transform child in transform)
        {
            child.transform.position = child.transform.position + Vector3.back * length * buildSpeed * Time.deltaTime;
        }
    }

    void TickTimeToBuild()
    {
        timerToNextBuild += Time.deltaTime;
        while (timerToNextBuild > 1 / buildSpeed)
        {
            timerToNextBuild -= 1 / buildSpeed;
            BuildOne();
        }
    }

    void BuildOne()
    {
        firstSegment.transform.position = new Vector3(0, 0, lastSegment.transform.position.z + length);
        firstSegment.Remake();
        HelperMethods.CyclicalIncrement(ref indexOfFirst, segmentPool.Count);
        HelperMethods.CyclicalIncrement(ref indexOfLast, segmentPool.Count);
    }
}
