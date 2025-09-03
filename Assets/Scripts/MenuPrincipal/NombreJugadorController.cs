using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NombreJugadorController : MonoBehaviour
{
    public GameObject panelNombreJugador;
    public GameObject panelMenuPrincipal;

    public TMP_InputField inputNombre;
    public TextMeshProUGUI textoAviso;

    private const int LIMITE_CARACTERES = 6;

    public void ConfirmarNombre()
    {
        string nombre = inputNombre.text.Trim();

        if (string.IsNullOrEmpty(nombre))
        {
            textoAviso.text = "Introduce un nombre.";
            return;
        }

        if (nombre.Length > LIMITE_CARACTERES)
        {
            textoAviso.text = "Máximo 6 caracteres.";
            return;
        }

        // Guardamos el nombre localmente (puedo usar Firebase más adelante si quiero)
        PlayerPrefs.SetString("NombreJugador", nombre);
        // Limpiar datos antiguos de partida
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("PlayerZ");
        PlayerPrefs.DeleteKey("EscenaGuardada");
        PlayerPrefs.SetInt("PartidaNueva", 1); // Confirmamos que es nueva
        PlayerPrefs.Save();
        Debug.Log("Nombre guardado: " + nombre);

        // Cargar escena del juego
        SceneManager.LoadScene("SampleScene");
    }

    public void CancelarNombre()
    {
        inputNombre.text = "";
        textoAviso.text = "";
        panelNombreJugador.SetActive(false);
        panelMenuPrincipal.SetActive(true);
    }
}