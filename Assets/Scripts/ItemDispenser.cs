using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDispenser : MonoBehaviour
{
    [SerializeField]
    public int item_index;
    StoreItem stocked_item;

    [SerializeField]
    public bool filled = false;

    SpriteRenderer obj;
    [SerializeField]
    Sprite filledimage;
    [SerializeField]
    Sprite emptyimage;

    [Header("Debug")]
    [SerializeField]
    int HeldItemID = -1;
    
    private void Start()
    {
        obj = GetComponentInParent<SpriteRenderer>();//setting variable
        if (item_index != 0)//if stocked, show that
        {
            stocked_item = new StoreItem(item_index);
            filled = true;
            if (filledimage == null) //if gameobject is FloorItem, set image to item's sprite
            {
                filledimage = stocked_item.image;
            }
            obj.sprite = filledimage;
        }
        else//if not stocked, show empty image
            obj.sprite = emptyimage;
    }

    /// <summary>
    /// handles when a player is next to it
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        ItemManager player = collision.GetComponent<ItemManager>();//grab Movement data from collision
        if (collision.tag == "Player" && filled && (player.grab == 1)) //if collision is with a player & grab is held down
        {
            if (player.AddItem(stocked_item))
            {
                filled = false;
                Debug.Log("grabbed");

                //if the ShelfManager is attached to a floor item, delete it
                if (emptyimage == null)
                    Destroy(gameObject);
                else //else, show the empty sprite
                    obj.sprite = emptyimage;
            }
        }
    }

    /// <summary>
    /// resets the image and item of the shelf object
    /// </summary>
    /// <param name="newIndex"></param>
    public void FillShelf(int newIndex)
    {
        Debug.Log("shelf filled: " + newIndex);
        if (HeldItemID == -1)
            item_index = newIndex;
        else
            item_index = HeldItemID;
        
        stocked_item = new StoreItem(item_index);
        filled = true;
        obj.sprite = filledimage;
    }

    public string HeldItemGroup()
    {
        return stocked_item.group;
    }
}
