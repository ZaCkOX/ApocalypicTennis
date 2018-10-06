using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketHit : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        //Check name
        if (other.name == "Ball") {
            if(PublicScript.gudcControllerEvents.Controls[0].Trigger) {

            }
        }
    }

}