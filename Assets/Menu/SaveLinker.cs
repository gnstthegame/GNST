using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// gromadzi relacje potrzebne do zapisu
/// </summary>
public class SaveLinker : MonoBehaviour {

    public Transform player;
    public bool saveTrigger = false, deleteTrigger=false;
    public InventoryManager IM;
    [HideInInspector]
    public CameraFollow cam;

    private void Awake() {
        player = FindObjectOfType<CharacterMotor>().transform;
        IM = GetComponentInChildren<InventoryManager>();
        cam = Camera.main.GetComponent<CameraFollow>();
    }
    private void Start() {
        LoadPlayer();
        //SavePlayer();
    }

    private void Update() {
        if (saveTrigger) {
            SavePlayer();
            saveTrigger = false;
        }
        if (deleteTrigger) {
            saveSystem.DeletePlayer();
            deleteTrigger = false;
        }
    }
    /// <summary>
    /// zapisz stan gry
    /// </summary>
    public void SavePlayer() {
        saveSystem.SavePlayer(this);
        Debug.Log("Save Succes");
    }
    /// <summary>
    /// wczytaj stan gry
    /// </summary>
    /// <returns>true gdy udało sie</returns>
    public bool LoadPlayer() {
        savedata data = saveSystem.Loaddata(this);
        if (data == null) {
            return false;
        }
        bool Position = false;
        if (data.Scen == SceneManager.GetActiveScene().name) {
            Position = true;
        }
        //Debug.Log(data.position.Length + " " + data.CS.Length + " " + data.INV.Length + " " + data.EQ.Length + " " + data.INVc.Length + " " + data.Money + " " + data.exp + " " + data.statP + " " + data.camPos.Length + " " + data.camRot.Length + " " + data.camPro.Length + " " + data.camUp + " " + data.camFrez.Length + " " + data.Scen);
        if (Position) {
            player.transform.position = data.FloToVec(data.position);
            cam.transform.position = data.FloToVec(data.camPos);
            cam.transform.eulerAngles = data.FloToVec(data.camRot);
            cam.distance = data.FloToVec(data.camPro);
            cam.lookUP = data.camUp;
            cam.x = data.camFrez[0];
            cam.y = data.camFrez[1];
            cam.z = data.camFrez[2];
            cam.ThirdPerson = data.camFrez[3];
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
        IM.Exp = data.exp;
        IM.statPoints = data.statP;

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
        return true;
    }
}
