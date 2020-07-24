using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;

public class EnemyMovement : MonoBehaviour
{
    public AIDestinationSetter target;
    public AIPath ai;
    public Seeker Thepath;
    public ItemManager inventory;
    public int pathtag;

    private void Start()
    {
        //adding the tag to seeker, so that it knows it can walk on itself
        //|= is a bitmask version of +=, no idea why
        Thepath.traversableTags |= pathtag;
    }

    // Update is called once per frame
    void Update()
    {
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
        else
        {
            //finding targets
            if (inventory.items.Count < inventory.maxItems)
                //target.target = FindClosestShelf().transform;
                target.target = FindRandomShelf().transform;
            else
                target.target = FindClosestRegister().transform;
        }
    }

    /*public GameObject FindClosestShelf()
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
    }*/
    public GameObject FindRandomShelf()
    {
        GameObject[] shelves = GameObject.FindGameObjectsWithTag("Shelf");
        GameObject targetShelf = shelves[Random.Range(0, shelves.Length)];
        while (!targetShelf.GetComponentInParent<ItemDispenser>().filled)
        {
            targetShelf = shelves[Random.Range(0, shelves.Length)];
        }
        return targetShelf;
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
