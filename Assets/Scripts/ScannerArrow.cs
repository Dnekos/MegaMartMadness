using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerArrow : MonoBehaviour
{
    public ItemDispenser shelf;
    ItemManager user;
    SpriteRenderer image;

    private void Start()
    {
        user = GetComponentInParent<ItemManager>();
        image = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.up = shelf.transform.position - transform.position;//points toward shelf
        image.color = new Color(image.color.r, image.color.g, image.color.b, user.ScannerTimer);//fade out

        if (!shelf.filled || user.ScannerTimer < 0)
            Destroy(gameObject);
    }
}
