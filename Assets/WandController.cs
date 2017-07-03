using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour {
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;

	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();	
	}
	
	// Update is called once per frame
	void Update () {
		if (controller == null) {
			Debug.Log("Controller not initalized");
			return;
		}

		if (controller.GetPressDown(gripButton)) {
			Debug.Log("Grip button was just pressed");
		}

		if (controller.GetPressUp(gripButton)) {
			Debug.Log("Grip button was just released");
		}

		if (controller.GetPressDown(triggerButton)) {
			Debug.Log("Trigger button was just pressed");
		}

		if (controller.GetPressUp(triggerButton)) {
			Debug.Log("Trigger button was just released");
		}
	}
}

