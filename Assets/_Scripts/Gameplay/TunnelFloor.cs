using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelFloor : MonoBehaviour
{
    public PointsItem dataCubePrefab;
    [Space]
    public Transform dataCubeSpot;
    public Firewall firewall;
    public FirewallBuffer buffer { get { return firewall.buffer; } set { firewall.buffer = value; } }

    PointsItem dataCube;

    public void MakeDataCube()
    {
        dataCube = Instantiate(dataCubePrefab, dataCubeSpot);
    }

    public void ClearCube()
    {
        if (dataCube != null && !dataCube.collected)
        {
            GameObject temp = dataCube.gameObject;
            dataCube = null;
            Destroy(temp);
        }
    }

    public void SetToDefault()
    {
        firewall.SetToDefault();
    }

    public void SetFirewall(Material white)
    {
        firewall.SetWhite(white);
    }
}
