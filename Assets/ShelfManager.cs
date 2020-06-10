using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    [SerializeField]
    int item_index;
    StoreItem stocked_item;

    [SerializeField]
    bool filled = false;

    [SerializeField]
    Sprite filledimage;
    [SerializeField]
    Sprite emptyimage;

    private void Start()
    {
        stocked_item = new StoreItem(item_index);
        if (item_index != 0)
        {
            filled = true;
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
                gameObject.GetComponent<SpriteRenderer>().sprite = emptyimage;
                Debug.Log("grabbed");
            }
        }
    }
}
