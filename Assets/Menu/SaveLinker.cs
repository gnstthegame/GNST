using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveLinker : MonoBehaviour {

    public Transform player;
    public bool saveTrigger = false;
    public InventoryManager IM;
    public bool newScene = true;

    private void Awake() {
        player = FindObjectOfType<CharacterMotor>().transform;
        IM = GetComponentInChildren<InventoryManager>();
    }
    private void Start() {
        if (newScene) {
            LoadPlayer(false);
            SavePlayer();
        }
    }

    private void Update() {
        if (saveTrigger) {
            SavePlayer();
            saveTrigger = false;
        }
    }

    public void SavePlayer() {
        saveSystem.SavePlayer(this);
        Debug.Log("Save Succes");
    }

    public void LoadPlayer(bool Position = true) {
        savedata data = saveSystem.Loaddata(this);
        if (data == null) {
            return;
        }
        if (Position) {
            player.transform.position = data.FloToVec(data.position);
        }
        player.GetComponent<Animator>().SetTrigger("Live");
        player.GetComponent<CharacterMotor>().enabled = true;
        IM.Level.BaseValue = data.CS[0];
        IM.Strength.BaseValue = data.CS[1];
        IM.Agility.BaseValue = data.CS[2];
        IM.Stamina.BaseValue = data.CS[3];
        IM.Luck.BaseValue = data.CS[4];
        IM.Armor.BaseValue = data.CS[5];
        IM.inventory.money = data.Money;

        List<Item> itms = new List<Item>(Resources.FindObjectsOfTypeAll<Item>());
        IM.inventory.startingItems = new Item[0];

        foreach (EquipmentSlot s in IM.equipmentPanel.equipmentSlots) {
            s.Amount = 0;
            s.Item = null;
        }
        foreach (ItemSlot s in IM.inventory.itemSlots) {
            s.Amount = 0;
            s.Item = null;
        }
        for (int i = 0; i < data.EQ.Length; i++) {
            if (data.EQ[i] != "0") {
                EquippableItem asd = (EquippableItem)itms.Find(unit => unit.ID == data.EQ[i]);
                IM.inventory.AddItem(asd);
                IM.Equip(asd);
            }
        }

        for (int i = 0; i < data.INV.Length; i++) {
            if (data.INV[i] != "0") {
                IM.inventory.AddItem(itms.Find(unit => unit.ID == data.INV[i]), data.INVc[i]);
            }
        }

        IM.ReloadStats();

        Debug.Log("Load Succes");
    }
}
