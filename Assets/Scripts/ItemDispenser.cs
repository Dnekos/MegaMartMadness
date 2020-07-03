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

    [SerializeField]
    Sprite filledimage;
    [SerializeField]
    Sprite emptyimage;

    private void Start()
    {
        if (item_index != 0)//if stocked, show that
        {
            stocked_item = new StoreItem(item_index);
            filled = true;
            if (filledimage == null) //if gameobject is FloorItem, set image to item's sprite
            {
                filledimage = stocked_item.image;
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = filledimage;
        }
        else
            gameObject.GetComponent<SpriteRenderer>().sprite = emptyimage;
    }

    /// <summary>
    /// handles when a player is next to it
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        Movement player = collision.GetComponent<Movement>();//grab Movement data from collision
        if (collision.tag == "Player" && filled && player.grab == 1) //if collision is with a player & grab is held down
        {
            if (collision.GetComponent<ItemManager>().AddItem(stocked_item))
            {
                filled = false;
                player.items++;
                Debug.Log("grabbed");

                //if the ShelfManager is attached to a floor item, delete it
                if (emptyimage == null)
                    Destroy(gameObject);
                else //else, show the empty sprite
                    gameObject.GetComponent<SpriteRenderer>().sprite = emptyimage;
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
        item_index = newIndex;
        stocked_item = new StoreItem(item_index);
        filled = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = filledimage;
    }
}
