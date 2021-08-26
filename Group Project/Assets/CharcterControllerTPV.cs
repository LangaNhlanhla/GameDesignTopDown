using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CharacterController))]

public class CharcterControllerTPV : MonoBehaviour
{
    public CharacterController controller;
    [Header("Movement")]
    public float Speed;
    public float Jump;

    [Header("Camera Input")]
    public Transform Camera;
    public float CamLerp= 0.5f;
    float Torque;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true ;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        float HoriInput = Input.GetAxisRaw("Horizontal");
        float VertiInput = Input.GetAxisRaw("Vertical");

        Vector3 Direction = new Vector3(HoriInput, 0f, VertiInput).normalized;

        if(Direction.magnitude>= 0.1f)
        {
            float Angle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
            float angle_ = Mathf.SmoothDampAngle(transform.eulerAngles.y, Angle, ref Torque, CamLerp);
            transform.rotation = Quaternion.Euler(0f, angle_, 0f);
            Vector3 move = Quaternion.Euler(0f, Angle, 0f) * Vector3.forward;
            controller.Move(move.normalized * Speed * Time.deltaTime);
        }
        // Jumping 
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.CheckSphere(GroundCheck.position, 0.1f, Ground))
            {
               
            }

        }*/
    }
}
