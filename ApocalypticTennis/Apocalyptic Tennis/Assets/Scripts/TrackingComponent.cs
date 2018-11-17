using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrackingComponent: MonoBehaviour
{

    // Public Decls
    public Rigidbody Rbody;
    public int NumOfTrackedIterations = 10;
    public float deltaTime = 0f;

    // Private Decls
    private Queue<Vector3> positions = new Queue<Vector3>();
    private Queue<float> deltaTimes = new Queue<float>();

    // Use this for initialization
    void Start() {
        positions.Enqueue(transform.position);
        deltaTimes.Enqueue(0);
    }

    // Update is called once per frame
    void Update() {
        positions.Enqueue(transform.position);
        deltaTimes.Enqueue(Time.deltaTime);

        if (positions.Count > NumOfTrackedIterations) {
            positions.Dequeue();
            deltaTimes.Dequeue();
        }

        //LineRenderer OjectLineRender = transform.GetComponent<LineRenderer>();
        //OjectLineRender.SetPosition(0, transform.position);
        //OjectLineRender.SetPosition(1, transform.position + GetForward(true));

        deltaTime = Time.deltaTime;
    }

    // Get the Forward Vector of the object
    public Vector3 GetForward(bool normalize = false) {
        Vector3 forward = Vector3.zero;
        if (positions.Count >= 2) {
            forward = positions.ElementAt(positions.Count - 1) - positions.ElementAt(positions.Count - 2);

            if (normalize) {
                forward.Normalize();
            }
        }

        return forward;
    }

    public Vector3 GetAvgVelocity() {
        Vector3 avg = Vector3.zero;

        for (int x = 0; x < positions.Count - 1; x++) {
            // final - init
            Vector3 dx = positions.ElementAt(x) - positions.ElementAt(x + 1);
            float dt = deltaTimes.ElementAt(x + 1);
            avg += dx / dt;
        }

        avg /= positions.Count;

        return avg;
    }

    public Vector3 GetVelocity() {
        return GetForward() / deltaTimes.ElementAt(positions.Count - 1);
    }

    public Vector3 GetMomentum() {
        return GetVelocity() * 0.09f; //Rigidbody was on racket gameobject, Rbody.mass; //Deleted rigid body for testing, used mass 0.09, ang 0, ang drag 0, no gravity, no kinematic, inter none, collision continuous dynamic
    }

    public Vector3 GetAvgMomentum() {
        return GetAvgVelocity() * Rbody.mass;
    }
}
