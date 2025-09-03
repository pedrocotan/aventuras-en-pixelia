using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAjustes : MonoBehaviour
{
    [SerializeField] private GameObject panelAjustes;

    public void TogglePanelAjustes()
    {
        bool activo = panelAjustes.activeSelf;
        panelAjustes.SetActive(!activo);
    }

    public void VolverAlMenuPrincipal()
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

        SceneManager.LoadScene("MenuPrincipal");
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

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // para cerrar desde el editor
#endif
    }
}
