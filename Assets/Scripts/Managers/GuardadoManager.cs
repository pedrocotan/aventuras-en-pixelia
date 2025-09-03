using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardadoManager : MonoBehaviour
{
    public void GuardarPartida()
    {
        GameObject jugador = GameObject.FindWithTag("Player");

        if (jugador != null)
        {
            Vector3 posicion = jugador.transform.position;

            PlayerPrefs.SetString("EscenaGuardada", SceneManager.GetActiveScene().name);
            PlayerPrefs.SetFloat("PlayerX", posicion.x);
            PlayerPrefs.SetFloat("PlayerY", posicion.y);
            PlayerPrefs.SetFloat("PlayerZ", posicion.z);
            PlayerPrefs.Save();

            Debug.Log("Partida guardada con éxito.");
        }
        else
        {
            Debug.LogWarning("No se encontró al jugador para guardar la partida.");
        }
    }
}