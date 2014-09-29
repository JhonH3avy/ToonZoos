using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private GameObject follow; 
	public float smoothing = 5f;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		//POsicionar la camara en la posición de partida?
		// transform.position += Vector3.right * (transform.position.x - TrackController.instance.GetStartLineX());
		follow = GameObject.FindGameObjectWithTag("Player"); 
		offset = transform.position - follow.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Detenerse al final de la pista
		if(transform.position.x < TrackController.instance.GetFinishLineX()){
			Vector3 newPosition = new Vector3 (follow.transform.position.x + offset.x, transform.position.y, transform.position.z);
			transform.position = Vector3.Lerp (transform.position, newPosition, smoothing * Time.deltaTime);
		}
	}
}