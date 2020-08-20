using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public enum Power_Ups
{
    Empty = -1,
    Grabber,
    Boost,
    Scanner,
    Shield
}

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    bool player_controlled;

    [Header("ItemStorage")]
    [SerializeField]
    public int maxItems = 3;
    [HideInInspector]
    public List<StoreItem> items = new List<StoreItem>();

    Text currentItemNoTxt;

    [HideInInspector]
    public float grab = 0;

    //sprites
    [SerializeField]
    GameObject CartItem;
    GameObject TopItem = null;

    [SerializeField]
    GameObject droppeditem;

    [Header("Purchasing")]
    [SerializeField]
    float max_buytime = 2f;
    [SerializeField]
    Transform healthbar;
    [HideInInspector]
    public bool atRegister;
    [HideInInspector]
    public bool buying;
    float buytimer = 0;

    //round/scoring stuff
    RoundManager round;
    [HideInInspector]
    public int p_index;

    [Header("PowerUps")]
    [SerializeField]
    Image PUIcon;
    [HideInInspector]
    public Power_Ups heldPU = Power_Ups.Empty;

    [SerializeField]
    float BoostDuration = 3f;
    float BoostTimer = 0f;
    [SerializeField]
    float BoostPower = 1.5f;

    public float GrabRange = 5f;
    [SerializeField]
    GameObject Grabber;

    [SerializeField]
    float ShieldDuration = 5f;
    float ShieldTimer = 0f;
    [HideInInspector]
    public bool Shielded = false;
    [SerializeField]
    SpriteRenderer ShieldImage;

    [SerializeField]
    float ScannerDuration = 15f;
    [HideInInspector]
    public float ScannerTimer = 0f;
    [SerializeField]
    GameObject Scanner;

    private void Start()
    {
        round = FindObjectsOfType<RoundManager>()[0];//grabs the round manager
        TopItem = gameObject;//defaults TopItem for parenting stuff

        if (player_controlled)//sets UI
        {
            currentItemNoTxt = gameObject.GetComponentsInChildren<Text>()[0];
            gameObject.GetComponentsInChildren<Text>()[1].text = "/" + maxItems;
        }
    }

    /// <summary>
    /// handles buying 
    /// </summary>
    private void Update()
    {
        if (player_controlled)//updates UI
            currentItemNoTxt.text = items.Count.ToString();

        if (BoostTimer > 0)//if Boost is active
        {
            BoostTimer -= Time.deltaTime;//decrease timer
            if (BoostTimer <= 0)//if boost ending, slow user down
            {
                if (player_controlled)
                    GetComponent<Movement>().temp_speed /= BoostPower;
                else
                    GetComponent<AIPath>().maxSpeed /= BoostPower;
            }
        }

        if (ScannerTimer > 0)//if Scanner active, decrease timer
            ScannerTimer -= Time.deltaTime;

        if (ShieldTimer > 0)//if Shield active
        {
            ShieldTimer -= Time.deltaTime;//decrease time
            ShieldImage.color = new Color(ShieldImage.color.r, ShieldImage.color.g, ShieldImage.color.b, Mathf.Clamp(ShieldTimer/2,0,0.5f));//fade out
            if (ShieldTimer <= 0)//if ending, set shield off
                Shielded = false;
        }

        if (buying && items.Count > 0) //if actively buying
        {
            buytimer += Time.deltaTime; //increase buytimer

            //makes bar visible and locally unmoving
            healthbar.parent.gameObject.SetActive(true);
            healthbar.parent.rotation = Quaternion.Euler(0, 0, 0);

            //setting the filling to go form one end to the other
            healthbar.localScale = new Vector3(buytimer / max_buytime, healthbar.localScale.y, healthbar.localScale.z);
            healthbar.localPosition = new Vector3(0.5f * (buytimer / max_buytime) -0.5f, healthbar.localPosition.y, healthbar.localPosition.z);
            if(buytimer >= max_buytime)
            {
                Debug.Log("Bought item");
                round.player_scores[p_index] += RemoveTop().point_value;

                buytimer = 0;
            }
        }
        else//reset values when no longer buying
        {
            buytimer = 0;
            healthbar.parent.gameObject.SetActive(false);  
        }
    }

    /// <summary>
    /// checks if possible to add an item to cart, adds it and sets image if so.
    /// </summary>
    /// <param name="item">Item object to be added</param>
    /// <returns></returns>
    public bool AddItem(StoreItem item)
    {
        if (item.group == "Power_Up" && heldPU == Power_Ups.Empty)
        {
            //Debug.Log("grabbed PU");
            heldPU = (Power_Ups)(item.index - 16);

            if (player_controlled)
            {
                Debug.Log(heldPU + "_icon");
                PUIcon.sprite = Resources.Load<Sprite>(heldPU + "_icon");
            }
            return true;
        }
        else if (items.Count < maxItems)//checks to see if cart is full
        {
            items.Add(item);
            Debug.Log("item count: "+items.Count);
            if (items.Count == 1)//needed to adjust the first item to the right spot
            {
                if (player_controlled)
                    CartItem.transform.position = new Vector3(0, 1.6f, -0.05f);
                else
                    CartItem.transform.position = new Vector3(0, 0.6f, -0.05f);
            }
            else if (items.Count == 2)//subsequent items are parented so they can be at 0
                CartItem.transform.position = new Vector3(0, 0, -0.05f);

            CartItem.transform.Rotate(Vector3.back * Random.Range(0, 360));//give it a nice spin
            CartItem.GetComponent<SpriteRenderer>().sprite = item.image;

            TopItem = Instantiate(CartItem, TopItem.transform);
            return true;
        }
        return false;//cart is full, dont update shelf
    }

    /// <summary>
    /// calls RemoveTop and creates a floor item object
    /// </summary>
    public void DropItem()
    {
        if (items.Count > 0)
        {
            int item = RemoveTop().index;
            droppeditem.GetComponent<ItemDispenser>().item_index = item;
            Instantiate(droppeditem, transform.position, transform.rotation);
        }
    }

    /// <summary>
    /// removes top last inserted item from cart and sets its sprite on the stack to null
    /// </summary>
    /// <returns></returns>
    public StoreItem RemoveTop()
    {
        GameObject temp = TopItem.transform.parent.gameObject;
        Destroy(TopItem);
        TopItem = temp;

        StoreItem item = items[items.Count - 1];
        items.Remove(item);
        return item;
    }

    /// <summary>
    /// handles shield and boost initial uses, and redirects for Grabber/Scanner functions
    /// </summary>
    public void UsePowerup()
    {
        if(heldPU != Power_Ups.Empty)
        {
            switch (heldPU)
            {
                case Power_Ups.Boost:
                    BoostTimer = BoostDuration;
                    if (player_controlled)
                        GetComponent<Movement>().temp_speed *= BoostPower;
                    else
                        GetComponent<AIPath>().maxSpeed *= BoostPower;
                    break;
                case Power_Ups.Shield:
                    ShieldTimer = ShieldDuration;
                    Shielded = true;
                    break;
                case Power_Ups.Grabber:
                    UseGrabber();
                    break;
                case Power_Ups.Scanner:
                    SpawnScanners();
                    break;
            }
            heldPU = Power_Ups.Empty;
            if (player_controlled)
                PUIcon.sprite = Resources.Load<Sprite>("empty_icon");
        }
    }

    public void UseGrabber()
    {
        //grabs each tagged collider near user 
        Collider2D[] nearobj = Physics2D.OverlapCircleAll(transform.position, GrabRange,9);
        foreach (var obj in nearobj) //filters through to find the first player with items
            if (obj.gameObject != gameObject && obj.GetComponent<ItemManager>().items.Count > 0)
            {
                Grabber.GetComponent<GrabberManager>().Target = obj.transform;
                Grabber.GetComponent<GrabberManager>().User = transform;
                Instantiate(Grabber);
                break;
            }
    }

    public void SpawnScanners()
    {
        if (!player_controlled)//enemies dont need help, so no scanners for them
            return;

        ScannerTimer = ScannerDuration;
        GameObject[] shelves = GameObject.FindGameObjectsWithTag("Shelf");
        foreach(var shelf in shelves)//spawns a Scanner object for each Rare shelf
            if (shelf.GetComponent<ItemDispenser>().HeldItemGroup() == "Rare")
            {
                Scanner.GetComponent<ScannerArrow>().shelf = shelf.GetComponent<ItemDispenser>();//direction to be pointed
                Instantiate(Scanner, transform);
            }        
    }
}