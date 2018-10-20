using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour {

    //Declare publics
    public float TheTargetedDistanceOfMovement = 1f;
    [Range(0f, 1f)]
    public float LerpingAlpha = 0.1f;
    public float LineLength = 0.25f;

    //Declare privates
    Vector3 PreviousLocation = Vector3.zero;
    Vector3 Direction = Vector3.zero;
    float Velocity = 0f;
    private LineRenderer TheLineRenderer;
    private Rigidbody TheRigidBody;

    public Vector3 GetDirectionVector() {
        return Direction;
    }

    public float GetVelocity() {
        return Velocity;
    }

    // Use this for initialization
    void Start () {
        //Set previous
        PreviousLocation = transform.position;
        //Set
        TheLineRenderer = GetComponent<LineRenderer>();
        //Set
        TheRigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        //Declare direction vectors
        Vector3[] TheDirectionVectors = new Vector3[2];
        //Set the direction vectors
        TheDirectionVectors[0] = transform.position;
        TheDirectionVectors[1] = transform.position + (Direction * LineLength);
        //Show line
        TheLineRenderer.SetPositions(TheDirectionVectors);

        //Check if not same position
        if (PreviousLocation != transform.position) {
            //Set direction
            Vector3 DeltaDirection = (transform.position - PreviousLocation).normalized;

            //Store distance
            float Distance = Vector3.Distance(PreviousLocation, transform.position);

            //Check distance
            Direction = DeltaDirection;
            PreviousLocation = transform.position;
            Velocity = Distance / Time.deltaTime;

        } else {

            Velocity = 0.0f;
            Direction = Vector3.zero;
        }
	}

}
