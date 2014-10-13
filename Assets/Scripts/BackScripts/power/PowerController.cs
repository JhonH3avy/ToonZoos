using UnityEngine;
using System.Collections;

/**
 * Representa un poder en general que podría afectar tanto al poseedor del poder 
 * como el jugador alcanzado por el poder
 * */
public abstract class PowerController : MonoBehaviour {

	protected RunnerController owner;
	public RunnerController Owner{ set{owner = value;}}
	private bool empowered = false;
	
	void Start () {

	}

	void FixedUpdate () {
		if(!empowered & owner != null){
			StartCoroutine("Empowering");
			empowered = true;
		}
		else if(empowered && renderer != null && !renderer.isVisible){
			Debug.Log ("Power became invisible.. destroy it");
			DestroyObject(gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other) { 
		if(owner == null || other.gameObject != owner.gameObject){
			Debug.Log("Touched other tag "+other.tag);
			if(other.tag.Contains( "Player" ) ){
				RunnerController runner = other.GetComponent<RunnerController>();
				if(runner != null )
					Strike(runner);
				else
					Debug.LogError("PowerController - Controller not found");
				Destroy(gameObject);
			}
		}
	}

	/**
	 * Usado para cualquier cambio que el poder ejerza sobre el jugador que lanza el poder,
	 * invocado en el fixed Update.
	 * */
	protected abstract IEnumerator Empowering();

	/**
	 * Usado cuando el poder alcanza a otro jugador
	 * */
	protected abstract void Strike(RunnerController runner);
}
