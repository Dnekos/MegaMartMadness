using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberManager : MonoBehaviour
{
    [SerializeField]
    float speed;
    public Transform User;
    public Transform Target;
    float percentLength = 0f;
    bool reverse = false;

    StoreItem grabbedItem;

    // Update is called once per frame
    void Update()
    {
        transform.position = User.position;
        transform.up = Target.position - transform.position;
        float distance = Vector3.Distance(User.position, Target.position) / 4f;

        if (reverse)//going backward
        {
            percentLength -= Time.deltaTime / speed;
            if (percentLength < 0)//when returned to player, give them the item then delete
            {
                if (grabbedItem != null)
                    User.GetComponent<ItemManager>().AddItem(grabbedItem);
                Destroy(gameObject);
            }
        }
        else//going forward
        {
            percentLength += Time.deltaTime / speed;
            if (percentLength > 1)//when arrived, remove player item (if possible) and reverse
            {
                reverse = true;
                if(!Target.GetComponent<ItemManager>().Shielded)
                    grabbedItem = Target.GetComponent<ItemManager>().RemoveTop();
            }
        }
        transform.localScale = new Vector3(1, Mathf.Lerp(0, distance, percentLength), 1);//sets the length of grabber
    }
}
