using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollider : MonoBehaviour
{
	[Header("Unity Handles")]
	[SerializeField] GameObject[] ColliderObjs;
   public void Disbable()
	{
		for (int i = 0; i < ColliderObjs.Length; i++)
		{
			Debug.Log("Disable");
			ColliderObjs[i].SetActive(false);
		}
	}

	public void EnableColliders()
	{
		for (int i = 0; i < ColliderObjs.Length; i++)
		{
			Debug.Log("Enable");
			ColliderObjs[i].SetActive(true);
		}
	}
}
