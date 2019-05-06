using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controls : MonoBehaviour
{
    public Runner runner;
    public BoolVariableSO building;
    public SwipeDetector swipeDetector;

    void Awake()
    {
        swipeDetector.OnSwipe.AddListener(SwipeMoveRunner);
    }

    void Update()
    {
        if (building.Value && !EventSystem.current.IsPointerOverGameObject())
        {
            ScreenSideTouchMoveRunner();
            //ArrowKeysMoveRunner();
        }
    }

    public void SwipeMoveRunner(SwipeData data)
    {
        if (building.Value)
        {
            if (data.Direction == SwipeDirection.Left)
            {
                runner.MoveLeft();
            }
            else if (data.Direction == SwipeDirection.Right)
            {
                runner.MoveRight();
            }
        }
    }

    public void ScreenSideTouchMoveRunner()
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
    }

    public void ArrowKeysMoveRunner()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            runner.MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            runner.MoveRight();
        }
    }
}
