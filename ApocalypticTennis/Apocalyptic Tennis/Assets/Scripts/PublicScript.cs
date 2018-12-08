using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PublicScript : MonoBehaviour {

    public static SteamVR_Controller.Device[] gasvrSteamVRDevices = new SteamVR_Controller.Device[2]; //0 = left controller, 1 = right controller
    public static ControllerEvents gudcControllerEvents;
    public static GameObject[] gaobjControllers = new GameObject[2];
    public static bool gblnControllersReady = false;
    public static TextMeshPro gtxtDebug;

    public static bool UseTimer(ref float TimeOfTimer, float TimeReset = 0f) {
        if (TimeOfTimer > 0f) {
            TimeOfTimer -= Time.deltaTime;
        }
        if (TimeOfTimer <= 0f) {
            if (TimeReset != 0f) {
                TimeOfTimer = TimeReset;
            }
            return true;
        } else {
            return false;
        }
    }

}