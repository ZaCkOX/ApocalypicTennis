using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour {

    //Declare publics
    public GameObject[] Controllers = new GameObject[2];
    public Rigidbody TheBallRigidbody;
    public Rigidbody TheRacketRigidbody;
    public SphereCollider TheBallSphereCollider;
    public MeshCollider TheRacketMeshCollider;
    public float BallReturnSpeed = 0.1f;
    [Range(0.01f, 0.99f)]
    public float SlowMotionSpeed = 0.25f;
    public TextMeshPro VRDebug;
    public Vector3 RacketOffsetRotation = Vector3.zero;

    //Declare privates
    private Tracking BallTracking;
    private bool UseLetGoForce = false;

    private void Start() {
        //Reset time scale
        Time.timeScale = 1f;
        //Set
        PublicScript.gudcControllerEvents = GameObject.Find("[CameraRig]").GetComponent<ControllerEvents>();
        //Reset
        PublicScript.gblnControllersReady = false;
        //Set
        PublicScript.gtxtDebug = VRDebug;
        //Set
        BallTracking = TheBallRigidbody.GetComponent<Tracking>();
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
            //Move racket to follow controller
            TheRacketRigidbody.position = PublicScript.gaobjControllers[1].transform.position;
            TheRacketRigidbody.rotation = PublicScript.gaobjControllers[1].transform.rotation * Quaternion.Euler(RacketOffsetRotation.x, RacketOffsetRotation.y, RacketOffsetRotation.z);
            //Check for touch pad press
            if (PublicScript.gasvrSteamVRDevices[0].GetPress(SteamVR_Controller.ButtonMask.Touchpad) || 
            PublicScript.gasvrSteamVRDevices[1].GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
                //Reset scene
                SceneManager.LoadScene(0);
            }
            //Check for grip press
            if (PublicScript.gasvrSteamVRDevices[0].GetPress(SteamVR_Controller.ButtonMask.Grip)) {

                // Ball was grabbed
                UseLetGoForce = true;

                //Disable physics on ball
                TheBallRigidbody.isKinematic = true;

                //Bring to hand
                TheBallRigidbody.position = PublicScript.gaobjControllers[0].transform.position;

                //float Distance = Vector3.Distance(TheBallRigidbody.position, PublicScript.gaobjControllers[0].transform.position);
                //if(Distance < 0.01f) {
                //    TheBallRigidbody.position = Vector3.Lerp(TheBallRigidbody.position, PublicScript.gaobjControllers[0].transform.position, BallReturnSpeed);

                //} else {
                //    TheBallRigidbody.position = PublicScript.gaobjControllers[0].transform.position;
                //}

            } else {

                TheBallRigidbody.isKinematic = false;

                if (UseLetGoForce) {
                    Vector3 ballForce = (BallTracking.GetDirectionVector() * BallTracking.GetVelocity() * TheBallRigidbody.mass) / Time.deltaTime;

                    PublicScript.gtxtDebug.text = "Ball Force = " + ballForce.ToString();

                    //Apply force
                    TheBallRigidbody.AddForce(ballForce);

                    UseLetGoForce = false;
                }

            }

            //Check for grip press
            if (PublicScript.gasvrSteamVRDevices[1].GetPress(SteamVR_Controller.ButtonMask.Grip)) {
                //Set slow motion
                Time.timeScale = SlowMotionSpeed;
            } else {
                //Reset
                Time.timeScale = 1f;
            }
        }
    }

}