using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ControllerState
{
    public bool Trigger;
    public bool Grips;
    public bool TouchPad;
    public bool XButton;
    public bool AButton;
    public bool YButton;
    public bool BButton;
}

public class ControllerEvents : MonoBehaviour {

    //Declare publics
    public ControllerState[] Controls = new ControllerState[2];

    private void Update() 
    {
        //Check if controllers are ready
        if (PublicScript.gblnControllersReady) 
        {
            //Loop through the two controllers
            for (int intLoop = 0; intLoop < 2; intLoop++) {

                Controls[intLoop].Trigger = PublicScript.gasvrSteamVRDevices[intLoop].GetPress(SteamVR_Controller.ButtonMask.Trigger);
                Controls[intLoop].Grips = PublicScript.gasvrSteamVRDevices[intLoop].GetPress(SteamVR_Controller.ButtonMask.Grip);
                Controls[intLoop].TouchPad = PublicScript.gasvrSteamVRDevices[intLoop].GetPress(SteamVR_Controller.ButtonMask.Touchpad);
                // Controls[intLoop].XButton = PublicScript.gasvrSteamVRDevices[intLoop].GetPress(Valve.VR.EVRButtonId.);
                Controls[intLoop].AButton = PublicScript.gasvrSteamVRDevices[intLoop].GetPress(Valve.VR.EVRButtonId.k_EButton_A);
                Controls[intLoop].YButton = PublicScript.gasvrSteamVRDevices[intLoop].GetPress(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
                //Controls[intLoop].BButton = PublicScript.gasvrSteamVRDevices[intLoop].GetPress(SteamVR_Controller.ButtonMask.Touchpad);
            }
        }
    }

}