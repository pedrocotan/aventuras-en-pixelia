using System.Collections;
using TMPro;
using UnityEngine;

public class BienvenidaTextoAnimado : MonoBehaviour
{
    public TextMeshProUGUI textoBienvenida;

    private void Start()
    {
        int esPartidaNueva = PlayerPrefs.GetInt("PartidaNueva", 1);

        if (esPartidaNueva == 0)
        {
            // Si es una partida cargada, ocultamos el contenedor completo
            textoBienvenida.transform.parent.gameObject.SetActive(false);
            return;
        }

        string nombre = PlayerPrefs.GetString("NombreJugador", "héroe");
        string mensaje = $"¡Bienvenido a Pixelia, {nombre}!\n" +
                         "Aquí comienza tu aventura, lucha, sobrevive, mejora y crea armas poderosas para salvar este reino y convertirte en una leyenda.";
        StartCoroutine(AnimarTexto(mensaje));
    }

    private IEnumerator AnimarTexto(string oracion)
    {
        textoBienvenida.text = "";
        char[] letras = oracion.ToCharArray();
        for (int i = 0; i < letras.Length; i++)
        {
            textoBienvenida.text += letras[i];
            yield return new WaitForSeconds(0.03f); // Puedes ajustar la velocidad
        }
        
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0));
        yield return new WaitForSeconds(0f);
        textoBienvenida.transform.parent.gameObject.SetActive(false);
    }
}