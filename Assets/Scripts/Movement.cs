using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum StatusConditions
{
    Slow,
    Conveyor,
    Stunned,

}


public class Movement : MonoBehaviour
{
    public int items = 0;

    public Vector2 inputVector;
    public float grab = 0;

    [SerializeField]
    float moveDrag;
    [SerializeField]
    float acceleration;
    [SerializeField]
    float maxSpeed = 10f;
    [SerializeField]
    float minSpeed;    
    [SerializeField]
    float turnAcceleration;

    float speed;
    float turnSpeed;
    float velocity;

    private void FixedUpdate()
    {
        //gices 
        speed += inputVector.y * (acceleration * Time.deltaTime);

        if (Mathf.Abs(speed) > maxSpeed)
            speed = maxSpeed;
        transform.Translate(Vector2.up * speed, Space.Self);

        //gives it the drag
        speed = speed * moveDrag;
        if (Mathf.Abs(speed) <= minSpeed && inputVector.y == 0)
            speed = 0;

        turnSpeed += inputVector.x * turnAcceleration * Time.deltaTime;

        transform.Rotate(Vector3.back * turnSpeed);
        turnSpeed *= moveDrag;
        if (speed < 0 && turnSpeed < 0)
            turnSpeed = Mathf.Abs(turnSpeed) * -1;
        else if (speed < 0 && turnSpeed > 0)
            turnSpeed = Mathf.Abs(turnSpeed);
        if (Mathf.Abs(speed) <= minSpeed && inputVector.y == 0)
            speed = 0;
    }
}
