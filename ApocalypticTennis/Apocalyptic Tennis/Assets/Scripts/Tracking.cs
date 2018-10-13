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
    Vector3 Direction = new Vector3(0f, 0f, 1f);
    Vector3 DeltaDirectionVector = new Vector3(0f, 0f, 1f);
    float CachedTime = 0f;
    float Velocity = 0f;
    bool LerpDeltaValues = true;
    private LineRenderer TheLineRenderer;
    private Rigidbody TheRigidBody;

    public Vector3 GetDirectionVector() {
        return Direction;
    }

    public float GetVelocity() {
        return Velocity;
    }

    public float GetAcceleration() {
        return 0f;
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
        //Update rigid body position
        //TheRigidBody.position = transform.position;
        //Debug.Log(TheRigidBody.velocity.x + ", " + TheRigidBody.velocity.y + ", " + TheRigidBody.velocity.z);
        //Declare direction vectors
        Vector3[] TheDirectionVectors = new Vector3[2];
        //Set the direction vectors
        TheDirectionVectors[0] = transform.position;
        TheDirectionVectors[1] = transform.position + (Direction * LineLength);
        //Show line
        TheLineRenderer.SetPositions(TheDirectionVectors);
        //Update time
        CachedTime += Time.deltaTime;
        //Set velocity default
        Velocity = 0f;
        //Check if not same position
        if (PreviousLocation != transform.position) {
            //Set direction
            Vector3 DeltaDirection = (transform.position - PreviousLocation).normalized;
            //Store distance
            float Distance = Vector3.Distance(PreviousLocation, transform.position);
            //Check distance
            if (Distance > TheTargetedDistanceOfMovement) {
                //Set
                LerpDeltaValues = true;
                PreviousLocation = transform.position;
                DeltaDirectionVector = DeltaDirection;
                Velocity = Distance / Time.deltaTime;
                CachedTime = 0f;
            }
        }
        //Check if delta values was set
        if (LerpDeltaValues) {
            //Set direction
            Direction = Vector3.Lerp(Direction, DeltaDirectionVector, LerpingAlpha);
            //Set stored distance
            float FinalDistance = Vector3.Distance(DeltaDirectionVector, Direction);
            //Check distance if close enough
            if (FinalDistance <= 0.01f) {
                //Reset
                LerpDeltaValues = false;
            }
        }
	}

}
