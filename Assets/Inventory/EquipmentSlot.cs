using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Slot w panelu ekwipunku
/// </summary>
public class EquipmentSlot : ItemSlot
{
    public EquipmentType equipmentType;


    public override bool CanReceiveItem(Item item)
    {
        if (item == null)
            return true;

        EquippableItem equippableItem = item as EquippableItem;
        return equippableItem != null && equippableItem.equipmentType == equipmentType;
    }
}
