using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Power_Ups
{
    Grabber,
    Scanner,
    Boost,
    Shield
}

public class ItemManager : MonoBehaviour
{
    [HideInInspector]
    public List<StoreItem> items = new List<StoreItem>();
    [SerializeField]
    public int maxItems = 3;

    [HideInInspector]
    public float grab = 0;

    //cart sprites
    [SerializeField]
    SpriteRenderer bottom;
    [SerializeField]
    SpriteRenderer mid;
    [SerializeField]
    SpriteRenderer top;

    [SerializeField]
    GameObject droppeditem;

    //register stuff
    [HideInInspector]
    public bool atRegister;
    [HideInInspector]
    public bool buying;
    float buytimer = 0;
    [SerializeField]
    float max_buytime = 2f;
    [SerializeField]
    Transform healthbar;

    //round/scoring stuff
    RoundManager round;
    public int p_index;

    [SerializeField]
    Image PUIcon;

    private void Start()
    {
        round = FindObjectsOfType<RoundManager>()[0];//grabs the round manager
    }

    /// <summary>
    /// handles buying 
    /// </summary>
    private void Update()
    {
        if (buying && items.Count > 0) //if selling and is possible to sell
        {
            buytimer += Time.deltaTime;

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
        else
        {
            //reset values when no longer selling
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
        if (item.group == "Power_Up")
        {

        }
        else if (items.Count < maxItems)//checks to see if cart is full
        {
            items.Add(item);
            Debug.Log("item count: "+items.Count);
            switch (items.Count)//sets the cart image with random rotation
            {
                case 1:
                    bottom.transform.Rotate(Vector3.back * Random.Range(0, 360));
                    bottom.sprite = item.image;
                    break;
                case 2:
                    mid.transform.Rotate(Vector3.back * Random.Range(0, 360));
                    mid.sprite = item.image;
                    break;
                case 3:
                    top.transform.Rotate(Vector3.back * Random.Range(0, 360));
                    top.sprite = item.image;
                    break;
            }
            return true;
        }
        return false;
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
        switch (items.Count)
        {
            case 1:
                bottom.sprite = null;
                break;
            case 2:
                mid.sprite = null;
                break;
            case 3:
                top.sprite = null;
                break;
        }
        StoreItem item = items[items.Count - 1];
        items.Remove(item);
        return item;
    }
}
