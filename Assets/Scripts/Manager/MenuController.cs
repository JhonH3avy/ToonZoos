using UnityEngine;
using Menu.Button;
using System.Collections;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPrincipal;
    [SerializeField]
    private GameObject seleccionPersonaje;

    [SerializeField]
    private Button buttonRace;
    [SerializeField]
    private Button buttonVersus;
    [SerializeField]
    private Button buttonUser;
    [SerializeField]
    private Button buttonBack;

    private void Start ()
    {
        buttonRace.Clicking += () =>
        {
            menuPrincipal.SetActive ( false );
            seleccionPersonaje.SetActive ( true );
        };

        buttonVersus.Clicking += () =>
        {
            //TODO rellenar funcion para que los jugadores puedan invitar a otros amigos
        };

        buttonUser.Clicking += () =>
        {
            //TODO hacer aparecer pantalla de usuario
        };

        buttonBack.Clicking += () =>
        {
            menuPrincipal.SetActive ( true );
            seleccionPersonaje.SetActive ( false );
            GameManager.Instance.ResetPlayersCharacter ();
        };
    }

    private void Update ()
    {
        if ( GameManager.Instance.GetRemainingPlayers () <= 0 )
        {
            StartCoroutine ( StartGame () );
        }
    }

    private IEnumerator StartGame ()
    {
        yield return new WaitForSeconds ( 2.0f );
        Application.LoadLevel ( "Dos Jugadores" );
    }
}
