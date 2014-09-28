using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

	public float smoothing = 5f;

	private Camera m_camera;
	private Vector3 offset;

	private void Awake ()
	{
		//if (photonView.isMine)
			m_camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
	}

	private void Start ()
	{
		//if (photonView.isMine)
		{
			offset = m_camera.transform.position - transform.position;
		}
	}

	private void FixedUpdate ()
	{
		//if (photonView.isMine)
		{
			Vector3 newPosition = new Vector3 (transform.position.x + offset.x, m_camera.transform.position.y, m_camera.transform.position.z);
			m_camera.transform.position = Vector3.Lerp (m_camera.transform.position, newPosition, smoothing * Time.deltaTime);
		}
	}
}

