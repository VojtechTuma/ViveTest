using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour {

	private Shader defaultShader;
	private Shader highlightShader;

	private void Start() {
		defaultShader = GetComponent<Renderer>().material.shader;
		highlightShader = Shader.Find("Outlined/Silhouetted Diffuse");
		if (!highlightShader) {
			Debug.Log("Highlight shader not found!");
		}
	}

	private void OnTriggerStay(Collider other) {
		if (other.gameObject.GetComponent<WandController>()) {
			GetComponent<Renderer>().material.shader = highlightShader;
			if (other.gameObject.GetComponent<SteamVR_TrackedController>().gripped) {
				GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.red);
			} else {
				GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.green);
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
		Debug.Log("Object collided with " + other.name);
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.GetComponent<WandController>()) {
			Debug.Log("Object stopped colliding with " + other.name);
			GetComponent<Renderer>().material.shader = defaultShader;
		}
	}

}
