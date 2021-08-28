using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[Header("Unity Handles")]
	[SerializeField] GameObject ggPanel;

	[Header("Booleans")]
	[SerializeField] bool paused;

	private void Awake()
	{
		ggPanel.SetActive(false);
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !paused)
		{
			PauseGame();
		}
	}

	public void Resume()
	{
		Debug.Log("Resumed");
		ggPanel.SetActive(false);
		paused = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void PauseGame()
	{
		Debug.Log("Paused");
		ggPanel.SetActive(true);
		paused = true;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void ExitGame()
	{
		Debug.Log("GGs");
		Application.Quit();
	}
}
