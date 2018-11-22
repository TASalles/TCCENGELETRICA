using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BicycleController : MonoBehaviour
{
    private Rigidbody rb;
    public Transform centerOfMass;

    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelBL;
    public WheelCollider wheelBR;

    public GameObject FL;
    public GameObject BL;

    public float topSpeed = 250f;
    public float maxTorque = 200f;
    public float maxBrakeTorque = 2200;
    public float maxSteerAngle = 45f;

    public float currentSpeed;

    private float forwardAxis;
    private float turnAxis;
    private float brakeAxis;

    public float antiRoll = 5000f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.centerOfMass = centerOfMass.position;
    }

    private void FixedUpdate()
    {
        //BICYCLE MOVEMENT
        wheelFL.steerAngle = maxSteerAngle * turnAxis;
        wheelFR.steerAngle = maxSteerAngle * turnAxis;

        currentSpeed = 2 * 22 / 7 * wheelBL.radius * wheelBL.rpm * 60 / 100; //formula for calculating speed in rpm

        if (currentSpeed < topSpeed)
        {
            Debug.Log("Acelerando");
            wheelBL.motorTorque = maxTorque * forwardAxis; //run the wheel on the back
            wheelBR.motorTorque = maxTorque * forwardAxis;
        } //will not be accurate but will try to slow down the bike before top speed

        wheelFL.brakeTorque = maxBrakeTorque * brakeAxis;
        wheelFR.brakeTorque = maxBrakeTorque * brakeAxis;
        wheelBL.brakeTorque = maxBrakeTorque * brakeAxis;
        wheelBR.brakeTorque = maxBrakeTorque * brakeAxis;

        //BICYCLE ANTIROLL BARS
        WheelHit hit = new WheelHit();
        float travelL = 1f;
        float travelR = 1f;

        bool groundedL = wheelBL.GetGroundHit(out hit);
        if (groundedL)
            travelL = (-wheelBL.transform.InverseTransformPoint(hit.point).y - wheelBL.radius) / wheelBL.suspensionDistance;

        var groundedR = wheelBR.GetGroundHit(out hit);
        if (groundedR)
            travelR = (-wheelBR.transform.InverseTransformPoint(hit.point).y - wheelBR.radius) / wheelBR.suspensionDistance;

        var antiRollForce = (travelL - travelR) * antiRoll;

        if (groundedL)
            rb.AddForceAtPosition(wheelBL.transform.up * -antiRollForce, wheelBL.transform.position);

        if (groundedR)
            rb.AddForceAtPosition(wheelBR.transform.up * antiRollForce,
                   wheelBR.transform.position);
    }

    private void Update()
    {
        //BICYCLE MOVEMENT
        forwardAxis = Input.GetAxis("Vertical");
        turnAxis = Input.GetAxisRaw("HorizontalJoystick");
        Debug.Log(turnAxis);
        brakeAxis = Input.GetAxis("Jump");

        //Quaternion fq; //rotation and position of wheel collider
        //Vector3 fv;
        //wheelFL.GetWorldPose(out fv, out fq); //get wheel collider position and rotation
        //FL.transform.position = fv;
        //FL.transform.rotation = fq;

        //Quaternion bq; //rotation and position of wheel collider
        //Vector3 bv;
        //wheelBL.GetWorldPose(out bv, out bq); //get wheel collider position and rotation
        //BL.transform.position = bv;
        //BL.transform.rotation = bq;

        //Debug.Log("" + fq + ", " + bq);


    }
}
