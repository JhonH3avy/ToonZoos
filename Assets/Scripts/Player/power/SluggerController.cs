using UnityEngine;
using System.Collections;

/**
 * Es una bola que el jugador lanza hacia el frente
 * Si toca a otro jugador, lo relentiza por unos momentos
 * */
public class SluggerController : PowerController {

	public float strength = 10f;
	/**	
	 * Lanza la bola de poder con tiro parabólico
	 * */
	protected override IEnumerator Empowering(){
		rigidbody.AddForce(Vector3.up * strength);
		rigidbody.AddForce(Vector3.right * strength * 2f);
		yield return null;
	}
	
	/**
	 * Usado cuando el poder alcanza a otro jugador
	 * */
	protected override void Strike(RunnerController runner){
		Debug.Log("Slugger Touched another" );
		runner.SlowDown(-1);
	}

	protected override void Stumble(ObstacleController obstacle){
	}
}
