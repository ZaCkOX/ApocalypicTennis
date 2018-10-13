using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {

    //Declare publics
    public GameObject[] Controllers = new GameObject[2];
    public Rigidbody TheBallRigidbody;
    public SphereCollider TheBallSphereCollider;
    public MeshCollider TheRacketMeshCollider;
    public float BallReturnSpeed = 0.1f;

    //Declare privates
    private bool UsingTheForce = false;

    private void Start() {
        //Set
        PublicScript.gudcControllerEvents = GameObject.Find("[CameraRig]").GetComponent<ControllerEvents>();
        //Reset
        PublicScript.gblnControllersReady = false;
    }

    private void Update() {
        //Check when controllers are available
        if (Controllers[0].activeSelf && Controllers[1].activeSelf && !PublicScript.gblnControllersReady) {
            //Set controllers
            PublicScript.gaobjControllers[0] = Controllers[0];
            PublicScript.gaobjControllers[1] = Controllers[1];
            //Set controller devices
            PublicScript.gasvrSteamVRDevices[0] = SteamVR_Controller.Input((int)PublicScript.gaobjControllers[0].GetComponent<SteamVR_TrackedObject>().index);
            PublicScript.gasvrSteamVRDevices[1] = SteamVR_Controller.Input((int)PublicScript.gaobjControllers[1].GetComponent<SteamVR_TrackedObject>().index);
            //Set
            PublicScript.gblnControllersReady = true;
        }
        //Check if controllers are ready
        if (PublicScript.gblnControllersReady) {
            //Check if ball not in hand
            if (!PublicScript.BallInHand) {
                //Check for grip press
                if (PublicScript.gasvrSteamVRDevices[0].GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
                    //Disable physics on ball
                    TheBallRigidbody.isKinematic = true;
                    //Turn on triggers
                    TheBallSphereCollider.isTrigger = true;
                    TheRacketMeshCollider.isTrigger = true;
                    //Set
                    UsingTheForce = true;
                }
            }
        }
        //Check if need to return the ball
        if (UsingTheForce) {
            //Bring to hand
            TheBallRigidbody.position = Vector3.Lerp(TheBallRigidbody.position, PublicScript.gaobjControllers[0].transform.position, BallReturnSpeed);
            //Check distance
            if (Vector3.Distance(TheBallRigidbody.position, PublicScript.gaobjControllers[0].transform.position) <= 0.1f) {
                //Change parent
                TheBallRigidbody.transform.parent = PublicScript.gaobjControllers[0].transform;
                //Move into hand
                TheBallRigidbody.position = PublicScript.gaobjControllers[0].transform.position;
                //Set
                UsingTheForce = false;
                //Set
                PublicScript.BallInHand = true;
            }
        }
    }

}