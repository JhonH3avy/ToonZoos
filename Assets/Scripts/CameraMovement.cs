using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

    [SerializeField]
    private GameObject follow;
    [SerializeField]
    private float smoothing = 5f;
    [SerializeField]
    private Vector3 offset = new Vector3 ( 5, 0, 0 );

    // Use this for initialization
    private void Start ()
    {
        // POsicionar la camara en la posición de partida?
        // transform.position += Vector3.right * (transform.position.x - TrackController.instance.GetStartLineX());
        var target = GameObject.FindGameObjectWithTag ( "Player" );
        if ( target != null )
        {
            follow = target;
        }
        else
        {
            Debug.LogError ( "There is no target player to follow" );
        }
    }

    // Update is called once per frame
    private void Update ()
    {
        //Detenerse al final de la pista
        if ( transform.position.x < TrackController.Instance.GetFinishLineX () )
        {
            Vector3 newPosition = new Vector3 ( follow.transform.position.x + offset.x, transform.position.y, transform.position.z );
            transform.position = Vector3.Lerp ( transform.position, newPosition, smoothing * Time.deltaTime );
        }
    }
}