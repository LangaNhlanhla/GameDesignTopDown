using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour : MonoBehaviour
{
    [Header("External Scripts")]
    ShrineTrigger shrineTrigger;

    [Header("Generic Elements")]
    public List<Color> defaultColors = new List<Color>();
    public List<Color> newColours = new List<Color>();

    [Header("Unity Handles")]
    public Material[] colours, defaultColor;
    public MeshRenderer rend;


	private void Awake()
	{
        shrineTrigger = FindObjectOfType<ShrineTrigger>();
	}

	private void Start()
    {
        rend = GetComponent<MeshRenderer>();
        for (int i = 0; i < colours.Length; i++)
        {

            rend.sharedMaterials[i].color = newColours[i];
        }
    }

	private void Update()
	{
        for (int i = 0; i < defaultColor.Length; i++)
        {
            if (shrineTrigger.colourCollected)
                rend.sharedMaterials[i].color = defaultColors[i];

        }

        //Stores List of Colors
       
    }
}
