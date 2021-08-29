using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollider : MonoBehaviour
{
	[Header("Unity Handles")]
	[SerializeField] Collider[] ColliderObjs;

	[Header("Externa Scripts")]
	HidingMechanic hm;

	private void Start()
	{
		hm = FindObjectOfType<HidingMechanic>();
	}
	private void Update()
	{
		if(hm.currentlyHiding)
		{
			Disbable();
		}
		else
		{
			EnableColliders();
		}
	}
	public void Disbable()
	{
		for (int i = 0; i < ColliderObjs.Length; i++)
		{
			Debug.Log("Disable");
			ColliderObjs[i].enabled = false;
		}
	}

	public void EnableColliders()
	{
		for (int i = 0; i < ColliderObjs.Length; i++)
		{
			Debug.Log("Enable");
			ColliderObjs[i].enabled = true;
		}
	}
}
