using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect {
    public int Triger = 0;//0-BeforeGetHit, 1-OnTurnStart, 2-OnApply
    public int Duration;
    public delegate int delegacja(int dmg, Unit u);
    public delegacja Func, OnDestroy;
    /// <param name="trigger">0-BeforeGetHit, 1-OnTurnStart, 2-OnApply</param>
    public Effect(int trigger, int duration, delegacja Function = null, delegacja onDestroy = null) {
        Triger = trigger;
        Func = Function;
        Duration = duration;
        OnDestroy = onDestroy;
    }
}

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
    public Skill() { }
    public Skill(Vector2 ForwardAtackDmg) {
        Area = new Vector2[1];
        Dmg = ForwardAtackDmg;
        maxDist = 1;
        Calculate();
    }

    /// <param name="Damage">przedział [min, max)</param>
    /// <param name="Moment">0-begin, 1-normal, 2-end, 3-self</param>
    public Skill(Vector2[] AtackArea, Vector2 Damage, int cost = 0, string AnimationTriggerName = "Punch", int Moment = 1, Effect Efect = null, bool Positive = false, int Range = 0) {
        Area = AtackArea;
        Dmg = Damage;
        Cost = cost;
        efect = Efect;
        AttackRange = Range;
        moment = Moment;
        positive = Positive;
        Calculate();
        trigger = AnimationTriggerName;
    }

    void Calculate() {
        Vector2 s = new Vector2(0, -1);
        foreach (Vector2 v in Area) {
            maxDist = Mathf.Max(TileMap.IntDistance(s, v), maxDist);
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
