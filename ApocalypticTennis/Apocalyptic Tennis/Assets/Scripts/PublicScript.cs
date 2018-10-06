using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicScript : MonoBehaviour {

    public static SteamVR_Controller.Device[] gasvrSteamVRDevices = new SteamVR_Controller.Device[2]; //0 = left controller, 1 = right controller
    public static ControllerEvents gudcControllerEvents;
    public static GameObject[] gaobjControllers = new GameObject[2];
    public static bool gblnControllersReady = false;

}