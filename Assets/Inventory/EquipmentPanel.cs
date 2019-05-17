using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Klasa panelu aktualnie używanych przedmiotów
/// </summary>
public class EquipmentPanel : MonoBehaviour {
    //obiekt zawierający obiekty klasy equipment slot
    [SerializeField] Transform equipmentSlotsParent;
    //tablica pól panelu wyposażonych przedmiotów
    public EquipmentSlot[] equipmentSlots;
    //ekwipunek
    [SerializeField] Inventory inventory;

    //zdarzenie obsługujące wyświetlanie informacji o przedmiocie po najechaniu kursorem
    public event Action<ItemSlot> OnPointerEnterEvent;
    //zdarzenie chowające panel z informacjami po przesunięciu kursora znad obiektu
    public event Action<ItemSlot> OnPointerExitEvent;
    //zdarzenie obsługujące kliknięcie na obiekt
    public event Action<ItemSlot> OnRightClickEvent;
    //zdarzenie obsługujące początek przeciągania w drag and drop w panelu ekwipunku
    public event Action<ItemSlot> OnBeginDragEvent;
    //zdarzenie obsługujące zakończenie procesu przeciągania obiektu
    public event Action<ItemSlot> OnEndDragEvent;
    //zdarzenie obsługujące sam proces pomiędzy rozpoczęciem, a zakończeniem procesu przeciągania
    public event Action<ItemSlot> OnDragEvent;
    //zdarzenie obsługujące koniec procesu drag and drop
    public event Action<ItemSlot> OnDropEvent;

    /// <summary>
    /// Metoda wykonująca się po rozpoczęciu rozgrywki
    /// </summary>
    private void Start()
    {
        //dodanie obsługi zdarzeń do każdego slotu
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            equipmentSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            equipmentSlots[i].OnRightClickEvent += OnRightClickEvent;
            equipmentSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            equipmentSlots[i].OnEndDragEvent += OnEndDragEvent;
            equipmentSlots[i].OnDragEvent += OnDragEvent;
            equipmentSlots[i].OnDropEvent += OnDropEvent;
        }
    }
<<<<<<< Updated upstream

    private void OnValidate()
    {
=======
    /// <summary>
    ///Odnalezienie wszystkich slotów ekwipunku w hierarchii projektu, oraz dodanie ich do tablicy
    /// </summary>
    private void Awake()
    {      
>>>>>>> Stashed changes
        equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    /// <summary>
    /// Metoda zakładająca przedmiot z panelu przedmiotów posiadanych na panel przedmiotów aktualnie używanych przez postać
    /// </summary>
    /// <param name="item">Przedmiot, który chcemy założyć</param>
    /// <param name="previousItem">Przedmiot, który mamy aktualnie wyposażony</param>
    /// <returns></returns>
    public bool AddItem(EquippableItem item, out EquippableItem previousItem)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if(equipmentSlots[i].equipmentType == item.equipmentType)
            {
                previousItem = (EquippableItem)equipmentSlots[i].Item;
                equipmentSlots[i].Item = item;
                equipmentSlots[i].Amount = 1;
                return true;
            }
        }
        previousItem = null;
        return false;
    }
    
    /// <summary>
    /// Metoda zdejmująca aktualnie wyposażony przedmiot i dodająca go do panelu przedmiotów posiadanych
    /// </summary>
    /// <param name="item">Przedmiot, który chcemy zdjąć</param>
    /// <returns>Prawda, gdy udało się zdjąć, fałsz w przeciwnym wypadku.</returns>
    public bool RemoveItem(EquippableItem item)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].Item == item)
            {
                equipmentSlots[i].Item = null;
                equipmentSlots[i].Amount = 0;
                return true;
            }
        }
        return false;
    }
}
