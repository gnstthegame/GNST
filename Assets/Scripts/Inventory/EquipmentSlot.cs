using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Slot w panelu ekwipunku
/// </summary>
public class EquipmentSlot : ItemSlot
{
    public EquipmentType equipmentType;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = equipmentType.ToString() + " Slot";
    }
}
