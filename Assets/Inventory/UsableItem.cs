using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsableItem : Item {
    public bool isConsumable;
    public string description;

    public virtual void Use(InventoryManager i)
    {
        Debug.Log("Using " + this.Name);
    }
}
