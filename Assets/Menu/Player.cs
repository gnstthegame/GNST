using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour {

    public int level = 3;
    public int health = 40;
    public Transform player;
    public bool saveTrigger = false;
    public InventoryManager IM;
    private void OnValidate() {
        IM = GetComponentInChildren<InventoryManager>();
    }

    private void Update() {
        if (saveTrigger) {
            SavePlayer();
            saveTrigger = false;
        }
    }

    public void SavePlayer() {
        saveSystem.SavePlayer(this);
    }

    public void LoadPlayer() {
        savedata data = saveSystem.Loaddata(this);
        level = data.level;
        health = data.health;

        player.transform.position = data.FloToVec(data.position);

        IM.Level.BaseValue = data.CS[0];
        IM.Strength.BaseValue = data.CS[1];
        IM.Agility.BaseValue = data.CS[2];
        IM.Stamina.BaseValue = data.CS[3];
        IM.Luck.BaseValue = data.CS[4];
        IM.Armor.BaseValue = data.CS[5];
        
        List<Item> itms = new List<Item>(Resources.FindObjectsOfTypeAll<Item>());
        List<EquippableItem> eitms = new List<EquippableItem>(Resources.FindObjectsOfTypeAll<EquippableItem>());
        
        foreach (EquipmentSlot s in IM.equipmentPanel.equipmentSlots) {
            if (s.Item != null) {
                IM.Unequip((EquippableItem)s.Item);
            }
        }
        foreach (ItemSlot s in IM.inventory.itemSlots) {
            s.Amount = 0;
            s.Item = null;
        }

        for (int i = 0; i < data.EQ.Length; i++) {
            if (data.EQ[i] != null) {
                EquippableItem asd = eitms.Find(unit => unit.ID == data.EQ[i]);
                IM.inventory.AddItem(asd);
                IM.Equip(asd);
            }
        }
        for (int i = 0; i < data.INV.Length; i++) {
            if (data.INVc[i] != 0) {
                IM.inventory.AddItem(itms.Find(unit => unit.ID == data.INV[i]), data.INVc[i]);
            }
        }

        IM.ReloadStats();
    }
}
