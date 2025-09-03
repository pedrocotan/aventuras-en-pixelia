
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class EnemigoLoot : MonoBehaviour
    {
        [Header("Exp")]
        [SerializeField] private float expGanada;

        
        [Header("Loot")]
        [SerializeField] private DropItem[] lootDisponible;
        
        [HideInInspector] public GameObject restosInstanciados;
        
        private List<DropItem> lootSeleccionado = new List<DropItem>();
        public List<DropItem> LootSeleccionado => lootSeleccionado;
        public float ExpGanada => expGanada;

        private void Start()
        {
            SeleccionarLoot();
        }

        private void SeleccionarLoot()
        {
            foreach (DropItem item in lootDisponible)
            {
                float probabilidad = Random.Range(0f, 100);
                if (probabilidad <= item.POrcentajeDrop)
                {
                    lootSeleccionado.Add(item);
                }
            }
        }
    }