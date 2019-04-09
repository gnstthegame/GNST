using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Slot w panelu ekwipunku
/// </summary>
public class EquipmentSlot : ItemSlot
{
    public EquipmentType equipmentType;
    public EquippableItem equippableItem;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = equipmentType.ToString() + " Slot";
    }

    public override bool CanReceiveItem(Item item)
    {
        if (item == null)
            return true;
        equippableItem = item as EquippableItem;
        Debug.Log(item.Name);
        return equippableItem != null && equippableItem.equipmentType == equipmentType;
    }
}
