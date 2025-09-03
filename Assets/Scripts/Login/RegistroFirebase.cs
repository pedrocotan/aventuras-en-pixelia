using System;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;

public class RegistroFirebase : MonoBehaviour
{
    public TMP_InputField campoCorreo;
    public TMP_InputField campoContraseña;
    public TMP_InputField campoConfirmarContraseña;
    public TextMeshProUGUI mensajeTexto;
    public GameObject panelFormulario;
    public GameObject panelLogin;

    private FirebaseAuth autenticacion;
    private bool cambiarPantalla = false;

    void Start()
    {
        autenticacion = FirebaseAuth.DefaultInstance;
    }

    void Update()
    {
        if (cambiarPantalla)
        {
            cambiarPantalla = false;
            StartCoroutine(VolverAPantallaPrincipal());
        }
    }

    public void RegistrarUsuario()
    {
        string correo = campoCorreo.text;
        string contraseña = campoContraseña.text;
        string confirmarContraseña = campoConfirmarContraseña.text;

        if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contraseña) || string.IsNullOrEmpty(confirmarContraseña))
        {
            mensajeTexto.text = "Completa todos los campos.";
            return;
        }

        if (contraseña != confirmarContraseña)
        {
            mensajeTexto.text = "Las contraseñas no coinciden.";
            return;
        }

        autenticacion.CreateUserWithEmailAndPasswordAsync(correo, contraseña).ContinueWithOnMainThread(tarea =>
        {
            if (tarea.IsCanceled)
            {
                Debug.LogError("Registro cancelado.");
                mensajeTexto.text = "Registro cancelado.";
                return;
            }
            if (tarea.IsFaulted)
            {
                Debug.LogError("Error en el registro: " + tarea.Exception);

                string mensajeError = tarea.Exception.InnerExceptions[0].Message;

                if (mensajeError.Contains("badly formatted"))
                {
                    mensajeTexto.text = "El correo no tiene un formato válido.";
                }
                else if (mensajeError.Contains("password is invalid") || mensajeError.Contains("at least 6 characters"))
                {
                    mensajeTexto.text = "La contraseña debe tener al menos 6 caracteres.";
                }
                else if (mensajeError.Contains("The email address is already in use"))
                {
                    mensajeTexto.text = "Este correo ya está registrado.";
                }
                else
                {
                    mensajeTexto.text = "Error: " + mensajeError;
                }

                return;
            }

            AuthResult resultado = tarea.Result;
            FirebaseUser nuevoUsuario = resultado.User;
            Debug.LogFormat("Usuario registrado correctamente: {0} ({1})", nuevoUsuario.Email, nuevoUsuario.UserId);
            mensajeTexto.text = "¡Registro exitoso! Ahora puedes iniciar sesión.";

            campoCorreo.text = "";
            campoContraseña.text = "";
            campoConfirmarContraseña.text = "";

            cambiarPantalla = true;
        });
    }

    private System.Collections.IEnumerator VolverAPantallaPrincipal()
    {
        yield return new WaitForSeconds(2f); // Espera 2 segundos antes de cambiar
        CambiarAPantallaPrincipal();
    }

    public void CambiarAPantallaPrincipal()
    {
        panelFormulario.SetActive(false);
        panelLogin.SetActive(true);
    }
}