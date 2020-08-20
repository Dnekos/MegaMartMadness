using System.Collections;
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
        //stop logs
        AstarPath.active.logPathResults = PathLog.None;

        //setting component variables
        DestinationSetter = GetComponent<AIDestinationSetter>();
        ai = GetComponent<AIPath>();
        Thepath = GetComponent<Seeker>();
        inventory = GetComponent<ItemManager>();

        //adding the tag to seeker, so that it knows it can walk on itself
        //|= is a bitmask version of +=, no idea why
        Thepath.traversableTags |= pathtag;
    }

    // Update used for setting destinations and performing actions
    void Update()
    {
        //enemy attempts to use powerups as soon as it holds them
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

        //updates node tags
        AstarPath.active.UpdateGraphs(guo);

        //grab/buying actions
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
                if (DestinationSetter.target.tag == "Shelf") //if at shelf, grab
                    inventory.grab = 1;
                SetDestination();
            }
        }
        else//basically only happens at the beginning of round, or if something jank happens
            SetDestination();
    }

    /// <summary>
    /// finds closest object based on tag, not the most efficient
    /// if object contains an ItemDispenser, filters out untagged objects
    /// </summary>
    /// <returns></returns>
    private Transform FindClosestObject(string tag)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            //checks to see, if it is a shelf, if it is filled, and skips if not
            try { if (go.GetComponentInParent<ItemDispenser>().filled == false) continue; }
            catch { };

            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest.transform;
    }
    /// <summary>
    /// finds closest shelf that is filled
    /// </summary>
    /// <returns></returns>
    private Transform FindRandomShelf()
    {
        GameObject[] shelves = GameObject.FindGameObjectsWithTag("Shelf");
        GameObject targetShelf;
        do
        {
            targetShelf = shelves[Random.Range(0, shelves.Length)];
        } while (!targetShelf.GetComponentInParent<ItemDispenser>().filled);
        return targetShelf.transform;
    }

    /// <summary>
    /// calls appropriate 'Find' function, based on inventory count
    /// </summary>
    private void SetDestination()
    {
        //first shelf is random
        if (inventory.items.Count == 0)
            DestinationSetter.target = FindRandomShelf().transform;
        else if (inventory.items.Count < inventory.maxItems) //sequential shelves are based on distance
            DestinationSetter.target = FindClosestObject("Shelf");
        else //when filled, return to register
        {
            Debug.Log("looking for register");
            DestinationSetter.target = FindClosestObject("Register");
        }
    }

    /// <summary>
    /// calls inventory.UsePowerup, checks if targets are in range for Grabber first
    /// </summary>
    private void UsePowerups()
    {
        if (inventory.heldPU == Power_Ups.Grabber)
        { //if powerup is Grabber, checks each frame if a player is in range, 9 is the player layermask
            if (Physics2D.OverlapCircleAll(transform.position, inventory.GrabRange, 9).Length != 0)
                inventory.UsePowerup();
        }
        else //if not grabber, uses instantly
            inventory.UsePowerup();
    }
}