using UnityEngine;
using System.Collections;

/***
 * Simplemente siga a la camara en la coordenada x
 * */
public class SkyMovement : MonoBehaviour {

	Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindObjectOfType<Camera>();
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y, transform.position.z);
	}
}
