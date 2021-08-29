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
    [SerializeField] Transform namelessPlayer;
    [SerializeField] ChangePlayer changePlayerScript;
    [SerializeField] LayerMask ground, player, environmment;
    [SerializeField] Vector3 walkingPoint;
    [SerializeField] GameObject pickUp;

    [Header("Booleans")]
    public static bool hasTakenColour;
    [SerializeField] bool walkingSet;
    [SerializeField] bool playerInSight, canTakeColour;

    [Header("Floats")]
    [SerializeField] float walkSetRange;
    [SerializeField] float timeBetweenTakingColour;
    [SerializeField] float sightRange, takeColourRange;
    
    [Range(0,360f)]
    [SerializeField] float radius, angle;

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
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, player);

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

[CustomEditor(typeof(HunterAI))]
public class DrawWireArc : Editor
{
    void OnSceneGUI()
    {
        Handles.color = Color.red;
        HunterAI myObj = (HunterAI)namelessPlayer;
        Handles.DrawWireArc(myObj.transform.position, myObj.transform.up, Vector3.forward, 180, myObj.angle);
        myObj.angle = (float)Handles.ScaleValueHandle(myObj.angle, myObj.transform.position + myObj.transform.forward * myObj.angle, myObj.transform.rotation, 1, Handles.ConeHandleCap, 1);

        Vector3 viewAngle01 = DirectionFromAngle(myObj.transform.eulerAngles.y, -myObj.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(myObj.transform.eulerAngles.y, myObj.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(myObj.transform.position, myObj.transform.position + viewAngle01 * myObj.radius);
        Handles.DrawLine(myObj.transform.position, myObj.transform.position + viewAngle02 * myObj.radius);



        if(myObj.playerInSight)
		{
            Handles.color = Color.blue;
            Handles.DrawLine(myObj.transform.position, myObj.namelessPlayer.position);
		}
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
