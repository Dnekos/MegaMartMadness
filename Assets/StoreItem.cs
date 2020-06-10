using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem
{
    public Sprite image;
    public int index;
    public int point_value;
    public StoreItem(int i)
    {
        index = i;
        Debug.Log(i);
        Debug.Log("worm");
        point_value = 100; 
        switch (i)
        {
            case 1:
               image = Resources.Load<Sprite>("temp_image_1");
               break;
            case 2:
                image = Resources.Load<Sprite>("temp_image_2");
                break;
            case 3:
                image = Resources.Load<Sprite>("temp_image_3");
                break;
        }
        Debug.Log(image.ToString());
    }
}
