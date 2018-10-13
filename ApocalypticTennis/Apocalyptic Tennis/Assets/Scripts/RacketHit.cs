using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketHit : MonoBehaviour {

    //Declare privates
    public Tracking RacketTracking;
    public float BallForceApplied = 500f;

    private void OnTriggerEnter(Collider other) {
        //Check name
        if (other.name == "Ball") {
            //Change
            PublicScript.BallInHand = false;
            //Remove parent
            other.transform.parent = null;
            //Fix scale
            other.transform.localScale = Vector3.one;
            //Change to collider
            other.GetComponent<SphereCollider>().isTrigger = false;
            //Change this to collider
            GetComponent<MeshCollider>().isTrigger = false;
            //Declare
            Rigidbody TheBallRigidbody = other.GetComponent<Rigidbody>();
            //Allow physics
            TheBallRigidbody.isKinematic = false;
            //Declare
            Tracking BallTracking = other.gameObject.GetComponent<Tracking>();
            //Apply force
            TheBallRigidbody.AddForce((RacketTracking.GetDirectionVector() * BallForceApplied) + (BallTracking.GetDirectionVector() * BallForceApplied));
        }
    }

    private void OnCollisionEnter(Collision collision) {
        //Check name
        if (collision.gameObject.name == "Ball") {
            //Change
            PublicScript.BallInHand = false;
            //Remove parent
            collision.gameObject.transform.parent = null;
            //Fix scale
            collision.gameObject.transform.localScale = Vector3.one;
            //Declare
            Rigidbody TheBallRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            //Allow physics
            TheBallRigidbody.isKinematic = false;
            //Apply force
            TheBallRigidbody.AddForce(collision.gameObject.GetComponent<Tracking>().GetDirectionVector() * 500f);
        }
    }

}