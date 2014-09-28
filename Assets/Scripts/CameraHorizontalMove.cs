using UnityEngine;
using System.Collections;

public class CameraHorizontalMove : MonoBehaviour {

	public GameObject trackControllerHolder;
	private TrackController2 trackController;

	// Use this for initialization
	void Start () {
		if(trackControllerHolder == null){
			trackControllerHolder = GameObject.Find("TrackControllerGo"); 
			trackController = trackControllerHolder.GetComponent<TrackController2>();
		}
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