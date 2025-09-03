﻿    using System;
    using UnityEngine;
    using UnityEngine.PlayerLoop;

    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Personaje personaje;
        [SerializeField] private Transform puntoReaparicion;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (personaje.PersonajeVida.Derrotado)
                {
                    personaje.transform.localPosition = puntoReaparicion.position;
                    personaje.RestaurarPersonaje();
                }
            }
        }
    }
