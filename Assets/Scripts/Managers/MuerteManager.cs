using UnityEngine;
using UnityEngine.SceneManagement;

public class MuerteManager : MonoBehaviour
{
    public void NuevaPartida()
    {
        PlayerPrefs.SetInt("PartidaNueva", 1); // Es nueva
        PlayerPrefs.Save();
        SceneManager.LoadScene("SampleScene");
    }

    public void ContinuarPartida()
    {
        if (PlayerPrefs.HasKey("EscenaGuardada"))
        {
            PlayerPrefs.SetInt("PartidaNueva", 0); // No es nueva
            PlayerPrefs.Save();

            string escena = PlayerPrefs.GetString("EscenaGuardada", "SampleScene");
            SceneManager.LoadScene(escena);
        }
        else
        {
            Debug.Log("No hay partida guardada.");
        }
    }

    public void SalirDelJuego()
    {
        GuardadoManager guardado = FindObjectOfType<GuardadoManager>();

        if (guardado != null)
        {
            guardado.GuardarPartida();
        }
        else
        {
            Debug.LogWarning("GuardadoManager no encontrado en la escena.");
        }

        Application.Quit();
    }
}