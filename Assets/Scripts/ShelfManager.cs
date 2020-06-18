using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    [SerializeField]
    public int item_index;
    StoreItem stocked_item;

    [SerializeField]
    bool filled = false;

    [SerializeField]
    Sprite filledimage;
    [SerializeField]
    Sprite emptyimage;

    private void Start()
    {
        
        if (item_index != 0)
        {
            stocked_item = new StoreItem(item_index);
            filled = true;
            if (filledimage == null)
            {
                filledimage = stocked_item.image;
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = filledimage;
        }
        else
            gameObject.GetComponent<SpriteRenderer>().sprite = emptyimage;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Movement player = collision.GetComponent<Movement>();
        if (collision.tag == "Player" && filled  && player.grab == 1)
        {
            if (collision.GetComponent<ItemManager>().AddItem(stocked_item))
            {
                filled = false;
                player.items++;
                Debug.Log("grabbed");

                //
                if (emptyimage == null)
                    Destroy(gameObject);
                else
                    gameObject.GetComponent<SpriteRenderer>().sprite = emptyimage;

            }
        }
    }
}
