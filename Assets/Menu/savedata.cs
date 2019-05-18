using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// klasa przechowująca dane do zapisu
/// </summary>
[System.Serializable]
public class savedata {

    // spis wszystki informacji które muszą zostać zapisane 
    public float[] position;
    public int[] CS;
    public string[] INV, EQ;
    public int[] INVc;
    public int Money, exp, statP;
    public float[] camPos, camRot, camPro;
    public float camUp;
    public bool[] camFrez;
    public string Scen;

    /// <summary>
    /// konstruktor
    /// </summary>
    /// <param name="player"></param>
    public savedata(SaveLinker player) {
        Scen = SceneManager.GetActiveScene().name;
        position = VecToFlo(player.player.position);

        CS = new int[6] { player.IM.Level.BaseValue, player.IM.Strength.BaseValue, player.IM.Agility.BaseValue, player.IM.Stamina.BaseValue, player.IM.Luck.BaseValue, player.IM.Armor.BaseValue };
        Money = player.IM.inventory.money;
        exp = player.IM.Exp;
        statP = player.IM.statPoints;

        List<string> temp = new List<string>();
        List<int> tempc = new List<int>();

        ItemSlot[] tmp = player.IM.inventory.itemSlots;
        for (int i = 0; i < tmp.Length; i++) {
            if (tmp[i].Amount != 0) {
                if (tmp[i].Item != null && tmp[i].Item.ID != null) {
                    tempc.Add(tmp[i].Amount);
                    temp.Add(tmp[i].Item.ID);
                }
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
        camPos = VecToFlo(player.cam.currentPlace.transform.position);
        camRot = VecToFlo(player.cam.currentPlace.transform.eulerAngles);
        camPro = VecToFlo(player.cam.distance);
        camUp = player.cam.lookUP;
        camFrez = new bool[] { player.cam.x, player.cam.y, player.cam.z, player.cam.ThirdPerson };


        //Debug.Log(position.Length + " " + CS.Length + " " + INV.Length + " " + EQ.Length + " " + INVc.Length + " " + Money + " " + exp + " " + statP + " " + camPos.Length + " " + camRot.Length + " " + camPro.Length + " " + camUp + " " + camFrez.Length + " " + Scen);

    }
    /// <summary>
    /// zamiana wektora 3d na tablice float
    /// </summary>
    /// <param name="v">wektor</param>
    /// <returns>float</returns>
    float[] VecToFlo(Vector3 v) {
        float[] position = new float[3];
        position[0] = v.x;
        position[1] = v.y;
        position[2] = v.z;
        return position;
    }
    /// <summary>
    /// zamiana tablicy float na wektor 3d 
    /// </summary>
    /// <param name="v">float</param>
    /// <returns>wektor</returns>
    public Vector3 FloToVec(float[] f) {
        Vector3 position = new Vector3(f[0], f[1], f[2]);
        return position;
    }

}
