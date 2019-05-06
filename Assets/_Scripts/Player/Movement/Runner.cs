using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    Animator _anim;
    Animator anim { get { if (!_anim) _anim = GetComponent<Animator>(); return _anim; } }

    FirewallLaneManager laneManager;

    public Transform pivot;
    public Cinemachine.CinemachineVirtualCamera cam;

    public float radius;
    public float angleRateOfChange;
    
    float curAngle;
    float targetAngle;
    bool isMoving;
    
    void Start()
    {
        laneManager = GetComponent<FirewallLaneManager>();
    }

    void Update()
    {
        SetPosition();
        SetCameraPosition();
    }

    public void Appear()
    {
        anim.SetTrigger("Intro");
    }

    public void Disappear()
    {
        anim.SetTrigger("Out");
    }

    public void MoveRight()
    {
        //if (!isMoving)
        //{
        isMoving = true;
        targetAngle += 360 / FirewallLaneManager.NUM_LANES;
        laneManager.SwitchLanesRight();
        //}
    }

    public void MoveLeft()
    {
        //if (!isMoving)
        //{
        isMoving = true;
        targetAngle -= 360 / FirewallLaneManager.NUM_LANES;
        laneManager.SwitchLanesLeft();
        //}
    }

    void SetPosition()
    {
        curAngle = Mathf.MoveTowards(curAngle, targetAngle, angleRateOfChange * Time.deltaTime);
        Vector3 pivotPos = pivot.position;

        transform.position = new Vector3(pivotPos.x + Mathf.Cos(curAngle * Mathf.Deg2Rad) * radius,
                                         pivotPos.y + Mathf.Sin(curAngle * Mathf.Deg2Rad) * radius,
                                         transform.position.z);
        transform.rotation = Quaternion.Euler(transform.rotation.y, transform.rotation.y, curAngle - 90);

        if (curAngle == targetAngle)
        {
            isMoving = false;
        }
    }

    void SetCameraPosition()
    {
        cam.m_Lens.Dutch = curAngle + 90;
    }
}
