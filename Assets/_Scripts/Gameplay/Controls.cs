using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controls : MonoBehaviour
{
    public Runner runner;
    public BoolVariableSO building;

    void Update()
    {
        if (building.Value && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x < Screen.width / 2)
                    {
                        runner.MoveLeft();
                    }
                    else if (touch.position.x > Screen.width / 2)
                    {
                        runner.MoveRight();
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                var position = Input.mousePosition;
                if (position.x < Screen.width / 2)
                {
                    runner.MoveLeft();
                }
                else if (position.x > Screen.width / 2)
                {
                    runner.MoveRight();
                }
            }
        }
    }
}
