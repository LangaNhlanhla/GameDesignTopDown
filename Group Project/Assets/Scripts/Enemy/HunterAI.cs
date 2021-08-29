using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class HunterAI : MonoBehaviour
{
    [Header("Unity Handles")]
    [SerializeField] NavMeshAgent hunterAgent;
    public Transform namelessPlayer;
    [SerializeField] ChangePlayer changePlayerScript;
    [SerializeField] LayerMask ground, player, environmment;
    [SerializeField] Vector3 walkingPoint;
    [SerializeField] GameObject pickUp;

    [Header("Booleans")]
    public static bool hasTakenColour;
    [SerializeField] bool walkingSet;
    [SerializeField] bool canTakeColour;
    public bool playerInSight;

    [Header("Floats")]
    [SerializeField] float walkSetRange;
    [SerializeField] float timeBetweenTakingColour;
    [SerializeField] float takeColourRange;
    
    [Range(0,360f)]
    public float angle;

    [Range(0, 360f)]
    public float sightRange;

    private void Awake()
	{
        namelessPlayer = GameObject.FindWithTag("Player").transform;
        hunterAgent = GetComponent<NavMeshAgent>();
        changePlayerScript = FindObjectOfType<ChangePlayer>();
        pickUp = GameObject.FindGameObjectWithTag("Pickup");
	}


    void Update()
    {
       //playerInSight = Physics.CheckSphere(transform.position, sightRange, player);
       canTakeColour = Physics.CheckSphere(transform.position, takeColourRange, player);

        //Ccheccck
        AngleSight();

        //Check the states
        if (!playerInSight && !canTakeColour)
            StandAround();
        if (playerInSight && !canTakeColour)
            ChasePlayer();
        if (playerInSight && canTakeColour && changePlayerScript.hasColour)
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
        Debug.Log("Chasing");
        hunterAgent.SetDestination(namelessPlayer.position);
    }

    void TakeColour()
    {
        hunterAgent.SetDestination(transform.position);
        transform.LookAt(namelessPlayer);

        if(!hasTakenColour)
		{
            //Hunter Will Take Colour if player has colour
            pickUp.SetActive(true);
            Pickup pick = pickUp.GetComponent<Pickup>();
            pick.isColour = false;
            ChangePlayer.ColourIndex = 0;
           // changePlayerScript.hasColour = false;
            //Figure out Picking Up Boolean
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
    void AngleSight()
	{
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, sightRange, player);

        if (rangeChecks.Length != 0)
        {
            Transform playerTar = rangeChecks[0].transform;
            Vector3 dir = (playerTar.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dir) < angle / 2)
            {
                float dis = Vector3.Distance(transform.position, playerTar.position);

                if (!Physics.Raycast(transform.position, dir, dis, environmment))
                {
                    playerInSight = true;
                }
                else
                    playerInSight = false;
            }
            else
                playerInSight = false;
        }
        else if (playerInSight)
            playerInSight = false;
	}

    public void ResetTakingColour()
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

