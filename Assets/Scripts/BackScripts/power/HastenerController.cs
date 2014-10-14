using UnityEngine;
using System.Collections;

/**
 * Aumenta la velocidad del jugador
 * */
public class HastenerController: PowerController {
	
	public float maxSpeed = 1.5f;
	/**	
	 * Lanza la bola de poder con tiro parabólico
	 * */
	protected override IEnumerator Empowering(){
		if(owner!=null){
			Debug.Log("Increasing speed");
			owner.SlowDown(maxSpeed);
		}
		yield return null;
	}
	
	/**
	 * Usado cuando el poder alcanza a otro jugador
	 * */
	protected override void Strike(RunnerController runner){

	}
}


