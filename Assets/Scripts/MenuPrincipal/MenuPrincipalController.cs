using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuPrincipalController : MonoBehaviour
{
    public GameObject panelMenuPrincipal;
    public GameObject panelNombreJugador;

    public TextMeshProUGUI mensajeTexto;
    
    public void NuevaPartida()
    {
        Debug.Log("Mostrando panel para introducir nombre del jugador...");
        panelMenuPrincipal.SetActive(false);
        panelNombreJugador.SetActive(true);
        PlayerPrefs.SetInt("PartidaNueva", 1); // Es nueva
        PlayerPrefs.Save();
    }

    public void CargarPartida()
    {
        if (!PlayerPrefs.HasKey("EscenaGuardada"))
        {
            Debug.Log("No hay partida guardada.");
            if (mensajeTexto  != null)
            {
                mensajeTexto .text = "No hay ninguna partida guardada.";
            }
            return;
        }

        string escena = PlayerPrefs.GetString("EscenaGuardada");
        float x = PlayerPrefs.GetFloat("PlayerX", 0);
        float y = PlayerPrefs.GetFloat("PlayerY", 0);
        float z = PlayerPrefs.GetFloat("PlayerZ", 0);

        PlayerPrefs.SetInt("PartidaNueva", 0); // Indicamos que es una partida cargada
        PlayerPrefs.Save();

        SceneManager.LoadScene(escena);
    }

    public void CerrarSesion()
    {
        Debug.Log("Cerrando sesión y volviendo al login...");
        SceneManager.LoadScene("Login");
    }

    private void BorrarMensaje()
    {
        if (mensajeTexto != null)
        {
            mensajeTexto.text = "";
        }
    }
}