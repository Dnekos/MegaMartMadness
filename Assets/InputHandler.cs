using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Movement mover;
    private ItemManager inventory;
    private PlayerInput input;

    bool drop_lifted = true;

    // Start is called before the first frame update
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        var index = input.playerIndex;
        var movers = FindObjectsOfType<Movement>();
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        inventory = mover.GetComponent<ItemManager>();
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

    public void OnDrop(CallbackContext context)
    {
        if (mover != null)
        {
            if (context.ReadValue<float>() == 1 && drop_lifted == true)
            {
                inventory.DropItem();
                drop_lifted = false;
            }
            else if (context.ReadValue<float>() == 0)
                drop_lifted = true;
        }
    }
}
