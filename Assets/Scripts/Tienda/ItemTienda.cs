using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTienda : MonoBehaviour
{
    [Header("Config")] 
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemCoste;
    [SerializeField] private TextMeshProUGUI cantidadPorComprar;

    public ItemVenta ItemCargado { get; private set; }
    
    private int cantidad;
    private int costeInicial;
    private int costeActual;

    private void Update()
    {
        cantidadPorComprar.text = cantidad.ToString();
        itemCoste.text = costeActual.ToString();
    }

    public void ConfigurarItemVenta(ItemVenta itemVenta)
    {
        ItemCargado = itemVenta;
        itemIcono.sprite = itemVenta.Item.Icono;
        itemNombre.text = itemVenta.Item.Nombre;
        itemCoste.text = itemVenta.Coste.ToString();
        cantidad = 1;
        costeInicial = itemVenta.Coste;
        costeActual = itemVenta.Coste;
    }

    public void ComprarItem()
    {
        if (MonedasManager.Instance.MonedasTotales >= costeActual)
        {
            Inventario.Instance.AñadirItem(ItemCargado.Item, cantidad);
            MonedasManager.Instance.EliminarMonedas(costeActual);
            cantidad = 1;
            costeActual = costeInicial;
        }
    }
    
    public void SumarItemPorComprar()
    {
        int costoDeCompra = costeInicial * (cantidad + 1);

        if (MonedasManager.Instance.MonedasTotales >= costoDeCompra)
        {
            cantidad++;
            costeActual = costeInicial * cantidad;
        }
    }

    public void RestarItemPorComprar()
    {
        if (cantidad == 1)
        {
            return;
        }
        
        cantidad--;
        costeActual = costeInicial * cantidad;
    }
}
