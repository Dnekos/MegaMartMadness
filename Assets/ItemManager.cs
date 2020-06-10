using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    public List<StoreItem> items = new List<StoreItem>();

    [SerializeField]
    int maxItems = 3;

    [SerializeField]
    SpriteRenderer bottom;
    [SerializeField]
    SpriteRenderer mid;
    [SerializeField]
    SpriteRenderer top;
    
    public bool AddItem(StoreItem item)
    {
        if (items.Count < maxItems)
        {
            items.Add(item);
            Debug.Log(items.Count);
            switch ( items.Count)
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
