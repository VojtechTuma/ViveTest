using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	public enum AxisType {
		XAxis,
		ZAxis
	}
	public bool Enabled = true;
	public Color color = Color.red;
	public float Thickness = 0.002f;
	public float Length = 2.0f;
	public AxisType FacingAxis = AxisType.XAxis;

	private GameObject laser;
	private GameObject holder;
	
	float contactDistance = 0f;
	Transform contactTarget = null;

	// Use this for initialization
	void Start () {
		Material newMaterial = new Material(Shader.Find("Unlit/Color"));
		if (!newMaterial) {
			Debug.Log("material not found!");
		}
		newMaterial.SetColor("_Color", color);

		holder = new GameObject();
		holder.transform.parent = this.transform;
		holder.transform.localPosition = Vector3.zero;

		laser = GameObject.CreatePrimitive(PrimitiveType.Cube);
		laser.transform.parent = this.transform;
		laser.GetComponent<MeshRenderer>().material = newMaterial;
		laser.GetComponent<BoxCollider>().isTrigger = true;
		laser.AddComponent<Rigidbody>().isKinematic = true;
		SetLaserTransform(Length, Thickness);
	}

	float GetBeamLength(bool bHit, RaycastHit hit) {
		float actualLength = Length;

		//reset if beam not hitting or hitting new target
		if (!bHit || (contactTarget && contactTarget != hit.transform)) {
			contactDistance = 0f;
			contactTarget = null;
		}

		//check if beam has hit a new target
		if (bHit) {
			if (hit.distance <= 0) {

			}
			contactDistance = hit.distance;
			contactTarget = hit.transform;
		}

		//adjust beam length if something is blocking it
		if (bHit && contactDistance < Length) {
			actualLength = contactDistance;
		}

		if (actualLength <= 0) {
			actualLength = Length;
		}

		return actualLength; ;
	}

	void Update() {
		if (Enabled) {
			laser.SetActive(true);
			Ray raycast = new Ray(transform.position, transform.forward);

			RaycastHit hitObject;
			bool rayHit = Physics.Raycast(raycast, out hitObject);

			float beamLength = GetBeamLength(rayHit, hitObject);
			SetLaserTransform(beamLength, Thickness);
		} else {
			laser.SetActive(false);
		}
	}

	void SetLaserTransform(float setLength, float setThickness) {
		//if the additional decimal isn't added then the beam position glitches
		float beamPosition = setLength / (2 + 0.00001f);

		if (FacingAxis == AxisType.XAxis) {
			laser.transform.localScale = new Vector3(setLength, setThickness, setThickness);
			laser.transform.localPosition = new Vector3(beamPosition, 0f, 0f);			
		} else {
			laser.transform.localScale = new Vector3(setThickness, setThickness, setLength);
			laser.transform.localPosition = new Vector3(0f, 0f, beamPosition);			
		}
	}
}
