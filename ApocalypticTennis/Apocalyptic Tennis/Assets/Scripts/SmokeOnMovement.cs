using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeOnMovement : MonoBehaviour {

    public ParticleSystem SmokeSystem;
    public TrackingComponent BallTracker;
    public float MinSpeed = 1;

    private bool m_ParticlesOff = true;
	
	// Update is called once per frame
	void Update () {
        if(BallTracker) {
            if(BallTracker.GetAvgVelocity().magnitude >= MinSpeed) {
                if(m_ParticlesOff) {
                    Debug.Log("Particles on!");
                    ToggleOnParticles(true);
                }
            }
            else {
                if(!m_ParticlesOff) {
                    Debug.Log("Particles off!");
                    ToggleOnParticles(false);
                }
            }
        }
    }

    private void ToggleOnParticles(bool value) {
        if (!SmokeSystem)
            return;

        if(!m_ParticlesOff) {
            SmokeSystem.Stop(true);
            m_ParticlesOff = true;
        } else {
            SmokeSystem.Play(true);
            m_ParticlesOff = false;
        }
    }
}
