﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    public AIDestinationSetter target;
    public AIPath ai;
    public Seeker Thepath;
    public ItemManager inventory;

    // Update is called once per frame
    void Update()
    {
        inventory.grab = 0;
        if (inventory.atRegister)
            inventory.buying = true;
        else
            inventory.buying = false;
    
        if (ai.hasPath)
        {
            //rotations
            //Debug.Log(Thepath.GetCurrentPath().GetTotalLength());
            if (Thepath.GetCurrentPath().GetTotalLength() < 1.5)
               transform.rotation = new Quaternion(0, 0, target.target.rotation.z - 180, 0);//prvents it sticking on the side of the shelf
            else
                transform.up = ai.desiredVelocity;//sets rotation right


            if (ai.reachedEndOfPath)//when arrived at a shelf
            {
                Debug.Log("ended path");
                if (target.target.tag == "Shelf")
                    inventory.grab = 1;
            }
        }
        //finding targets
        if (inventory.items.Count < inventory.maxItems)
            target.target = FindClosestShelf().transform;
        else
            target.target = FindClosestRegister().transform;
    }

    public GameObject FindClosestShelf()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Shelf");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && go.GetComponentInParent<ItemDispenser>().filled)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    public GameObject FindClosestRegister()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Register");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
