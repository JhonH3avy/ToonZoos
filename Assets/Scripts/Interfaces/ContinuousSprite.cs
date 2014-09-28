using UnityEngine;
using System.Collections;

/**
 * Se asegura de crear la ilusión de que la pista es continua para la cámara, 
 * simplemente moviendo el sprite que quede fuera de la cámara y poniendolo al frente 
 * del grupo de sprites que contiene el GameObject padre.
 * 
 * Para configurar:
 * Cree un GameObject vacío y agregue como hijos al menos dos Sprites idénticos 
 * que casen por los bordes derecho e izquierdo. Luego agregue este script a todos los sprites.
 * 
 * Para probar asegúrese de que la cámara del scene NO esté (ni vaya a visualizar durante el play)
 * Ninguno de los sprites del gameObject 
 * 
 * @Author Edwin 
 * */
public class ContinuousSprite : MonoBehaviour {
	void OnBecameInvisible(){
		//posicion del primero y el último
		Transform[] childrenPos = transform.parent.GetComponentsInChildren<Transform>( );
		float maxX = 0, minX = float.MaxValue;
		foreach(Transform t in childrenPos){
			if(t.renderer != null){
				minX = Mathf.Min(minX,t.renderer.bounds.min.x);
				maxX = Mathf.Max(maxX,t.renderer.bounds.max.x);
			}
		}
		//si es el de más a la izquierda, mover a la derecha de todos
		if(renderer.bounds.min.x == minX){
			//cuanto hay que mover?
			float dist = Mathf.Abs(maxX - renderer.bounds.min.x);
			transform.position += Vector3.right * dist;				
		}
	}
}
