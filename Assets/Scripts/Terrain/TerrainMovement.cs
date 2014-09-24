using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainMovement : MonoBehaviour
{
	public GameObject terrainPrefab;
	public Vector3 terrainBenchmark;
	public float placeFactor = 2f;
	public int terrainAmount = 3;
	public bool willGrow = true;
	public float offset = 0f;

	private List<GameObject> terrainList;

	private void Awake ()
	{
		terrainList = new List<GameObject> ();

		for(int i = 0; i < terrainAmount; i++)
		{
			GameObject terrain = Instantiate(terrainPrefab, terrainBenchmark, terrainPrefab.transform.rotation) as GameObject;

			terrain.transform.parent = this.transform;
			terrain.name = terrain.transform.parent.name+ "_" + i.ToString();
			float distanceToMove = Vector3.Distance (terrain.renderer.bounds.max, terrain.renderer.bounds.min);
			terrain.transform.position += new Vector3 ((distanceToMove - offset)* i, 0f, 0f);

			terrainList.Add(terrain);
		}
	}

	private void OnEnable ()
	{
		foreach (GameObject terrain in terrainList)
		{
			terrain.GetComponent<TerrainPlacement> ().Placing += PlaceIncomingTerrain;
		}
	}

	private void OnDisable ()
	{
		foreach (GameObject terrain in terrainList)
		{
			terrain.GetComponent<TerrainPlacement> ().Placing -= PlaceIncomingTerrain;
		}
	}

	private void PlaceIncomingTerrain (GameObject placeObject, TerrainPlacement.PlacementEventArgs args)
	{
		if (args.distance < 0)
		{
			Vector3 newPos = placeObject.transform.position;
			newPos.Set (newPos.x + Mathf.Abs (args.distance) * placeFactor, newPos.y, newPos.z);
			placeObject.transform.position = newPos;
		}
	}
}
