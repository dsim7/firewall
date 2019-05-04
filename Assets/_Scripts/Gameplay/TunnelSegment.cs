using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelSegment : MonoBehaviour
{
    public TunnelFloor floorPrefab;
    [Space]
    public BoolVariableSO building;
    public float radius = 10;
    public float travelSpeed = 10;
    public float probabilityWhite = 0.3f;
    public float probabilityCube = 0.3f;
    public Material firewallMaterial;
    
    public BufferCollection buffers { get; set; }

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

        SetAllDefault();
    }

    public void Remake()
    {
        foreach (TunnelFloor floor in floors)
        {
            floor.ClearCube();
            DetermineFloorIsFirewall(floor);
            DetermineFloorHasDataCube(floor);
        }
    }

    void SetAllDefault()
    {
        foreach (TunnelFloor floor in floors)
        {
            floor.SetToDefault();
        }
    }
    
    void DetermineFloorIsFirewall(TunnelFloor floor)
    {
        if (building.Value && Random.Range(0f, 1f) < probabilityWhite)
        {
            floor.SetWhite(firewallMaterial);
        }
        else
        {
            floor.SetToDefault();
        }
    }

    void DetermineFloorHasDataCube(TunnelFloor floor)
    {
        if (building.Value && Random.Range(0f, 1f) < probabilityCube)
        {
            floor.MakeDataCube();
        }
    }
}
