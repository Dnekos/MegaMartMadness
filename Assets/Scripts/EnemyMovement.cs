﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;

public class EnemyMovement : MonoBehaviour
{
    AIDestinationSetter DestinationSetter;
    AIPath ai;
    Seeker Thepath;
    ItemManager inventory;
    public int pathtag;

    private void Start()
    {
        DestinationSetter = GetComponent<AIDestinationSetter>();
        ai = GetComponent<AIPath>();
        Thepath = GetComponent<Seeker>();
        inventory = GetComponent<ItemManager>();

        //adding the tag to seeker, so that it knows it can walk on itself
        //|= is a bitmask version of +=, no idea why
        Thepath.traversableTags |= pathtag;
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.heldPU != Power_Ups.Empty)
            UsePowerups();

        //sets the area just a bit bigger than the seeker to default tag
        GraphUpdateObject bigguo = new GraphUpdateObject(new Bounds(transform.position, 
            new Vector3(ai.radius, ai.radius) * 1.1f));
        bigguo.modifyTag = true;
        bigguo.setTag = 0;
        AstarPath.active.UpdateGraphs(bigguo);

        //sets the area in the seeker to P1 tag
        GraphUpdateObject guo = new GraphUpdateObject(new Bounds(transform.position, 
            new Vector3(ai.radius, ai.radius)));
        guo.modifyTag = true;
        guo.setTag = pathtag;

        AstarPath.active.UpdateGraphs(guo);

        inventory.grab = 0;
        if (inventory.atRegister)
            inventory.buying = true;
        else
            inventory.buying = false;

        if (ai.hasPath)
        {
            //rotation
            /*if (ai.remainingDistance < 1.5) //depricated bug fix, but not deleted in case bug comes up again
                transform.rotation = new Quaternion(0, 0, target.target.rotation.z - 180, 0);//prvents it sticking on the side of the shelf
                else*/
            transform.up = ai.desiredVelocity;//sets rotation right


            if (ai.reachedEndOfPath)//when arrived at a shelf
            {
                Debug.Log("ended path");
                if (DestinationSetter.target.tag == "Shelf")
                    inventory.grab = 1;
                SetDestination();
            }
        }
        else
            SetDestination();
    }

    private GameObject FindClosestShelf()
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
    private GameObject FindRandomShelf()
    {
        GameObject[] shelves = GameObject.FindGameObjectsWithTag("Shelf");
        GameObject targetShelf = shelves[Random.Range(0, shelves.Length)];
        while (!targetShelf.GetComponentInParent<ItemDispenser>().filled)
        {
            targetShelf = shelves[Random.Range(0, shelves.Length)];
        }
        return targetShelf;
    }
    private GameObject FindClosestRegister()
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

    private void SetDestination()
    {
        //finding targets
        if (inventory.items.Count == 0)
            DestinationSetter.target = FindRandomShelf().transform;
        else if (inventory.items.Count < inventory.maxItems)
            DestinationSetter.target = FindClosestShelf().transform;
        else
        {
            Debug.Log("looking for register");
            DestinationSetter.target = FindClosestRegister().transform;
        }
    }

    private void UsePowerups()
    {
        if (inventory.heldPU == Power_Ups.Grabber)
        {
            if (Physics2D.OverlapCircleAll(transform.position, inventory.GrabRange, 9).Length != 0)
                inventory.UsePowerup();
        }
        else
            inventory.UsePowerup();
    }
}