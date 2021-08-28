using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
    [Header("Unity Handles")]
    public GameObject partcile;
    public GameObject cube;
    public GameObject prism;
    public GameObject Sphere;
    public ParticleSystem Part;

    [Header("Colour Input")]
    public Color color1;
    public Color defaultColour;
    public static int ColourIndex = 0;
    // public GameObject Current;
    //bool isPart;
    [Header("Booleans")]
    public bool hasColour;

    // Start is called before the first frame update
    void Start()
    {
       // Current = partcile;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ColourIndex== 1)
        {
            Debug.Log("Space works");
            if (partcile.activeInHierarchy == true)
            {
                Debug.Log("Particle");
                partcile.SetActive(false);
                cube.SetActive(true);
            }
            else
            {
                partcile.SetActive(true);
                cube.SetActive(false);
            }
            
        }
        //Colour change 
        CheckColour();
    }
    void CheckColour()
    {
        if (ColourIndex == 0)
        {
            Debug.Log("Default");
            partcile.SetActive(true);
            cube.SetActive(false);
            var m = Part.main;
            m.startColor = new ParticleSystem.MinMaxGradient(Color.black, defaultColour);
        }
        if (ColourIndex == 1)
        {
            Debug.Log("Cube");
            var m = Part.main;
            m.startColor = new ParticleSystem.MinMaxGradient(Color.black, color1);
        }
        if (ColourIndex == 2)
        {
            Debug.Log("Sphere");
        }
        if (ColourIndex == 3)
        {
            Debug.Log("Prism");
        }
    }

   private void OnTriggerEnter(Collider other)
    {
        if(other.tag== ("Pickup"))
        {
            if (ColourIndex == 1)
            {
                Debug.Log("Cube");
                partcile.SetActive(false);
                cube.SetActive(true);

            }
            if (ColourIndex == 2)
            {
                Debug.Log("Sphere");
            }
            if (ColourIndex == 3)
            {
                Debug.Log("Prism");
            }
        }
    }
   
}
