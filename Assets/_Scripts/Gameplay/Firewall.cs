using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : MonoBehaviour
{
    Renderer _render;
    Renderer render { get { if (_render == null) _render = GetComponent<Renderer>(); return _render; } }
    
    public BoolVariableSO building;

    public FirewallBuffer buffer { get; set; }
    public bool IsBad { get; set; }

    void OnTriggerEnter(Collider col)
    {
        if (building.Value && IsBad && col.gameObject.CompareTag("Player"))
        {
            SetToDefault();
            FirewallBufferManager bufferManager = col.gameObject.GetComponent<FirewallBufferManager>();
            if (bufferManager != null)
            {
                bufferManager.Buffer(buffer);
            }
            BufferBurst bufferBurst = col.gameObject.GetComponent<BufferBurst>();
            if (bufferBurst != null)
            {
                bufferBurst.Burst(buffer.color, buffer.color2);
            }
        }
    }

    public void SetToDefault()
    {
        render.material = buffer.floorMaterial;
        IsBad = false;
    }

    public void SetWhite(Material white)
    {
        render.material = white;
        IsBad = true;
    }
}
