using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class HunterAI : MonoBehaviour
{
    [Header("Unity Handles")]
    [SerializeField] NavMeshAgent hunterAgent;
    [SerializeField] Transform namelessPlayer;
    [SerializeField] LayerMask ground, player;
    [SerializeField] Vector3 walkingPoint;

    [Header("Booleans")]
    [SerializeField] bool walkingSet;
    [SerializeField] bool hasTakenColour;
    [SerializeField] bool playerInSight, canTakeColour;

    [Header("Floats")]
    [SerializeField] float walkSetRange;
    [SerializeField] float timeBetweenTakingColour;
    [SerializeField] float sightRange, takeColourRange;

	private void Awake()
	{
        namelessPlayer = GameObject.FindWithTag("Player").transform;
        hunterAgent = GetComponent<NavMeshAgent>();
	}


    void Update()
    {
       playerInSight = Physics.CheckSphere(transform.position, sightRange, player);
       canTakeColour = Physics.CheckSphere(transform.position, takeColourRange, player);

        //Check the states
        if (!playerInSight && !canTakeColour)
            StandAround();
        if (playerInSight && !canTakeColour)
            ChasePlayer();
        if (playerInSight && canTakeColour)
            TakeColour();
    }

	#region States
     void StandAround()
	 {
        if (!walkingSet)
            SearchForPointsToStandAround();

        if (walkingSet)
            hunterAgent.SetDestination(walkingPoint);

        //Distance to standing point
        Vector3 disToStandPoint = transform.position - walkingPoint;

        //Point Reached
        if (disToStandPoint.magnitude < 1f)
            walkingSet = false;
	 }


    void ChasePlayer()
    {
        hunterAgent.SetDestination(namelessPlayer.position);
    }

    void TakeColour()
    {
        hunterAgent.SetDestination(transform.position);
        transform.LookAt(namelessPlayer);

        if(!hasTakenColour)
		{
            //Hunter Will Take Colour if player has colour
            Debug.Log("Colour Taken");
            //Remove COlour from player (We should Lerp this)

            hasTakenColour = true;
            Invoke(nameof(ResetTakingColour), timeBetweenTakingColour);
		}
    }
    #endregion

    void SearchForPointsToStandAround()
	{
        float randomZ = Random.Range(-walkSetRange, walkSetRange);
        float randomX = Random.Range(-walkSetRange, walkSetRange);

        walkingPoint = new Vector3(transform.position.x + randomX, transform.position.y , transform.position.z + randomZ);

        if (Physics.Raycast(walkingPoint, -transform.up, 2f, ground))
            walkingSet = true;
    }

    void ResetTakingColour()
	{
        hasTakenColour = false;
	}


	//Visualising
	private void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, takeColourRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
	}
}
