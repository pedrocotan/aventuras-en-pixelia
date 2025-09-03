using UnityEngine;
using TMPro;

public class BotonRegistro : MonoBehaviour
{
    public GameObject panelLogin;
    public GameObject panelFormularioRegistro;
    public TextMeshProUGUI mensajeTexto; // ← Añade esto

    public void MostrarFormularioRegistro()
    {
        panelLogin.SetActive(false);
        panelFormularioRegistro.SetActive(true);
        mensajeTexto.text = ""; // ← Aquí limpiamos el mensaje
    }

    public void CancelarRegistro()
    {
        panelFormularioRegistro.SetActive(false);
        panelLogin.SetActive(true);
    }
}