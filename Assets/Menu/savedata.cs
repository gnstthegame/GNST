using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class savedata {

    // spis wszystki informacji które muszą zostać zapisane 
    public float[] position;
    //public CharacterStat[] CS;
    public int[] CS;
    public string[] INV, EQ;
    public int[] INVc;
    public ItemSlot[] IS;

    //funkcja trzymająca dane playera z zapisu
    public savedata(SaveLinker player) {

        position = VecToFlo(player.player.position);

        CS = new int[6] { player.IM.Level.BaseValue, player.IM.Strength.BaseValue, player.IM.Agility.BaseValue, player.IM.Stamina.BaseValue, player.IM.Luck.BaseValue, player.IM.Armor.BaseValue };

        ItemSlot[] tmp= player.IM.inventory.itemSlots;
        INV = new string[tmp.Length];
        INVc = new int[tmp.Length];
        for(int i =0; i< tmp.Length; i++) {
            INVc[i] = tmp[i].Amount;
            if (INVc[i] != 0) {
                INV[i] = tmp[i].Item.ID;
            }
        }

        EquipmentSlot[] tmp2 = player.IM.equipmentPanel.equipmentSlots;
        EQ = new string[tmp2.Length];
        for (int i = 0; i < tmp2.Length; i++) {
            if (tmp2[i].Item != null) {
                EQ[i] = tmp2[i].Item.ID;
            }
        }
        
    }

    float[] VecToFlo(Vector3 v) {
        float[] position = new float[3];
        position[0] = v.x;
        position[1] = v.y;
        position[2] = v.z;
        return position;
    }
    public Vector3 FloToVec(float[] f) {
        Vector3 position = new Vector3(f[0], f[1], f[2]);
        return position;
    }

}
