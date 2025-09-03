using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
   [Header("Config")]
   [SerializeField] private GameObject panelLoot;
   [SerializeField] private LootButton lootButtonPrefab;
   [SerializeField] private Transform lootContenedor;
   
   private GameObject dropItemActual;

   
   public void MostrarLoot(EnemigoLoot enemigoLoot)
   {
      panelLoot.SetActive(true);
      dropItemActual = enemigoLoot.restosInstanciados;
      if (ContenedorOcupado())
      {
         foreach (Transform hijo in lootContenedor.transform)
         {
            Destroy(hijo.gameObject);
         }
      }

      for (int i = 0; i < enemigoLoot.LootSeleccionado.Count; i++)
      {
         CargarLootPanel(enemigoLoot.LootSeleccionado[i]);
      }
   }

   public void RevisarLootRestante()
   {
      LootButton[] hijos = lootContenedor.GetComponentsInChildren<LootButton>();
      if (hijos.Length == 0)
      {
        
         panelLoot.SetActive(false);

         if (dropItemActual != null)
         {
            Destroy(dropItemActual);
            dropItemActual = null;
         }
      }
   }
   
   public void CerrarPanel()
   {
      panelLoot.SetActive(false);
      Invoke("RevisarLootRestante", 0.05f); // lo mismo, por si ya no queda loot
   }
   
   private void CargarLootPanel(DropItem dropItem)
   {
      if (dropItem.ItemRecogido)
      {
         return;
      }

      LootButton loot = Instantiate(lootButtonPrefab, lootContenedor);
      loot.ConfigurarLootItem(dropItem);
      loot.transform.SetParent(lootContenedor);
   }

   private bool ContenedorOcupado()
   {
      LootButton[] hijos = lootContenedor.GetComponentsInChildren<LootButton>();
      if (hijos.Length > 0)
      {
         return true;
      }
      
      return false;
   }
}
