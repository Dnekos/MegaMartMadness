using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    public PlayerConfiguration playerConfig;
    private PlayerControl controls;

    //for communicating to player
    private Movement mover;
    private ItemManager inventory;
    private RoundManager game;

    //camera stuff
    [SerializeField]
    private Transform cam;
    [SerializeField]
    float camera_dist;
    Vector2 cam_position;


    // Start is called before the first frame update
    private void Awake()
    {
        mover = GetComponent<Movement>();
        inventory = GetComponent<ItemManager>();

        controls = new PlayerControl();

        //round manager to know current gamestate
        game = FindObjectOfType<RoundManager>();
    }


    public void InitializePlayer(PlayerConfiguration pc)
    {
        Debug.Log("initialized player " + pc.playerIndex);
        playerConfig = pc;
        playerConfig.theInput.SwitchCurrentActionMap("Gameplay");
        playerConfig.theInput.onActionTriggered += DoAction;

        GetComponent<ItemManager>().p_index = playerConfig.playerIndex;
    }

    private void DoAction(CallbackContext obj)
    {
        Debug.Log(obj.action.name);
        if (obj.action.name == controls.Gameplay.Move.name)
            OnMove(obj);
        else if (obj.action.name == controls.Gameplay.Camera.name)
            OnCamMove(obj);
        else if (obj.action.name == controls.Gameplay.Sell.name)
            OnSell(obj);
        else if (obj.action.name == controls.Gameplay.Grab.name)
            OnGrab(obj);
        else if (obj.action.name == controls.Gameplay.Drop.name)
            OnDrop(obj);
        else if (obj.action.name == controls.Gameplay.UsePowerUp.name)
            OnUsePowerup(obj);
    }

    // Update is called once per frame
    private void Update()
    {
        cam.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void OnCamMove(CallbackContext context)
    {
         if (context.control.name == "position") //normalize vector if mouse
         {
            float camh = Screen.height / 2;
            float camw = Screen.width / 2;
            float camx = Mathf.Clamp((context.ReadValue<Vector2>().x - camw) / camw,-1,1);
            float camy = Mathf.Clamp((context.ReadValue<Vector2>().y - camh) / camh , -1, 1);
            cam.localPosition = new Vector3(camx * camera_dist, camy * camera_dist, -10);
        }
        else //for joysticks and such
            cam.localPosition = new Vector3(context.ReadValue<Vector2>().x * camera_dist, context.ReadValue<Vector2>().y * camera_dist, -10);
    }

    public void OnMove(CallbackContext context)
    {
        if (mover != null && game.gameState != "Round_End")
            mover.inputVector = context.ReadValue<Vector2>();
    }

    public void OnGrab(CallbackContext context)
    {
        Debug.Log("grab context = " + context.ReadValue<float>());
        if (mover != null && game.gameState != "Round_End")
            mover.grab = context.ReadValue<float>();
    }

    public void OnSell(CallbackContext context)
    {
        if (mover != null && game.gameState != "Round_End")
        {
            if (context.ReadValue<float>() == 1 && inventory.atRegister)
                inventory.selling = true;
            else
                inventory.selling = false;
        }
    }

    public void OnDrop(CallbackContext context)
    {
        if (mover != null && game.gameState != "Round_End")
            if (context.ReadValue<float>() == 1 && !inventory.atRegister)
                inventory.DropItem();
    }
    public void OnUsePowerup(CallbackContext context)
    {

    }
}
