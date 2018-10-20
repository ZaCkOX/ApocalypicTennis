using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketHit : MonoBehaviour {

    //Declare publics
    public Tracking RacketTracking;
    public Rigidbody TheBallRigidbody;
    public Rigidbody TheRacketRigidbody;

    //Declare privates
    private Tracking BallTracking;

    private void Start() {
        //Set
        BallTracking = TheBallRigidbody.GetComponent<Tracking>();
    }

    private void OnTriggerEnter(Collider other) {
        //Check name
        if (other.name == "Ball (1)") {
            PublicScript.gtxtDebug.text += " HIT2";
        }
        //Check name
        if (other.name == "Ball") {
            //Remove parent
            other.transform.parent = null;
            //Fix scale
            other.transform.localScale = Vector3.one;
            //Allow physics
            // TheBallRigidbody.isKinematic = false;
            //Declare
            Vector3 resultVelocity = GetResultantVelocity(RacketTracking.GetDirectionVector() * RacketTracking.GetVelocity(), TheRacketRigidbody.mass, 
                BallTracking.GetDirectionVector() * BallTracking.GetVelocity(), TheBallRigidbody.mass);

            Vector3 ballForce = (resultVelocity * TheBallRigidbody.mass) / Time.deltaTime;

            PublicScript.gtxtDebug.text = "Ball Force = " + ballForce.ToString();

            //Apply force
            TheBallRigidbody.AddForce(ballForce);
            PublicScript.gtxtDebug.text += " HIT1";
        }
    }

    private Vector3 GetResultantVelocity(Vector3 v1, float m1, Vector3 v2, float m2) {
        Vector3 result = ((2f * m1) / (m1 + m2)) * v1 + ((m2 - m1) / (m1 + m2)) * v2;

        return result;
    }

}