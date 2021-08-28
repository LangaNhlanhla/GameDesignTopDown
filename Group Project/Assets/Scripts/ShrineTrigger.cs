using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineTrigger : MonoBehaviour
{
    [Header("External Scripts")]
    [SerializeField] ChangePlayer changePlayerScript;

	[Header("Booleans")]
	public bool colourCollected;

	[Header("Unity Handles")]
	[SerializeField] Material defaultSkybox, newSkybox;
	[SerializeField] GameObject GoodGameObject;

	[Header("Floats")]
	[SerializeField] float waitTimeTillDisabling;
	[SerializeField] float waitTimeTillEnabling;

    void Start()
    {
        changePlayerScript = FindObjectOfType<ChangePlayer>();
		defaultSkybox = RenderSettings.skybox;
		GoodGameObject.SetActive(false);
    }

	private void Update()
	{
		//TODO: Delete
		if (colourCollected)
			RenderSettings.skybox = newSkybox;
		else
			defaultSkybox = RenderSettings.skybox;
	}
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player") && changePlayerScript.hasColour)
		{
			Debug.Log("Display GG UI " + " -GGs");
			colourCollected = true;
			StartCoroutine(DisableObject());
			RenderSettings.skybox = newSkybox;
		}
	}

	IEnumerator DisableObject()
	{
		yield return new WaitForSeconds(waitTimeTillEnabling);
		GoodGameObject.SetActive(true);

		yield return new WaitForSeconds(waitTimeTillDisabling);
		GoodGameObject.SetActive(false);
	}
}
