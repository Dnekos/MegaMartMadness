﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum StatusConditions
{
    Slow,
    Conveyor,
    Stunned,
    Slippery
}


public class Movement : MonoBehaviour
{
    [SerializeField]
    bool TankControls;

    [HideInInspector]
    public Vector2 inputVector;
    [HideInInspector]
    public float grab = 0;

    //adjustables
    [SerializeField]
    float Drag;
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

    //condition stuff   
    [HideInInspector]
    public float temp_drag = 1f;   
    [HideInInspector]
    public float temp_speed = 1f;
    [HideInInspector]
    public Vector2 temp_translate = Vector2.zero;

    Vector2 newspeed;
    float newturnspeed;
    float target_degree;
    [SerializeField]
    int reverse_allowance = 25;

    private void Start()
    {
        temp_drag = 1f;
        temp_speed = 1f;
    }

    private void FixedUpdate()
    {
        if (TankControls)
        {
            //gives the speed 
            speed += inputVector.y * (acceleration * Time.deltaTime) * temp_speed;

            if (Mathf.Abs(speed) > maxSpeed)
                speed = maxSpeed;
            transform.Translate(Vector2.up * speed, Space.Self);

            //gives it the drag
            speed = speed * Drag * temp_drag;
            if (Mathf.Abs(speed) <= minSpeed && inputVector.y == 0)
                speed = 0;

            turnSpeed += inputVector.x * turnAcceleration * Time.deltaTime * temp_speed;

            transform.Rotate(Vector3.back * turnSpeed);
            turnSpeed *= Drag * temp_drag;
            if (speed < 0 && turnSpeed < 0)
                turnSpeed = Mathf.Abs(turnSpeed) * -1;
            else if (speed < 0 && turnSpeed > 0)
                turnSpeed = Mathf.Abs(turnSpeed);
            if (Mathf.Abs(speed) <= minSpeed && inputVector.y == 0)
                speed = 0;
        }
        else
        {
            //gives the speed 
            newspeed += inputVector * (acceleration * Time.deltaTime) * temp_speed;

            Vector2.ClampMagnitude(newspeed, maxSpeed);

            target_degree = (Mathf.Atan2(-newspeed.x, newspeed.y) * Mathf.Rad2Deg + 360) % 360;
            bool fob = !(Mathf.DeltaAngle(transform.eulerAngles.z, target_degree) > 180-reverse_allowance);
            Debug.Log(Mathf.DeltaAngle(transform.eulerAngles.z + 180, target_degree));
            Debug.Log(fob);
            if (fob)
                transform.Translate(Vector2.up * newspeed.magnitude, Space.Self);
            else
                transform.Translate(Vector2.down * newspeed.magnitude, Space.Self);

            //gives it the drag
            newspeed = newspeed * Drag * temp_drag;

            if (Mathf.Abs(speed) <= minSpeed && inputVector.y == 0)
                speed = 0;

            float angle;
            if (fob)
            {
                newturnspeed += inputVector.magnitude * turnAcceleration * Time.deltaTime * temp_speed;
                angle = Mathf.LerpAngle(transform.eulerAngles.z, target_degree, newturnspeed);
            }
            else
                angle = Mathf.LerpAngle(transform.eulerAngles.z, target_degree + 180, newturnspeed);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            newturnspeed *= Drag * temp_drag;
        }
    }
}
