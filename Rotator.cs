using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Rotator : MonoBehaviour
{
    public float acceleration = 125f;
    public float velocity = 0f;
    public float xRotation;
    
    // Clamp values
    public float maxRadialSpeed = 600f;
    private float minRadialSpeed = 0f;


    // Update is called once per frame
    void Update()
    {

        rotationAcceleration();
        rotationDeceleration();
    }

    

    void rotationAcceleration()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                velocity += acceleration * Time.deltaTime;
                xRotation += velocity * Time.deltaTime;
                transform.Rotate(new Vector3(xRotation * Time.deltaTime, 0, 0));
                if (xRotation >= maxRadialSpeed)
                {
                    xRotation = maxRadialSpeed;
                    transform.Rotate(new Vector3(xRotation * Time.deltaTime, 0, 0));
                }
            }
        }

    void rotationDeceleration()
    {
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            if (velocity > 0f && xRotation > 0f)
            { 
                velocity -= acceleration * Time.deltaTime;
                xRotation -= velocity * Time.deltaTime; 
                transform.Rotate(new Vector3(xRotation * Time.deltaTime, 0f, 0f));
                if (velocity <= 0f) { 
                    velocity = 0f;
                    xRotation = minRadialSpeed;
                    transform.Rotate(new Vector3(xRotation * Time.deltaTime, 0f, 0f));
                }  
            }
           
        }
    }





}


