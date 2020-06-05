using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    GamepadControls gcontrols;
    KeyControls kcontrols;

    public int controls = 3;// (0 keyboard 1 gp)

    public Vector2 move;
    public float speed = 10f;
    private void Awake()
    {
        gcontrols = new GamepadControls();
        gcontrols.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        gcontrols.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        kcontrols = new KeyControls();
        kcontrols.Gameplay.MoveUp.performed += ctx => move.y = 1;
        kcontrols.Gameplay.MoveUp.canceled += ctx => move.y = 0;

        kcontrols.Gameplay.MoveDown.performed += ctx => move.y =-1;
        kcontrols.Gameplay.MoveDown.canceled += ctx => move.y = 0;

        kcontrols.Gameplay.MoveLeft.started += ctx => move.x = -1;
        kcontrols.Gameplay.MoveLeft.canceled += ctx => move.x = 0;

        kcontrols.Gameplay.MoveRight.started += ctx => move.x = 1;
        kcontrols.Gameplay.MoveRight.canceled += ctx => move.x = 0;
    }


    private void Update()
    {
        Vector2 m = move * Time.deltaTime * speed;
        transform.Translate(m);

        if (controls == 0)
        {
            gcontrols.Gameplay.Disable();
            kcontrols.Gameplay.Enable();
            //   move = Vector2.zero;
        }
        else if (controls == 1)
        {
            gcontrols.Gameplay.Enable();
            kcontrols.Gameplay.Disable();
        }
    }

    private void OnEnable()
    {
        gcontrols.Gameplay.Enable();
        kcontrols.Gameplay.Enable();
    }
    private void OnDisable()
    {
        gcontrols.Gameplay.Disable();
        kcontrols.Gameplay.Disable();
    }
}
