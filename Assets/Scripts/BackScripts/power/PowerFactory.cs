using UnityEngine;
using System.Collections;


/**
 * Controla la ejecución de los poderes.
 * No permite usar el poder más de una vez.
 * Se debe agregar al prefab del jugador
 * */
[RequireComponent (typeof (RunnerController))]
public class PowerFactory : MonoBehaviour {
	public PowerController PowerType;
	private RunnerController owner;
	public Transform LaunchPosition;
	private bool allow = true;

	private float launchTime = float.MaxValue;
	private readonly float FIRE_DELAY = 0.002f;

	public void Start(){
		if(PowerType == null)
			Debug.LogError("PowerFactory without PowerType");
		owner = GetComponentInParent<RunnerController>();
		if(owner == null)
			Debug.LogError("PowerFactory requires RunnerController");
	}

	public void Update(){
		//FIXME no lanzar poder si está caido o aun no ha empezado la carrera
		if(allow && launchTime <= Time.time && PowerType != null){
			Debug.Log("Firing power actual fire");
			PowerController pw = null;
			if(LaunchPosition != null)
				pw = (PowerController)Instantiate(PowerType, LaunchPosition.position, Quaternion.identity);
			else
				pw = (PowerController)Instantiate(PowerType);

			pw.Owner = owner;
			//allow = false;
			launchTime = float.MaxValue;
		}
	}

	/**
	 * Instancia el poder para ser lanzado en el retraso por defecto FIRE_DELAY
	 * */
	public void Fire(){
		launchTime = Time.time + FIRE_DELAY;
	}

	/**
	 * Instancia el poder en el momento dado, si es en el pasado, lo lanza de inmediato
	 * */
	public void Fire(float time){
		launchTime = time;
	}
}
