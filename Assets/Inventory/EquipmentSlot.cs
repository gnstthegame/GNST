using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Slot w panelu ekwipunku
/// </summary>
public class EquipmentSlot : ItemSlot
{
    //Typ przedmiotu
    public EquipmentType equipmentType;

<<<<<<< Updated upstream
    protected override void OnValidate()
=======
    /// <summary>
    /// Dodanie właściwych nazw do slotu
    /// </summary>
    protected override void Awake()
>>>>>>> Stashed changes
    {
        base.OnValidate();
        gameObject.name = equipmentType.ToString() + " Slot";
    }

    /// <summary>
    /// Metoda sprawdzająca, czy można założyć dany przedmiot do konkretnego slotu
    /// </summary>
    /// <param name="item">Przedmiot, który chcemy dodać(nie musi być możliwy do założenia)</param>
    /// <returns></returns>
    public override bool CanReceiveItem(Item item)
    {
        if (item == null)
            return true;

        EquippableItem equippableItem = item as EquippableItem;
        return equippableItem != null && equippableItem.equipmentType == equipmentType;
    }
}
