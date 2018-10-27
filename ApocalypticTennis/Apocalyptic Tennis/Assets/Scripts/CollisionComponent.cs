using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComponent: MonoBehaviour
{
    public Transform trackingObject;
    public Rigidbody TrackingRigidbody;
    public float AcceptanceCollisionRadius = 12f;
    private bool inFront = false;
    private bool inRange = false;

    private TrackingComponent BaseTrackingComponent;

    // Use this for initialization
    void Start() {
        BaseTrackingComponent = GetComponent<TrackingComponent>();
        CheckForHit();
    }

    // Update is called once per frame
    void Update() {
        CheckForHit();
    }

    //private void OnDrawGizmos() {
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, AcceptanceCollisionRadius);
    //    Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    //}

    private void CheckForHit() {
        // Get the current information of the tracked object
        Vector3 normal = transform.forward;
        Vector3 ab = trackingObject.position - transform.position;

        float proj = Vector3.Dot(ab, normal);

        // Is the ball in front (Assume being inside as in front)
        bool nowInFront = proj >= 0;

        // Check to see the distance from the normal
        Vector3 distanceVec = ab - (normal * proj);
        float distSquared = distanceVec.sqrMagnitude;

        // Is the ball is within range of an acceptable hit
        bool nowInRange = distSquared <= (AcceptanceCollisionRadius * AcceptanceCollisionRadius);


        // A hit has been detected!
        if (inRange && inFront != nowInFront) {
            Debug.Log("A hit was made");
            HandleCollisionLogic();
        }

        // Set the current values as the previous ones for the next hit detection
        inRange = nowInRange;
        inFront = nowInFront;
    }

    private void HandleCollisionLogic() {

        Debug.Log(Time.timeScale);
        TrackingRigidbody.AddForce((BaseTrackingComponent.GetMomentum() / Time.deltaTime) * Time.timeScale);

    }

}