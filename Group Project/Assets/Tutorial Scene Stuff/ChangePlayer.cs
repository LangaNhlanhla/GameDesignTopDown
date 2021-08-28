using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
    public GameObject partcile;
    public GameObject cube;

   // public GameObject Current;
    //bool isPart;
   
    // Start is called before the first frame update
    void Start()
    {
       // Current = partcile;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space works");
            if (partcile.active== true)
            {
                Debug.Log("Particle");
                partcile.active = false;
                cube.active = true;
            }
            else
            {
                partcile.active = true;
                cube.active = false;
            }
            
        }
    }
}
