using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Movement mover;
    private PlayerInput input;
    // Start is called before the first frame update
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        var index = input.playerIndex;
        var movers = FindObjectsOfType<Movement>();
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
    }

    // Update is called once per frame
    public void OnMove(CallbackContext context)
    {
        if (mover != null)
        {
            mover.inputVector = context.ReadValue<Vector2>();
        }
    }

    public void OnGrab(CallbackContext context)
    {
        if (mover != null)
            mover.grab = context.ReadValue<float>();
    }
}
