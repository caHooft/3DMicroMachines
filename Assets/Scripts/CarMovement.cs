using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//makes the  public variables not appear in the inspector
//entire class is make for easy acces to these variables
[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class CarMovement : MonoBehaviour
{

    public int lap;
    public GameObject respawnPoint;
    
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public WheelCollider groundedCheck;
    private BoxCollider boxCol;
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;

    public int playernumber;

    public Rigidbody rigidbodyCar;
    private float brakeTorque = float.MaxValue;
    private Vector3 latestCheckPoint;
    public Quaternion latestCheckPointR;

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        //checks if colliders have been added otherwise it doesn't continue
        if (collider.transform.childCount == 0)
        {
            return;
        }
        //gets the collider transform  and sets it to visual wheel
        Transform visualWheel = collider.transform.GetChild(0);

        //sets a position
        Vector3 position;
        //sets a rotation
        Quaternion rotation;
        //gets the position of the collider
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        //downforce for the car (spoiler)
        rigidbodyCar.AddForce(0, -75000, 0);

        //Creates a new float that gets the input of the vertical axis (Either 1, 0 or -1) and multiplies it by the maxMotorTorque variable.
        float motor = Input.GetAxis("Vertical"+ playernumber) * maxMotorTorque;

        //Creates a new float that gets the input of the horizontal axis (Either 1, 0 or -1) and multiplies it by the maxSteeringAngle variable.
        float steering = Input.GetAxis("Horizontal"+ playernumber) * maxSteeringAngle;

        //for each axleInfo in the the list this forloop checks if it is part of the steering or part of the motor.
        foreach (AxleInfo axleInfo in axleInfos)
        {
            //if the axleInfo is part of the steering send the steerAngle of the wheels to steering
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }

            //if the axleInfo is part of the motor send the motorTorque of the wheels to motor
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            
        }
    }

    public void Update()
    {
        //if the player releases the configured brake button call the NoHandBrake function
        if (Input.GetButtonUp("Brake" + playernumber))
        {
            NoHandbrake();
        }

        //if the player presses the configured brake button and isGrounded returns true the handbrake function is called
        if (Input.GetButton("Brake" + playernumber) && groundedCheck.isGrounded)
        {
            Handbrake();
        }

        //Log the brakeTorque of the frontLeftWheel 
       
   }
      
    //this function sets the brakeTorque of the frontLeftWheel and the frontRightWheel to The variable brakeTorque
    public void Handbrake()
    {
        
        if (Input.GetButtonDown("Brake" + playernumber) && groundedCheck.isGrounded)
        {
            frontLeftWheel.brakeTorque = brakeTorque;
            frontRightWheel.brakeTorque = brakeTorque;
                                        
        }

    }

    //this function sets the frontLeftWheel and the frontRightWheel brakeTorque to zero, this way the player can continue his way after braking
    public void NoHandbrake()
    {
        frontLeftWheel.brakeTorque = 0;
        frontRightWheel.brakeTorque = 0;
    }
}
