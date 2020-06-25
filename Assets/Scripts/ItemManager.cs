using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    public List<StoreItem> items = new List<StoreItem>();

    [SerializeField]
    int maxItems = 3;

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
    public bool atRegister;
    public bool selling;
    float selltimer = 0;
    [SerializeField]
    float max_selltime = 2f;
    [SerializeField]
    Transform healthbar;

    RoundManager round;
    public int p_index;

    private void Start()
    {
        round = FindObjectsOfType<RoundManager>()[0];
        //p_index = GetComponent<InputHandler>().playerConfig.playerIndex;
    }

    private void Update()
    {
        if (selling && items.Count > 0)
        {
            selltimer += Time.deltaTime;

            healthbar.parent.gameObject.SetActive(true);
            healthbar.parent.rotation = Quaternion.Euler(0, 0, 0);

            //setting the filling to go form one end to the other
            healthbar.localScale = new Vector3(selltimer / max_selltime, healthbar.localScale.y, healthbar.localScale.z);
            healthbar.localPosition = new Vector3(0.5f * (selltimer / max_selltime) -0.5f, healthbar.localPosition.y, healthbar.localPosition.z);
            if(selltimer >= max_selltime)
            {
                Debug.Log("bleh");
                round.player_scores[p_index] += RemoveTop().point_value;

                selltimer = 0;
            }
        }
        else
        {
            selltimer = 0;
            healthbar.parent.gameObject.SetActive(false);  
        }
    }

    public bool AddItem(StoreItem item)
    {
        if (items.Count < maxItems)
        {
            items.Add(item);
            Debug.Log(items.Count);
            switch (items.Count)
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

    public void DropItem()
    {
        if (items.Count > 0)
        {
            int item = RemoveTop().index;
            droppeditem.GetComponent<ShelfManager>().item_index = item;
            Instantiate(droppeditem, transform.position, transform.rotation);
        }

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
