using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// klasa wrogich jednostek bojowych
/// </summary>
public class Ai : Unit {
    /// <summary>
    /// klasa nagród
    /// </summary>
    [System.Serializable]
    public class Loot {
        public string Name = "Szperacz";
        public int EXP;
        public int Plush;
        public Item[] Rewards;
    }
    public enum Personality {
        lil,
        Boss,
        slime,
        archer
    }
    public Personality Person;
    public GameObject chest;
    public Loot reward;
    /// <summary>
    /// przypisuje umiejętności zgodnie z klasą postaci 
    /// </summary>
    void Awake() {
        Vector2[] fwd = new Vector2[1] { Vector2.zero };
        Vector2[] self = new Vector2[1] { new Vector2(0, -1) };//.
        Vector2[] tmp = new Vector2[4];
        tmp = new Vector2[4]{ //T
            new Vector2(0, 0),new Vector2(1, 0),new Vector2(-1, 0),new Vector2(0, 1) };
        Vector2[] tmp2 = new Vector2[4]{//O
            new Vector2(1, 0),new Vector2(0, 0),new Vector2(1, -1),new Vector2(0, -1) };
        Vector2[] tmp3 = new Vector2[3]{//-
            new Vector2(-1, -1),new Vector2(0, -1),new Vector2(1, -1)};
        Vector2[] tmp4 = new Vector2[2]{//-
            new Vector2(0, 1),new Vector2(0, 0)};// |

        Effect rest = new Effect(Effect.trig.OnApply, 1, (int dmg, Unit u) => { u.AP += 2; return 0; });
        switch (Person) {
            case Personality.lil:
                Skile.Add(new Skill(self, Vector2.zero, 0, "Wait", 3, null, true));
                Skile.Add(new Skill(fwd, new Vector2(2, 3), 0, "Stab", 1));
                Skile.Add(new Skill(tmp4, new Vector2(3, 4), 2, "Slash", 1));
                break;
            case Personality.Boss:
                Skile.Add(new Skill(self, Vector2.zero, 0, "Wait", 3, null, true));
                Skile.Add(new Skill(fwd, new Vector2(1, 3), 0, "Punch", 1));
                Skile.Add(new Skill(tmp, new Vector2(2, 4), 4, "Slam", 1));
                break;
            case Personality.slime:
                freezRotation = true;
                Skile.Add(new Skill(self, Vector2.zero, 0, "Wait", 3, null, true));
                Skile.Add(new Skill(fwd, new Vector2(2, 3), 0, "attack", 1));
                break;
            case Personality.archer:
                Skile.Add(new Skill(self, Vector2.zero, 0, "Wait", 3, null, true));
                Skile.Add(new Skill(fwd, new Vector2(2, 4), 0, "shoot", 1,null,false,5));
                bestDist = 5;
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
    /// <summary>
    /// usówa model jednostki i zastępuje go skrzynią z nagrodą, a także dodaje punkty doświadczenia i plusz bohaterowi
    /// </summary>
    /// <returns></returns>
    public Loot Clear() {
        if (reward.Rewards.Length > 0) {
            GameObject go = Instantiate(chest, transform.position, transform.rotation);
            List<Pair> par = new List<Pair>();
            foreach(Item i in reward.Rewards) {
                par.Add(new Pair(i,1));
            }
            go.GetComponent<Chest>().lootItems = par;
        }
        //Destroy(gameObject);
        Destroy(this);
        return reward;
    }
    /// <summary>
    /// Metoda wywoływana na początku walki
    /// </summary>
    public override void Activ() {
        if (Person == Personality.slime) {
            anim = GetComponent<Animator>();
            anim.SetTrigger("expand");
        }
    }
}
