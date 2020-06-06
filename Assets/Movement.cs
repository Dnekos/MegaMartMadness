using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed = 10f;

    [SerializeField]
    private int playerindex = 0;

    public Vector2 inputVector;

    private void Awake()
    {
    }

    public int GetPlayerIndex()
    {
        return playerindex;
    }
    private void Update()
    {
        transform.Translate(inputVector*Time.deltaTime*speed);
    }
}
