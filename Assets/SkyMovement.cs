using UnityEngine;
using System.Collections;

/***
 * Simplemente siga a la camara en la coordenada x
 * */
public class SkyMovement : MonoBehaviour {

	Camera camera;
	// Use this for initialization
	void Start () {
		camera = GameObject.FindObjectOfType<Camera>();
		if(camera == null)
			throw new UnityException("Camera not found!s");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(camera.transform.position.x, transform.position.y, transform.position.z);
	}
}
