using UnityEngine;
using System.Collections;

public class CameraHorizontalMove : MonoBehaviour {
	
	private TrackController2 trackController;

	// Use this for initialization
	void Start () {
		trackController = GameObject.FindObjectOfType<TrackController2>(); 
		print ("Camera Limit="+trackController.GetFinishLineX());
		transform.position += Vector3.right * (transform.position.x - trackController.GetStartLineX());
	}
	
	// Update is called once per frame
	void Update () {
		//Detenerse al final de la pista
		if(transform.position.x < trackController.GetFinishLineX())
			transform.position += Vector3.right * .06f;
	}
}