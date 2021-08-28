using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour : MonoBehaviour
{

    public List<Color> defaultColors = new List<Color>();
    public List<Color> newColours = new List<Color>();
    public Material[] colours, defaultColor;
    public MeshRenderer rend;



    private void Start()
    {
        rend = GetComponent<MeshRenderer>();


      

    }

	private void Update()
	{
        
        //Stores List of Colors
        for (int i = 0; i < colours.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rend.sharedMaterials[i].color = newColours[i];
        }
    }
}
