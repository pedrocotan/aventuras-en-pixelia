using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{
    [Header("Stats")] [SerializeField] private PersonajeStats stats;
    
    [Header("Config")]
    [SerializeField] private int nivelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;

    private float expActual;
    private float expActualTemp;
    private float expRequeridaSiguienteNivel;
    
    // Start is called before the first frame update
    void Start()
    {
        stats.Nivel = 1;
        expRequeridaSiguienteNivel = expBase;
        stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
        ActualizarBarraExp();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AñdirExperiencia(2f);
        }
    }

    public void AñdirExperiencia(float expObtenida)
    {
        if (expObtenida > 0f)
        {
            float expRestanteNuevoNivel = expRequeridaSiguienteNivel - expActualTemp;
            if (expObtenida >= expRestanteNuevoNivel)
            {
                expObtenida -= expRestanteNuevoNivel;
                expActual += expObtenida;
                ActualizarNivel();
                AñdirExperiencia(expObtenida);
            }
            else
            {
                expActual += expObtenida;
                expActualTemp += expObtenida;
                if (expActualTemp == expRestanteNuevoNivel)
                {
                    ActualizarNivel();
                }
            }
        }
        
        stats.ExpActual = expActual;
        ActualizarBarraExp();
    }

    private void ActualizarNivel()
    {
        if (stats.Nivel < nivelMax)
        {
            stats.Nivel++;
            expActualTemp = 0f;
            expRequeridaSiguienteNivel *= valorIncremental;
            stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
            stats.PuntosDisponibles += 3;
        }
    }

    private void ActualizarBarraExp()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActualTemp, expRequeridaSiguienteNivel);
    }
    
    private void RespuestaEnemigoDerrotado(float exp)
    {
        AñdirExperiencia(exp);
    }

    private void OnEnable()
    {
        EnemigoVida.EventoEnemigoDerrotado += RespuestaEnemigoDerrotado;
    }

    private void OnDisable()
    {
        EnemigoVida.EventoEnemigoDerrotado -= RespuestaEnemigoDerrotado;

    }
}
