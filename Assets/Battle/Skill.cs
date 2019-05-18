using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// klasa efektów nakładanych na jednostki
/// </summary>
public class Effect {
    public enum trig {
        BeforeGetHit, OnTurnStart, OnApply
    }
    public trig Triger;//0-BeforeGetHit, 1-OnTurnStart, 2-OnApply
    public int Duration;
    public delegate int delegacja(int dmg, Unit u);
    public delegacja Func, OnDestroy;

    /// <summary>
    /// konstuktor efektu
    /// </summary>
    /// <param name="trigger">0-BeforeGetHit, 1-OnTurnStart, 2-OnApply</param>
    /// <param name="duration">czas trwania</param>
    /// <param name="Function">delegacja wywoływana zgodnie z triggerem</param>
    /// <param name="onDestroy">delegacja wywoływana po skończeniu czasu trwania</param>
    public Effect(trig trigger, int duration, delegacja Function = null, delegacja onDestroy = null) {
        Triger = trigger;
        Duration = duration;
        Func = Function;
        OnDestroy = onDestroy;
    }
    public Effect(trig trigger, int duration, int val = 0, EffectType Function = 0, EffectType onDestroy = 0) {
        Triger = trigger;
        Duration = duration;
        Func = Eff(Function, val);
        OnDestroy = Eff(onDestroy, val);
    }
    public enum EffectType {
        Null,
        ArmorPlus,
        ArmorMinus,
        APPlus,
        APMinus,
        APaccPlus,
        APaccMinus,
        HPPlus,
        HPMinus,
        MovPlus,
        MovMinus,
        LuckPlus,
        LuckMinus,
        ClearEffects,
        DmgMultiplayer
    }
    /// <summary>
    /// zwraca popularne delegacje 
    /// </summary>
    /// <param name="id">enum</param>
    /// <param name="value">wartość modyfikatora</param>
    /// <returns></returns>
    delegacja Eff(EffectType id, int value = 0) {
        int it = (int)id;
        switch (it) {
            default:
                return null;
            case 1:
                //Armor+
                return (int dmg, Unit u) => { u.ArmorMod += value; return dmg; };
            case 2:
                //Armor-
                return (int dmg, Unit u) => { u.ArmorMod -= value; return dmg; };
            case 3:
                //AP+
                return (int dmg, Unit u) => { u.AP += value; return dmg; };
            case 4:
                //AP-
                return (int dmg, Unit u) => { u.AP -= value; return dmg; };
            case 5:
                //APacc+
                return (int dmg, Unit u) => { u.APaccMod += value; return dmg; };
            case 6:
                //APacc-
                return (int dmg, Unit u) => { u.APaccMod -= value; return dmg; };
            case 7:
                //HP+
                return (int dmg, Unit u) => { u.HP += value; return dmg; };
            case 8:
                //HP-
                return (int dmg, Unit u) => { u.HP -= value; return dmg; };
            case 9:
                //Mov+
                return (int dmg, Unit u) => { u.MovMod += value; return dmg; };
            case 10:
                //Mov-
                return (int dmg, Unit u) => { u.MovMod -= value; return dmg; };
            case 11:
                //Luck+
                return (int dmg, Unit u) => { u.LuckMod += value; return dmg; };
            case 12:
                //Luck-
                return (int dmg, Unit u) => { u.LuckMod -= value; return dmg; };
            case 13:
                //Oczyszczenie
                return (int dmg, Unit u) => {
                    foreach (Effect e in u.Effects) {
                        e.OnDestroy(0, u);
                    }
                    u.Effects.Clear();
                    return dmg;
                };
            case 14:
                //Multiplayer
                return (int dmg, Unit u) => { return dmg * value; };
            case 15:
                //skuteczność
                return (int dmg, Unit u) => {  ;return dmg; };
        }
    }

}
/// <summary>
/// umiejętność
/// </summary>
public class Skill {/*
    -1,0 |0,0 |1,0
    -1,-1| U  |1,-1
     */
    public Vector2[] Area;
    public List<Vector2>[] DirectedArea;
    public Vector2 Dmg;
    public int Cost;
    public Effect efect;
    public int AttackRange = 0;
    public int maxDist = 0;
    public int moment = 1;//0-begin, 1-normal, 2-end, 3-self-efect
    public string trigger = "Punch";
    public bool positive = false;
    public Sprite Icon;
    public GameObject Model;
    public Skill() { }
    public Skill(Vector2 ForwardAtackDmg) {
        Area = new Vector2[1];
        Dmg = ForwardAtackDmg;
        maxDist = 1;
        Calculate();
    }
    /// <summary>
    /// konstruktor umiejętności
    /// </summary>
    /// <param name="AtackArea">tablica miejsc ataku</param>
    /// <param name="Damage">obrażenia przedział [min, max)</param>
    /// <param name="cost">koszt punktów akcji</param>
    /// <param name="AnimationTriggerName">nazwa animacji</param>
    /// <param name="Moment">0-begin, 1-normal, 2-end, 3-self</param>
    /// <param name="Efect">efekt</param>
    /// <param name="Positive">pozytywny</param>
    /// <param name="Range">zasieg</param>
    public Skill(Vector2[] AtackArea, Vector2 Damage, int cost = 0, string AnimationTriggerName = "Punch", int Moment = 1, Effect Efect = null, bool Positive = false, int Range = 0) {
        Area = AtackArea;
        Dmg = Damage;
        Cost = cost;
        efect = Efect;
        AttackRange = Range;
        moment = Moment;
        positive = Positive;
        trigger = AnimationTriggerName;
        Calculate();
    }
    public Skill(Vector2[] AtackArea, Vector2 Damage, int cost, string AnimationTrigger, Sprite icon, GameObject model, Effect.trig trigg = 0, int duration=0,int value=0, Effect.EffectType Function = 0, Effect.EffectType onDestroy = 0, bool Positive = false, int Range = 0) {
        Area = AtackArea;
        Dmg = Damage;
        Cost = cost;
        Icon = icon;
        Model = model;
        efect = new Effect(trigg, duration, value, Function, onDestroy);
        AttackRange = Range;
        positive = Positive;
        trigger = AnimationTrigger;
        Calculate();
    }
    /// <summary>
    /// przelicza informacje dodatkowe
    /// </summary>
    void Calculate() {
        foreach (Vector2 v in Area) {
            maxDist = Mathf.Max(TileMap.IntDistance(0,-1, v), maxDist);
        }
        DirectedArea = new List<Vector2>[4];
        List<Vector2> up = new List<Vector2>();
        foreach (Vector2 v in Area) {
            up.Add(v);
        }
        DirectedArea[0] = up;

        List<Vector2> down = new List<Vector2>();
        foreach (Vector2 v in Area) {
            down.Add(new Vector2(v.x * -1, v.y * -1));
        }
        DirectedArea[1] = down;

        List<Vector2> left = new List<Vector2>();
        foreach (Vector2 v in Area) {
            left.Add(new Vector2(v.y * -1, v.x));
        }
        DirectedArea[2] = left;

        List<Vector2> right = new List<Vector2>();
        foreach (Vector2 v in Area) {
            right.Add(new Vector2(v.y, v.x * -1));
        }
        DirectedArea[3] = right;
    }
}
