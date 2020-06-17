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
    private Transform player;
    private PlayerInput input;
    [SerializeField]
    private Transform cam;

    [SerializeField]
    float camera_dist;
    Vector2 cam_position;

    // Start is called before the first frame update
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        var index = input.playerIndex;
        var movers = FindObjectsOfType<Movement>();
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        inventory = mover.GetComponent<ItemManager>();
        player = mover.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = player.position;
        //cam.localPosition = new Vector3(cam_position.x * camera_dist, cam_position.y * camera_dist, -10);
    }

    public void onCamMove(CallbackContext context)
    {
        cam.localPosition = new Vector3(context.ReadValue<Vector2>().x * camera_dist, context.ReadValue<Vector2>().y * camera_dist, -10);
        //cam_position = context.ReadValue<Vector2>();
    }

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

    public void OnSell(CallbackContext context)
    {
        if (mover != null)
        {
            Debug.Log("pee haha");

            if (context.ReadValue<float>() == 1 && inventory.atRegister)
                inventory.selling = true;
            else
            {
                inventory.selling = false;
            }
        }
    }

    public void OnDrop(CallbackContext context)
    {
        if (mover != null)
        {
            if (context.ReadValue<float>() == 1 && !inventory.atRegister)
            {
                inventory.DropItem();
            }
        }
    }
}
