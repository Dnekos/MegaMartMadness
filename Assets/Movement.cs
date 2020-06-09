using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    

    [SerializeField]
    private int playerindex = 0;

    public Vector2 inputVector;

    [SerializeField]
    float moveDrag;
    [SerializeField]
    float acceleration;
    public float maxSpeed = 10f;
    [SerializeField]
    float minSpeed;    
    [SerializeField]
    float turnAcceleration;

    float speed;
    float turnSpeed;

    public int GetPlayerIndex()
    {
        return playerindex;
    }
    private void Update()
    {
        speed += inputVector.y * acceleration * Time.deltaTime ;
        if (Mathf.Abs(speed) > maxSpeed)
            speed = maxSpeed;
        transform.Translate(Vector2.up * speed, Space.Self);

        speed *= moveDrag;
        if (Mathf.Abs(speed) <= minSpeed  && inputVector.y == 0)
            speed = 0;

        turnSpeed += inputVector.x * turnAcceleration * Time.deltaTime;

        transform.Rotate(Vector3.back * turnSpeed);
        turnSpeed *= moveDrag;
        if (Mathf.Abs(speed) <= minSpeed && inputVector.y == 0)
            speed = 0;
    }
}
