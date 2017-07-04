using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catalogue : MonoBehaviour {

	public bool Enabled = false;
	public GameObject CataloguePrefab;
	private GameObject catalogue;



	// Use this for initialization
	void Start () {

		catalogue = Instantiate(CataloguePrefab, transform.position, transform.rotation);
		catalogue.transform.parent = this.transform;

	}
	
	// Update is called once per frame
	void Update () {
		catalogue.SetActive(Enabled);		
	}
}
