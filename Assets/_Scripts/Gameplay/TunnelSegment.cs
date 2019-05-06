using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelSegment : MonoBehaviour
{
    public TunnelFloor floorPrefab;
    [Space]
    public float radius = 10;
    public float travelSpeed = 10;
    public Material firewallMaterial;
    
    public BufferCollection buffers { get; set; }
    public bool lastSegmentOfGame { get; set; }

    List<TunnelFloor> floors;

    void Start()
    {
        floors = new List<TunnelFloor>();

        for (int i = 0; i < FirewallLaneManager.NUM_LANES; i++)
        {
            float angle = i * (360 / FirewallLaneManager.NUM_LANES);
            Vector3 spawnPoint = new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad),
                                             radius * Mathf.Sin(angle * Mathf.Deg2Rad),
                                             0);
            TunnelFloor newFloor = Instantiate(floorPrefab, transform);
            newFloor.transform.localPosition = spawnPoint;
            newFloor.transform.localRotation = Quaternion.Euler(0, 0, angle);
            floors.Add(newFloor);
        }
        
        floors[0].buffer = buffers.red;
        floors[1].buffer = buffers.red;
        floors[8].buffer = buffers.red;

        floors[2].buffer = buffers.green;
        floors[3].buffer = buffers.green;
        floors[4].buffer = buffers.green;

        floors[5].buffer = buffers.blue;
        floors[6].buffer = buffers.blue;
        floors[7].buffer = buffers.blue;

        RemakeBlank();
    }

    public void Remake(float probabilityFirewall, float probabilityCube)
    {
        foreach (TunnelFloor floor in floors)
        {
            floor.ClearCube();
            DetermineFloorIsFirewallRandom(floor, probabilityFirewall);
            DetermineFloorHasDataCubeRandom(floor, probabilityCube);
        }
    }

    public void RemakeBlank()
    {
        foreach (TunnelFloor floor in floors)
        {
            floor.ClearCube();
            floor.SetToDefault();
        }
    }

    public void Remake(short[] layout, int startIndex = 0)
    {
        int indexToBuild = startIndex;
        for (int i = 0; i < floors.Count; i++)
        {
            TunnelFloor floor = floors[indexToBuild];
            floor.ClearCube();
            
            if ((layout[i] & (short)FloorType.Bad) != 0)
            {
                floor.SetFirewall(firewallMaterial);
            }
            else
            {
                floor.SetToDefault();
            }

            if ((layout[i] & (short)FloorType.HasCube) != 0)
            {
                floor.MakeDataCube();
            }

            HelperMethods.CyclicalIncrement(ref indexToBuild, floors.Count);
        }
    }
    
    void DetermineFloorIsFirewallRandom(TunnelFloor floor, float probability)
    {
        if (Random.Range(0f, 1f) < probability)
        {
            floor.SetFirewall(firewallMaterial);
        }
        else
        {
            floor.SetToDefault();
        }
    }

    void DetermineFloorHasDataCubeRandom(TunnelFloor floor, float probability)
    {
        if (Random.Range(0f, 1f) < probability)
        {
            floor.MakeDataCube();
        }
    }
}
