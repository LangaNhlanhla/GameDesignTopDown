using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("External Scripts")]
    public ChangePlayer changePlayerScript;

    [Header("Booleans")]
    public bool isColour;

    [Header("Integers")]
    public int ActualIndex = -1;

    [Header("Unity Handles")]
    public GameObject Beam;

    // Start is called before the first frame update
    void Start()
    {
        isColour = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isColour)
        {
            Beam.SetActive(true);
            gameObject.SetActive(true);
        }

        changePlayerScript.hasColour = isColour;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "Player" && !isColour)
        {
            
            Debug.Log("Colour Obtain");
            isColour = true;
            changePlayerScript.hasColour = isColour;
            ChangePlayer.ColourIndex = ActualIndex;
            // set active...
            Beam.SetActive(false);
            gameObject.SetActive(false);
        }
        
    }
}
