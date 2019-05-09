using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
/// <summary>
/// Slot w panelu ekwipunku
/// </summary>
public class EquipmentSlot : ItemSlot
{
    public EquipmentType equipmentType;
=======
public class EquipmentSlot : ItemSlot {
    public EquipmentType EquipmentType;
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d

    protected override void OnValidate()
    {
        base.OnValidate();
<<<<<<< HEAD
        gameObject.name = equipmentType.ToString() + " Slot";
=======
        gameObject.name = EquipmentType.ToString() + "Slot";
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
    }

    public override bool CanReceiveItem(Item item)
    {
<<<<<<< HEAD
        if (item == null)
            return true;

        EquippableItem equippableItem = item as EquippableItem;
        return equippableItem != null && equippableItem.equipmentType == equipmentType;
=======
        if(item == null)
        {
            return true;
        }

        EquippableItem equippableItem = item as EquippableItem;
        return equippableItem != null && equippableItem.EquipmentType == EquipmentType;
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
    }
}
