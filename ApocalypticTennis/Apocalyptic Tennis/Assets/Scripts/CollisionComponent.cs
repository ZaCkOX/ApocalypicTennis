using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComponent: MonoBehaviour
{
    public Transform trackingObject;
    public Rigidbody TrackingRigidbody;
    public float AcceptanceCollisionRadius = 12f;
    public float DampeningRatio = 1.0f;
    public float TrackingForceMultipler = 1f;
    public float TimeBetweenHits = 1f;
    public float MinForceMag = 100f;
    private float TimeBetweenHitsSubtract = 0f;
    private bool inFront = false;
    private bool inRange = false;
    private bool canHitBall = true;

    private TrackingComponent BaseTrackingComponent;
    private Transform TrackingTransform;

    // Use this for initialization
    void Start() {
        BaseTrackingComponent = GetComponent<TrackingComponent>();
        TrackingTransform = BaseTrackingComponent.transform;
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

        if (PublicScript.UseTimer(ref TimeBetweenHitsSubtract, TimeBetweenHits)) {
            canHitBall = true;
        }


        // A hit has been detected!
        if (inRange && inFront != nowInFront) {

            Debug.Log(TimeBetweenHitsSubtract);
            if (canHitBall) {
                HandleCollisionLogic();
                canHitBall = false;
            }
        }

        // Set the current values as the previous ones for the next hit detection
        inRange = nowInRange;
        inFront = nowInFront;
    }

    private void HandleCollisionLogic() {

        Debug.Log("A hit was made");

        TrackingComponent CollidingObject = TrackingRigidbody.transform.GetComponent<TrackingComponent>();

        Vector3 collidingForce = CollidingObject.GetMomentum() / Time.deltaTime;
        Vector3 colliderForce = BaseTrackingComponent.GetMomentum() / Time.deltaTime;
        Vector3 reflectedCollidingForce = -(Vector3.Dot(collidingForce, TrackingTransform.forward) * TrackingTransform.forward);
        Vector3 finalForce = (reflectedCollidingForce * DampeningRatio) + (colliderForce * TrackingForceMultipler);

        float mag = Vector3.Magnitude(finalForce);

        if(mag < MinForceMag) {
            finalForce.Normalize();
            finalForce *= MinForceMag;
        }

        TrackingRigidbody.AddForce(finalForce);

        TimeBetweenHitsSubtract = TimeBetweenHits;


    }

}