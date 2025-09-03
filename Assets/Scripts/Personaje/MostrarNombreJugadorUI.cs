using UnityEngine;
using TMPro;

public class MostrarNombreJugadorUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoNombreJugador;

    void Start()
    {
        string nombre = PlayerPrefs.GetString("NombreJugador", "Jugador");
        textoNombreJugador.text = nombre;
    }
}