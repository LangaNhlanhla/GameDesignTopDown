using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingMechanic : MonoBehaviour
{
    [Header("Unity Handles")]
    [SerializeField] LayerMask namelessPlayer;
    [SerializeField] LayerMask hidingSpot, hunterEnemy;

    [Header("Booleans")]
    [SerializeField] bool canHide;
    [SerializeField] bool currentlyHiding;

    [Header("Generic Elements")]
    [SerializeField] string nameOfObjectToHideIn;

    // Update is called once per frame
    void Update()
    {
        if(canHide && Input.GetKeyDown(KeyCode.LeftControl))
		{
            Physics.IgnoreLayerCollision(namelessPlayer, hunterEnemy, true);

            //Hide Player and Play Animation/Camera Stuff/sound Here

            currentlyHiding = true;
		}
        else
		{
            Physics.IgnoreLayerCollision(namelessPlayer, hunterEnemy, false);

            //Player Stops Hiding and Play Animation/sound Here

            currentlyHiding = false;
        }
    }

	private void FixedUpdate()
	{
        if (!currentlyHiding)
            Debug.Log("Player Moves");
        else
            Debug.Log("StopPlayerMovement" + " Is Hiding");
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag(nameOfObjectToHideIn))
            canHide = true;
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(nameOfObjectToHideIn))
            canHide = false;
    }
}
