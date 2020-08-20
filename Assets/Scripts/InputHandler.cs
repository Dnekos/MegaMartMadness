using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    public PlayerConfiguration playerConfig;
    private PlayerControl controls;

    //for communicating to player
    private Movement mover;
    private ItemManager inventory;
    private RoundManager game;

    [Header("Camera")]
    [SerializeField]
    private Transform cam;
    [SerializeField]
    float camera_dist;
    Vector2 cam_position;
    [SerializeField]
    Transform camholder;


    // gathers components needed for the On__ functions
    private void Awake()
    {
        mover = GetComponent<Movement>();
        inventory = GetComponent<ItemManager>();

        controls = new PlayerControl();

        //round manager to know current gamestate
        game = FindObjectOfType<RoundManager>();
    }

    private void OnDestroy()
    {
        playerConfig.theInput.onActionTriggered -= DoAction;
    }

    /// <summary>
    /// creates configuration and links it with DoAction()
    /// </summary>
    /// <param name="pc">configuration from PlayerSelect scene</param>
    public void InitializePlayer(PlayerConfiguration pc)
    {
        Debug.Log("initialized player " + pc.playerIndex);

        playerConfig = pc;
        playerConfig.theInput.SwitchCurrentActionMap("Gameplay");//make sure we dont start in "Menu"
        playerConfig.theInput.onActionTriggered += DoAction;

        GetComponent<ItemManager>().p_index = playerConfig.playerIndex;
    }

    /// <summary>
    /// calls each of the On__ functions based on the context of the button
    /// </summary>
    /// <param name="obj"></param>
    private void DoAction(CallbackContext obj)
    {
        if (obj.action.name == controls.Gameplay.Move.name)
            OnMove(obj);
        else if (obj.action.name == controls.Gameplay.Camera.name)
            OnCamMove(obj);
        else if (obj.action.name == controls.Gameplay.Sell.name)
            OnBuy(obj);
        else if (obj.action.name == controls.Gameplay.Grab.name)
            OnGrab(obj);
        else if (obj.action.name == controls.Gameplay.Drop.name)
            OnDrop(obj);
        else if (obj.action.name == controls.Gameplay.UsePowerUp.name)
            OnUsePowerup(obj);
        else if (obj.action.name == controls.Gameplay.Reverse.name)
            OnReverse(obj);

    }

    // Update is called once per frame
    private void Update()
    {
        camholder.rotation = Quaternion.Euler(0, 0, 0);
    }

    /// <summary>
    /// handles pressing any of the 'cam' buttons/joystick
    /// </summary>
    /// <param name="context"></param>
    public void OnCamMove(CallbackContext context)
    {
        if (game.gameState == GameStates.RoundPlay)
        {
            if (context.control.name == "position") //normalize vector if mouse
            {
                float camh = Screen.height / 2;
                float camw = Screen.width / 2;
                float camx = Mathf.Clamp((context.ReadValue<Vector2>().x - camw) / camw, -1, 1);
                float camy = Mathf.Clamp((context.ReadValue<Vector2>().y - camh) / camh, -1, 1);
                cam.localPosition = new Vector3(camx * camera_dist, camy * camera_dist, -10);
            }
            else //for joysticks and such
                cam.localPosition = new Vector3(context.ReadValue<Vector2>().x * camera_dist, context.ReadValue<Vector2>().y * camera_dist, -10);
        }
    }

    /// <summary>
    /// handles pressing any of the 'move' buttons/joystick
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(CallbackContext context)
    {
        if (mover != null && game.gameState == GameStates.RoundPlay)
            mover.inputVector = context.ReadValue<Vector2>();
        else if (game.gameState == GameStates.RoundEnd)
            mover.inputVector = Vector2.zero;
    }

    /// <summary>
    /// handles pressing the 'grab' button
    /// </summary>
    /// <param name="context"></param>
    public void OnGrab(CallbackContext context)
    {
        if (inventory != null && game.gameState == GameStates.RoundPlay)
            inventory.grab = context.ReadValue<float>();
        else if (game.gameState == GameStates.RoundEnd)
            SceneManager.LoadScene("PlayerSelect");
    }

    /// <summary>
    /// handles holding the 'buy' button
    /// </summary>
    /// <param name="context"></param>
    public void OnBuy(CallbackContext context)
    {
        if (inventory != null && game.gameState == GameStates.RoundPlay)
        {
            if (context.ReadValue<float>() == 1 && inventory.atRegister)
                inventory.buying = true;
            else
                inventory.buying = false;
        }
    }

    /// <summary>
    /// handles pressing the 'drop' button
    /// </summary>
    /// <param name="context"></param>
    public void OnDrop(CallbackContext context)
    {
        if (inventory != null && game.gameState == GameStates.RoundPlay)
            if (context.ReadValue<float>() == 1 && !inventory.atRegister)
                inventory.DropItem();
    }
    public void OnUsePowerup(CallbackContext context)
    {
        if (inventory != null && game.gameState == GameStates.RoundPlay)
            if (context.ReadValue<float>() == 1)
                inventory.UsePowerup();
    }
    public void OnReverse(CallbackContext context)
    {
        if (mover != null && game.gameState == GameStates.RoundPlay)
            mover.reverse = context.ReadValue<float>();
    }
}
