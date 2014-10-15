using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private GameObject follow; 
	public float smoothing = 5f;
	private Vector3 offset;

	// Use this for initialization
	private void Start () {
		// POsicionar la camara en la posición de partida?
		// transform.position += Vector3.right * (transform.position.x - TrackController.instance.GetStartLineX());
		var target = GameObject.FindGameObjectWithTag("Player");
		if (target != null)
		{
			follow = target;
			offset = transform.position - follow.transform.position;
		}
		else
		{
			Debug.LogError ("There is no target player to follow");
		}
	}
	
	// Update is called once per frame
	private void Update () {
		//Detenerse al final de la pista
		if(transform.position.x < TrackController.instance.GetFinishLineX()){
			Vector3 newPosition = new Vector3 (follow.transform.position.x + offset.x, transform.position.y, transform.position.z);
			transform.position = Vector3.Lerp (transform.position, newPosition, smoothing * Time.deltaTime);
		}
	}
}