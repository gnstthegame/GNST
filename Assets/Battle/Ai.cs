using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : Unit {
    //public SpecPole[,] mapka;
    // Use this for initialization
    void Awake() {
        //Skile.Add(new Skill(new Vector2(2, 3)));
        Vector2[] tmp = new Vector2[4];
        tmp[0] = new Vector2(0, 0);
        tmp[1] = new Vector2(1, 0);
        tmp[2] = new Vector2(-1, 0);
        tmp[3] = new Vector2(0, 1);
        Vector2[] tmp2 = new Vector2[4];
        tmp2[0] = new Vector2(1, 0);
        tmp2[1] = new Vector2(0, 0);
        tmp2[2] = new Vector2(1, -1);
        tmp2[3] = new Vector2(0, -1);
        Skile.Add(new Skill(tmp, new Vector2(3, 6)));
        Skile.Add(new Skill(tmp2, new Vector2(2, 2), 0, 1,null,false,5));
        Vector2[] tmp3 = new Vector2[3];
        tmp3[0] = new Vector2(-1, -1);
        tmp3[1] = new Vector2(0, -1);
        tmp3[2] = new Vector2(1, -1);
        //Effect e = new Effect(2, 3,(int dmg, Unit u) => { return dmg/2; });
        Effect e = new Effect(2, 2, (int dmg, Unit u) => { u.ArmorMod += 2; return 0; }, (int dmg, Unit u) => { u.ArmorMod -= 2; return 0; });
        Skile.Add(new Skill(tmp3, Vector2.zero, 4, 2, e, true));


        Vector2[] self = new Vector2[1] { new Vector2(0, -1) };
        Effect f = new Effect(2, 1, (int dmg, Unit u) => { u.AP += 2; return 0; });
        Skile.Add(new Skill(self, Vector2.zero, 0, 3, f));
    }
}
