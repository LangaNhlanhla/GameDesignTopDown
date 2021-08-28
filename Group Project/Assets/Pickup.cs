using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isColour;
    public int ActualIndex = -1;
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "Player" && !isColour)
        {
            
            Debug.Log("Colour Obtain");
            isColour = true;
            ChangePlayer.ColourIndex = ActualIndex;
            // set active...
            Beam.SetActive(false);
            gameObject.SetActive(false);
        }
        
    }
}
