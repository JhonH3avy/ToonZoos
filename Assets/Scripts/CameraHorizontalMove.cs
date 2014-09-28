using UnityEngine;
using System.Collections;

public class CameraHorizontalMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position += Vector3.right * .06f;
	}
}