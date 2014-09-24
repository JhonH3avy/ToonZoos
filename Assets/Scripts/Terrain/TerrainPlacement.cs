using UnityEngine;
using System.Collections;

public class TerrainPlacement : MonoBehaviour
{
	public float layerMultiplier;

	private Transform m_camera;	
	private PlayerController player;	


	#region Events and EventsHanlder implementation

	public delegate void PlacementEventHandler (GameObject sender, PlacementEventArgs args);
	public PlacementEventHandler Placing;

	protected virtual void OnPlacing (PlacementEventArgs args)
	{
		PlacementEventHandler handler = Placing;

		if(handler != null)
		{
			handler.Invoke (gameObject, args);
		}
	}

	public class PlacementEventArgs : System.EventArgs
	{
		private float _distance;

		public float distance
		{
			private set
			{
				_distance = value;
			}

			get
			{
				return _distance;
			}
		}

		public PlacementEventArgs (float dis)
		{
			distance = dis;
		}
	}

	#endregion

	private void Awake ()
	{
		m_camera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}

	private void OnEnable ()
	{
		GameManager.RaceStarted += StartMoveTerrain;
	}
	
	private void OnDisable ()
	{
		GameManager.RaceStarted -= StartMoveTerrain;
	}

	private void StartMoveTerrain ()
	{
		StartCoroutine (MoveTerrain ());
	}

	private void OnBecameInvisible ()
	{
		float distance = transform.position.x - m_camera.position.x;
		PlacementEventArgs newArgs = new PlacementEventArgs (distance);
		OnPlacing (newArgs);
	}

	private IEnumerator MoveTerrain ()
	{
		Vector3 moveVec = new Vector3 (-player.speed * layerMultiplier, 0f, 0f);
		while (true)
		{
			transform.Translate (moveVec);
			yield return null;
		}
	}

}

