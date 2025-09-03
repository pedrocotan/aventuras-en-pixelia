using System;
using UnityEngine;
using TMPro;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using Firebase.Extensions;

public class InicioSesion : MonoBehaviour
{
    public TMP_InputField campoCorreo;
    public TMP_InputField campoContraseña;
    public TextMeshProUGUI mensajeTexto;

    private FirebaseAuth autenticacion;

    void Start()
    {
        autenticacion = FirebaseAuth.DefaultInstance;
    }

    public void IniciarSesion()
    {
        string correo = campoCorreo.text;
        string contraseña = campoContraseña.text;

        if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contraseña))
        {
            mensajeTexto.text = "Por favor, completa todos los campos.";
            return;
        }

        autenticacion.SignInWithEmailAndPasswordAsync(correo, contraseña).ContinueWithOnMainThread(tarea =>
        {
            if (tarea.IsCanceled)
            {
                Debug.LogWarning("Inicio de sesión cancelado.");
                mensajeTexto.text = "Inicio cancelado.";
                return;
            }
            if (tarea.IsFaulted)
            {
                Debug.LogError("Error en el inicio de sesión: " + tarea.Exception);

                Exception baseExcepcion = tarea.Exception.GetBaseException();
                string mensajeError = baseExcepcion.Message;

                if (mensajeError.Contains("EMAIL_NOT_FOUND") || mensajeError.Contains("no user record"))
                    mensajeTexto.text = "Esta cuenta no existe. Verifica tu correo o regístrate.";
                else if (mensajeError.Contains("INVALID_PASSWORD") || mensajeError.Contains("password is invalid"))
                    mensajeTexto.text = "Contraseña incorrecta.";
                else if (mensajeError.Contains("INVALID_EMAIL") || mensajeError.Contains("badly formatted"))
                    mensajeTexto.text = "Correo malformado.";
                else if (mensajeError.Contains("TOO_MANY_ATTEMPTS_TRY_LATER") || mensajeError.Contains("blocked"))
                    mensajeTexto.text = "Demasiados intentos fallidos. Intenta más tarde.";
                else if (mensajeError.Contains("INTERNAL_ERROR") || mensajeError.Contains("internal error"))
                    mensajeTexto.text = "Error interno de servidor. Intenta de nuevo en unos minutos.";
                else
                    mensajeTexto.text = "Error inesperado: " + mensajeError;

                Debug.LogError("Error en el inicio de sesión: " + mensajeError);

                return;
            }

            FirebaseUser usuario = tarea.Result.User;
            Debug.LogFormat("Sesión iniciada con éxito: {0} ({1})", usuario.Email, usuario.UserId);
            mensajeTexto.text = "¡Bienvenido, " + usuario.Email + "!";

            // Cambiar a la escena del juego
            SceneManager.LoadScene("MenuPrincipal", LoadSceneMode.Single);
        });
    }
}