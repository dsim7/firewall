using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeDetector : MonoBehaviour
{
    Vector2 fingerDownPosition;
    Vector2 fingerUpPosition;

    public bool detectSwipeOnlyAfterRelease = false;
    public float minDistanceForSwipe = 20f;

    public SwipeEvent OnSwipe = new SwipeEvent();

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }

            if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }
        }
    }

    void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticleSwipe())
            {
                var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                SendSwipe(direction);
            }
            else
            {
                var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                SendSwipe(direction);
            }
            fingerUpPosition = fingerDownPosition;
        }
    }

    bool SwipeDistanceCheckMet()
    {
        return VerticalMoveDistance() > minDistanceForSwipe || HorizontalMoveDistance() > minDistanceForSwipe;
    }

    float VerticalMoveDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    float HorizontalMoveDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    bool IsVerticleSwipe()
    {
        return VerticalMoveDistance() > HorizontalMoveDistance();
    }

    void SendSwipe(SwipeDirection direction)
    {
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
            StartPos = fingerDownPosition,
            EndPos = fingerUpPosition
        };
        OnSwipe.Invoke(swipeData);
    }
}

public class SwipeEvent : UnityEvent<SwipeData> { }

public struct SwipeData
{
    public Vector2 StartPos;
    public Vector2 EndPos;
    public SwipeDirection Direction;
}

public enum SwipeDirection
{
    Up, Down, Left, Right
}
