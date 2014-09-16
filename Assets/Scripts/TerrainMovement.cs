using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainMovement : MonoBehaviour
{
	public GameObject terrainPrefab;
	public int terrainAmount = 3;
	public bool willGrow = true;

	private List<GameObject> terrainList;

	private void Start ()
	{
		terrainList = new List<GameObject> ();

		for(int i = 0; i < terrainAmount; i++)
		{
			GameObject terrain = Instantiate(terrainPrefab) as GameObject;

			terrainList.Add(terrain);
		}
	}
}
