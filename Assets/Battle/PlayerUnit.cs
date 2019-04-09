using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerUnit : Unit {
    public GameObject ButtonsMenu;
    public GameObject[] Buttons;
    public InventoryManager Inv;

    public void Activ() {
        GetComponent<CharacterMotor>().enabled = false;
        MaxAP = 2+ (int)Inv.Stamina.Value;
        MovementRange = 1+(int)Inv.Agility.Value;
        MaxHP = 4+(int)Inv.Stamina.Value;
        AP = (int)Inv.Stamina.Value;
        APacc = (int)Inv.Stamina.Value;
        Armor = (int)Inv.Armor.Value;
        Skile = Inv.GetSkills();
        ButtonsMenu.SetActive(true);
        for (int i=0; i<Buttons.Length; i++) {
            if(i<Skile.Count) {
                Buttons[i].SetActive(true);
                Buttons[i].GetComponent<Image>().sprite = Skile[i].Icon;
            } else {
                Buttons[i].SetActive(false);
            }
        }

    }
    private void Awake() {
        //Effect e = new Effect(0,3, (int dmg, Unit u) => { return 0; });

        //Skile.Add(new Skill(new Vector2(2, 3)));
        /*
        Vector2[] tmp = new Vector2[4];
        tmp[0] = new Vector2(-1, 0);
        tmp[1] = new Vector2(0, 0);
        tmp[2] = new Vector2(1, 0);
        tmp[3] = new Vector2(0, 1);
        Vector2[] tmp2 = new Vector2[4];
        tmp2[0] = new Vector2(1, 0);
        tmp2[1] = new Vector2(0, 0);
        tmp2[2] = new Vector2(1, -1);
        tmp2[3] = new Vector2(0, -1);
        Skile.Add(new Skill(tmp, new Vector2(2, 3)));
        Skile.Add(new Skill(tmp2, new Vector2(2, 3), 1, "Dash", 0, null, false, 6));
        */
    }
}
