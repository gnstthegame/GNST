using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerUnit : Unit {
    public InventoryManager Inv;
    
    /// <summary>
    /// importuje i przelicza atrybuty gracza na modyfikatory podczas walki
    /// </summary>
    public override void Activ() {
        GetComponent<CharacterMotor>().enabled = false;
        MaxAP = 2+ (int)Inv.Stamina.Value;
        Movement = 1+(int)Inv.Agility.Value;
        MaxHP = 4+(int)Inv.Stamina.Value;
        AP = 0;
        APacc = (int)Inv.Stamina.Value;
        Armor = (int)Inv.Armor.Value;
        CanMove = true;
        CanAct = true;
        Skile = Inv.GetSkills();
        Items = Inv.GetItems();
        HP = MaxHP;
    }
    public void SkillUsed(int k) {
        if (k > 2 && k < 9) {
            Items.RemoveAt(k - 3);
            hud.Upd();
        }
    }
}
