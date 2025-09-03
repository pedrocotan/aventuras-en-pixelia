using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;
    
    [Header("Paneles")]
    [SerializeField] private GameObject panelStats;
    [SerializeField] private GameObject panelTienda;
    [SerializeField] private GameObject panelCrafting;
    [SerializeField] private GameObject panelCraftingInfo;
    [SerializeField] private GameObject panelInventario;
    [SerializeField] private GameObject panelInspectorQuest;
    [SerializeField] private GameObject panelPersonajeQuest;
    [SerializeField] private GameObject panelMuerte;
    public GameObject panelMensajeInformativo;
    
    [Header("Barra")]
    [SerializeField] private Image vidaPlayer;
    [SerializeField] private Image manaPlayer;
    [SerializeField] private Image expPlayer;
    
    [Header("Texto")]
    [SerializeField] private TextMeshProUGUI vidaTMP;
    [SerializeField] private TextMeshProUGUI manaTMP;
    [SerializeField] private TextMeshProUGUI expTMP;
    [SerializeField] private TextMeshProUGUI nivelTMP;
    [SerializeField] private TextMeshProUGUI monedasTMP;
    public TextMeshProUGUI mensajeInformativoTexto;
    
    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI statDañoTMP;
    [SerializeField] private TextMeshProUGUI statDefensaTMP;
    [SerializeField] private TextMeshProUGUI statCriticoTMP;
    [SerializeField] private TextMeshProUGUI statBloqueoTMP;
    [SerializeField] private TextMeshProUGUI statVelocidadTMP;
    [SerializeField] private TextMeshProUGUI statNivelTMP;
    [SerializeField] private TextMeshProUGUI statExpTMP;
    [SerializeField] private TextMeshProUGUI statExpRequeridaTMP;
    [SerializeField] private TextMeshProUGUI atributoFuerzaTMP;
    [SerializeField] private TextMeshProUGUI atributoInteligenciaTMP;
    [SerializeField] private TextMeshProUGUI atributoDestrezaTMP;
    [SerializeField] private TextMeshProUGUI atributosDisponiblesTMP;
    
    
    private float vidaActual;
    private float vidaMax;
    private float manaActual;
    private float manaMax;
    private float expActual;
    private float expRequeridaNuevoNivel;

    
    
    // Update is called once per frame
    void Update()
    {
        ActualizarUIPersonaje();
        ActualizarPanelStats();
    }
    
    public void MostrarPanelMuerte()
    {
        panelMuerte.SetActive(true);
    }
    
    public void MostrarMensaje(string mensaje, float duracion = 2f)
    {
        StopAllCoroutines(); // Por si hay otro mensaje activo
        StartCoroutine(MostrarMensajeTemporal(mensaje, duracion));
    }

    private IEnumerator MostrarMensajeTemporal(string mensaje, float duracion)
    {
        panelMensajeInformativo.SetActive(true);
        mensajeInformativoTexto.text = mensaje;
        yield return new WaitForSeconds(duracion);
        panelMensajeInformativo.SetActive(false);
    }
    
    private void ActualizarUIPersonaje()
    {
        vidaPlayer.fillAmount = Mathf.Lerp(vidaPlayer.fillAmount, vidaActual / vidaMax, 10f * Time.deltaTime);
        manaPlayer.fillAmount = Mathf.Lerp(manaPlayer.fillAmount, manaActual / manaMax, 10f * Time.deltaTime);
        expPlayer.fillAmount = Mathf.Lerp(expPlayer.fillAmount, expActual / expRequeridaNuevoNivel, 10f * Time.deltaTime);
        
        vidaTMP.text = $"{vidaActual}/{vidaMax}";
        manaTMP.text = $"{manaActual}/{manaMax}";
        expTMP.text = $"{((expActual/expRequeridaNuevoNivel) * 100):F2}%";
        nivelTMP.text = $"Nivel {stats.Nivel}";
        monedasTMP.text = MonedasManager.Instance.MonedasTotales.ToString();
    }

    private void ActualizarPanelStats()
    {
        if (panelStats.activeSelf == false)
        {
            return;
        }
        
        statDañoTMP.text = stats.Daño.ToString();
        statDefensaTMP.text = stats.Defensa.ToString();
        statCriticoTMP.text = $"{stats.PorcentajeCritico}%";
        statBloqueoTMP.text = $"{stats.PorcentajeBloqueo}%";
        statVelocidadTMP.text = stats.Velocidad.ToString();
        statNivelTMP.text = stats.Nivel.ToString();
        statExpTMP.text = stats.ExpActual.ToString();
        statExpRequeridaTMP.text = stats.ExpRequeridaSiguienteNivel.ToString();
        
        atributoFuerzaTMP.text = stats.Fuerza.ToString();
        atributoInteligenciaTMP.text = stats.Inteligencia.ToString();
        atributoDestrezaTMP.text = stats.Destreza.ToString();
        atributosDisponiblesTMP.text = $"Puntos: {stats.PuntosDisponibles}";
    }

    public void ActualizarVidaPersonaje(float pVidaActual, float pVidaMax)
    {
        vidaActual = pVidaActual;
        vidaMax = pVidaMax;
    }
    
    public void ActualizarManaPersonaje(float pManaActual, float pManaMax)
    {
        manaActual = pManaActual;
        manaMax = pManaMax;
    }
    
    public void ActualizarExpPersonaje(float pExpActual, float pExpRequerida)
    {
        expActual = pExpActual;
        expRequeridaNuevoNivel = pExpRequerida;
    }

    #region Paneles

    public void AbrirCerrarPanelStats()
    {
        panelStats.SetActive(!panelStats.activeSelf);
    }

    public void AbrirCerrarPanelTienda()
    {
        panelTienda.SetActive(!panelTienda.activeSelf);
    }

    public void AbrirPanelCrafting()
    {
        panelCrafting.SetActive(true);
    }
    
    public void CerrarPanelCrafting()
    {
        panelCrafting.SetActive(false);
        AbrirCerrarPanelCraftingInformacion(false);
    }

    public void AbrirCerrarPanelCraftingInformacion(bool estado)
    {
        panelCraftingInfo.SetActive(estado);
    }

    public void AbrirCerrarPanelInvetario()
    {
        panelInventario.SetActive(!panelInventario.activeSelf);
    }

    public void AbrirCerrarPanelPersonajeQuest()
    {
        panelPersonajeQuest.SetActive(!panelPersonajeQuest.activeSelf);
    }
    
    public void AbrirCerrarPanelInspectorQuest()
    {
        panelInspectorQuest.SetActive(!panelInspectorQuest.activeSelf);
    }

    public void AbrirPanelInteraccion(InteraccionExtraNPC tipoInteraccion)
    {
        switch (tipoInteraccion)
        {
            case InteraccionExtraNPC.Quests:
                AbrirCerrarPanelInspectorQuest();
                break;
            case InteraccionExtraNPC.Tienda:
                AbrirCerrarPanelTienda();
                break;
            case InteraccionExtraNPC.Crafting:
                AbrirPanelCrafting();
                break;
                
        }
    }
    
    #endregion
}
