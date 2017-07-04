using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour {

	public GameObject DeleteSpherePrefab;
	private GameObject DeleteSphere;
	
	GameObject collidingObject;
	GameObject pickedObject;
	Transform pickedObjectParent;

	// Use this for initialization
	void Start () {
		DeleteSphere = Instantiate(DeleteSpherePrefab, transform.position + new Vector3(0, -0.05f, 0.01f), transform.rotation);
		DeleteSphere.transform.parent = this.transform;
		DeleteSphere.SetActive(false);

	}

	// Update is called once per frame
	void Update() {

	}

	private void OnDestroy() {
		Destroy(DeleteSphere);
	}

	// Subscribe event listeners on device enable
	private void OnEnable() {
		GetComponent<SteamVR_TrackedController>().TriggerClicked += new ClickedEventHandler(ClickTrigger);
		GetComponent<SteamVR_TrackedController>().TriggerUnclicked += new ClickedEventHandler(UnclickTrigger);
		GetComponent<SteamVR_TrackedController>().Gripped += new ClickedEventHandler(ClickGrip);
		GetComponent<SteamVR_TrackedController>().Ungripped += new ClickedEventHandler(UnclickGrip);
		GetComponent<SteamVR_TrackedController>().MenuButtonClicked += new ClickedEventHandler(ClickMenu);
		GetComponent<SteamVR_TrackedController>().MenuButtonUnclicked += new ClickedEventHandler(UnclickMenu);
	}


	// Unsubscribe event listeners on device disable
	private void OnDisable() {
		GetComponent<SteamVR_TrackedController>().TriggerClicked -= new ClickedEventHandler(ClickTrigger);
		GetComponent<SteamVR_TrackedController>().TriggerUnclicked -= new ClickedEventHandler(UnclickTrigger);
		GetComponent<SteamVR_TrackedController>().Gripped -= new ClickedEventHandler(ClickGrip);
		GetComponent<SteamVR_TrackedController>().Ungripped -= new ClickedEventHandler(UnclickGrip);
		GetComponent<SteamVR_TrackedController>().MenuButtonClicked -= new ClickedEventHandler(ClickMenu);
		GetComponent<SteamVR_TrackedController>().MenuButtonUnclicked -= new ClickedEventHandler(UnclickMenu);
	}

	private void ClickTrigger(object sender, ClickedEventArgs e) {
		Debug.Log("Trigger button on " + this.gameObject.name + " was just pressed");
		if (collidingObject) {
			if (DeleteSphere.activeSelf) {
				Delete(collidingObject);
			} else {
				PickUp(collidingObject);
			}
		}
	}


	private void UnclickTrigger(object sender, ClickedEventArgs e) {
		Debug.Log("Trigger button on " + this.gameObject.name + " was just released");
		if (pickedObject) {
			Drop(pickedObject);
		}
	}

	private void ClickGrip(object sender, ClickedEventArgs e) {
		DeleteSphere.SetActive(true);
		if (pickedObject) {
			Delete(pickedObject);
		}
	}

	private void UnclickGrip(object sender, ClickedEventArgs e) {
		DeleteSphere.SetActive(false);
	}

	private void ClickMenu(object sender, ClickedEventArgs e) {
		throw new NotImplementedException();
	}

	private void UnclickMenu(object sender, ClickedEventArgs e) {
		throw new NotImplementedException();
	}


	private void OnTriggerEnter(Collider other) {
		collidingObject = other.gameObject;
		
	}

	private void OnTriggerExit(Collider other) {
		if(other.gameObject == collidingObject) {
			collidingObject = null;
		}
	}
	private void PickUp(GameObject collidingObject) {
		pickedObject = collidingObject;
		pickedObjectParent = pickedObject.transform.parent;
		pickedObject.transform.parent = this.transform;
	}

	private void Drop(GameObject pickedObject) {
		if (pickedObject) {
			pickedObject.transform.parent = pickedObjectParent;
		}
		pickedObject = null;		
	}

	private void Delete(GameObject gameObject) {
		if (gameObject == pickedObject) {
			pickedObject = null;
		}
		Destroy(gameObject);
	}
}

