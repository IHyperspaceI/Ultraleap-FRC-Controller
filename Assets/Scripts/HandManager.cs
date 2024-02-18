using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class HandManager : MonoBehaviour
{
    public LeapServiceProvider provider;
    public GameObject rightWrist;
    public GameObject leftWrist;

    [Header("Tuning")]
    public float inputMultiplier;
    public float xySensitivity;
    public float zSensitivity;

    private Vector3 handPosOffset;
    private Vector3 handRotOffset;

    [Header("Output")]
    public string currentPose = "None";

    private Vector3 handPosition = Vector3.zero;
    private Vector3 handRotation = Vector3.zero;

    private Transform handTransform;

    // Methods called by Leap events
    public void FistPose()
    {
        currentPose = "Fist";
    }

    public void OpenPalmPose()
    {
        currentPose = "OpenPalm";
    }

    public void OKPose()
    {
        currentPose = "OK";
    }

    public void PointPose()
    {
        currentPose = "Point";
    }


    // Actual public interface
    public Vector3 getPosition()
    {
        return handPosition;
    }

    public Vector3 getRotation()
    {
        return handRotation;
    }

    public string getPose()
    {
        return currentPose;
    }



    void Update()
    {
        //Prioritize the right hand, but if only the left hand exists, use that:
        if (rightWrist.transform.parent.gameObject.activeSelf == false)
        {
            handTransform = leftWrist.transform;
        }
        else
        {
            handTransform = rightWrist.transform;
        }


        //Zero the output for tuning:
        if (Input.GetKeyDown(KeyCode.Space))
        {
            handPosOffset = handTransform.position;
            /*
            handPosOffset = Vector3.zero;
            handRotOffset = Vector3.zero;
            // reset handPosition and handRotation
            handPosition = Vector3.zero;
            handRotation = Vector3.zero;
            */
        }


        handRotation = handRotation - handRotOffset;

        /*
        handPosition = new Vector3(
            (handTransform.position.x - handPosOffset.x) * xySensitivity * inputMultiplier,
            (handTransform.position.y) * zSensitivity * inputMultiplier,
            (handTransform.position.z - handPosOffset.z) * xySensitivity * inputMultiplier
        ); 

        handRotation = new Quaternion(
            (int)Mathf.Round(handTransform.rotation.x * inputMultiplier),
            (int)Mathf.Round(handTransform.rotation.y * inputMultiplier),
            (int)Mathf.Round(handTransform.rotation.z * inputMultiplier),
            (int)Mathf.Round(handTransform.rotation.w * inputMultiplier)
        ).eulerAngles + handRotOffset;
        */

        handPosition = new Vector3(
            handTransform.position.x,
            handTransform.position.y,
            handTransform.position.z
        );


        handRotation = new Quaternion(
            handTransform.rotation.x,
            handTransform.rotation.y,
            handTransform.rotation.z,
            handTransform.rotation.w
        ).eulerAngles;

        //Debug.Log(handRotation);
    }
}