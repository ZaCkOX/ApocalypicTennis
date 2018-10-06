using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {

    //Declare publics
    public GameObject[] Controllers = new GameObject[2];

    private void Start() {
        //Set
        PublicScript.gudcControllerEvents = GameObject.Find("[CameraRig]").GetComponent<ControllerEvents>();
        //Reset
        PublicScript.gblnControllersReady = false;
    }

    private void Update() {
        //Check when controllers are available
        if (Controllers[0].activeSelf && Controllers[1].activeSelf) {
            //Set controllers
            PublicScript.gaobjControllers[0] = Controllers[0];
            PublicScript.gaobjControllers[1] = Controllers[1];
            //Set controller devices
            PublicScript.gasvrSteamVRDevices[0] = SteamVR_Controller.Input((int)PublicScript.gaobjControllers[0].GetComponent<SteamVR_TrackedObject>().index);
            PublicScript.gasvrSteamVRDevices[1] = SteamVR_Controller.Input((int)PublicScript.gaobjControllers[1].GetComponent<SteamVR_TrackedObject>().index);
            //Set
            PublicScript.gblnControllersReady = true;
        }
    }

}