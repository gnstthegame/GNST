using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ai : Unit {
    [System.Serializable]
    public class Loot {
        public string Name = "Szperacz";
        public int EXP;
        public int Plush;
        public Item[] Rewards;
    }
    public enum Personality {
        Boss
    }
    public Personality Person;
    public GameObject chest;
    public Loot reward;
    // Use this for initialization
    void Awake() {
        Vector2[] fwd = new Vector2[1] { new Vector2(0, 1) };
        Vector2[] self = new Vector2[1] { new Vector2(0, -1) };//.
        Vector2[] tmp = new Vector2[4];
        tmp = new Vector2[4]{ //T
            new Vector2(0, 0),new Vector2(1, 0),new Vector2(-1, 0),new Vector2(0, 1) };
        Vector2[] tmp2 = new Vector2[4]{//O
            new Vector2(1, 0),new Vector2(0, 0),new Vector2(1, -1),new Vector2(0, -1) };
        Vector2[] tmp3 = new Vector2[3]{//-
            new Vector2(-1, -1),new Vector2(0, -1),new Vector2(1, -1)};

        Effect rest = new Effect(Effect.trig.OnApply, 1, (int dmg, Unit u) => { u.AP += 2; return 0; });
        switch (Person) {
            case Personality.Boss:
                Skile.Add(new Skill(self, Vector2.zero, 0, "Wait", 3, rest, true));
                Skile.Add(new Skill(fwd, new Vector2(1, 3), 0, "Punch", 1));
                Skile.Add(new Skill(tmp, new Vector2(2, 4), 4, "Slam", 1));
                break;
            default:
                break;
        }
        //Skile.Add(new Skill(new Vector2(2, 3)));
        //Skile.Add(new Skill(tmp2, new Vector2(2, 5), 0, "Dash",1,null,false,5));
        //Effect e = new Effect(2, 3,(int dmg, Unit u) => { return dmg/2; });
        //Skile.Add(new Skill(tmp, new Vector2(3, 6)));
        //Effect e = new Effect(Effect.trig.OnApply, 2, (int dmg, Unit u) => { u.ArmorMod += 2; return 0; }, (int dmg, Unit u) => { u.ArmorMod -= 2; return 0; });
        //Skile.Add(new Skill(tmp3, Vector2.zero, 4, "Wait", 2, e, true));


        //Effect f = new Effect(Effect.trig.OnApply, 1, (int dmg, Unit u) => { u.AP += 2; return 0; });
        //Skile.Add(new Skill(self, Vector2.zero, 0, "Wait", 3, f));
    }
    public Loot Clear() {
        if (reward.Rewards.Length > 0) {
            GameObject go = Instantiate(chest, transform.position, transform.rotation);
            go.GetComponent<Collectable>().items = reward.Rewards;
        }
        Destroy(gameObject);
        return reward;
    }
}
