using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class savedata {

    // spis wszystki informacji które muszą zostać zapisane 
    public float[] position;
    public int[] CS;
    public string[] INV, EQ;
    public int[] INVc;
    public int Money;

    //funkcja trzymająca dane playera z zapisu
    public savedata(SaveLinker player) {

        position = VecToFlo(player.player.position);

        CS = new int[6] { player.IM.Level.BaseValue, player.IM.Strength.BaseValue, player.IM.Agility.BaseValue, player.IM.Stamina.BaseValue, player.IM.Luck.BaseValue, player.IM.Armor.BaseValue };
        Money = player.IM.inventory.money;
        
        List<string> temp = new List<string>();
        List<int> tempc = new List<int>();

        ItemSlot[] tmp = player.IM.inventory.itemSlots;
        for (int i = 0; i < tmp.Length; i++) {
            if (tmp[i].Amount != 0 && tmp[i].Item.ID != null) {
                tempc.Add(tmp[i].Amount);
                temp.Add(tmp[i].Item.ID);
            }
        }
        tempc.Add(0);
        temp.Add("0");
        INV = temp.ToArray();
        INVc = tempc.ToArray();

        temp = new List<string>();
        EquipmentSlot[] tmp2 = player.IM.equipmentPanel.equipmentSlots;
        for (int i = 0; i < tmp2.Length; i++) {
            if (tmp2[i].Item != null) {
                temp.Add(tmp2[i].Item.ID);
            }
        }
        temp.Add("0");
        EQ = temp.ToArray();
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
