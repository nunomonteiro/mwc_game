using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierMovement : MonoBehaviour {

    private Vector2 startPoint;

    private bool isMovingDown;

    public float Velocity;

    public float BarrierMaxDistanceMoving;

    void Start()
    {
        startPoint = transform.position;
        moveDown();
        isMovingDown = true;
    }

    void Update()
    {
        if (isMovingDown)
        {
            moveDown();
        }
        else
        {
            moveUp();
        }

    }

    void moveDown()
    {
        if (Vector2.Distance(transform.position, startPoint) < BarrierMaxDistanceMoving)
        {
            transform.position -= new Vector3(0, Velocity, 0);

        }
        else
        {
            isMovingDown = false;
        }
    }

    void moveUp()
    {
        if (Vector2.Distance(transform.position, startPoint)  > 0f)
        {
            transform.position += new Vector3(0, Velocity, 0);

        }
        else
        {
            isMovingDown = true;
        }
    }


}
