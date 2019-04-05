using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
    public int tileX;
    public int tileY;
    public int MaxHP = 10, MaxAP = 10;//private
    public int ArmorBase = 0, APaccBase = 2;//private
    public int ArmorMod = 0, APaccMod = 2;
    public int ap=2;//private
    public int AP {
        get { return ap; }
        set { ap = Mathf.Clamp(value, 0, MaxAP); }
    }
    public int hp = 10;//private
    public int HP {
        get { return hp; }
        set { hp = Mathf.Clamp(value, 0, MaxHP);if (hp == 0) Die(); }
    }
    public int APacc {
        get { return Mathf.Max(0, APaccBase + APaccMod); }
        set { APaccBase = Mathf.Max(0,value); }
    }
    public int Armor {
        get { return Mathf.Max(0,ArmorBase + ArmorMod); }
        set { ArmorBase = Mathf.Max(0,value); }
    }

    public TileMap map;
    public Pole[,] mapa;
    List<Effect> Effects = new List<Effect>();
    public int MovementRange = 3;
    public bool animated = false;
    public bool CanMove = true;
    public bool CanAct = true;
    int lastUsedSkill;
    Animator anim;
    public bool agresive = true;//defense offens
    public List<Skill> Skile = new List<Skill>();

    public void EndRound() {
        CanMove = false;
        CanAct = false;
    }
    public void NewRound() {
        foreach (Effect i in Effects) {
            if (i.Triger == 1) {
                i.Func(0, this);
            }
            i.Duration--;
            if (i.Duration <= 0 && i.OnDestroy != null) {
                i.OnDestroy(0, this);
            }
        }
        Effects.RemoveAll(item => item.Duration <= 0);

        AP += APacc;
        CanMove = true;
        CanAct = true;
    }
    private void Awake() {
        //Effect e = new Effect(0,3, (int dmg, Unit u) => { return 0; });

        if (animated) {
            anim = GetComponent<Animator>();
        }
        //Skile.Add(new Skill(new Vector2(2, 3)));
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
        Skile.Add(new Skill(tmp2, new Vector2(2, 3), 1, 0, null, false, 6));
    }
    public Skill GetSkill(int k) {
        if (k >= Skile.Count) {
            return null;
        }
        lastUsedSkill = k;
        return Skile[k];
    }
    void Die() {
        map.UnitDie(this);
        //animation
        Destroy(gameObject);
    }
    public void GetAttack(int dmg, Effect e = null) {
        if (e != null) {
            Effect prim = Effects.Find(item => item.Func == e.Func);
            if (prim != null) {
                prim.OnDestroy(0, this);
                Effects.Remove(prim);
            }
            Effects.Add(e);
            if (e.Triger == 2) {
                e.Func(dmg, this);
            }
        }
        foreach (Effect i in Effects) {
            if (i.Triger == 0) {
                dmg = i.Func(dmg, this);
            }
        }
        dmg -= Armor;
        dmg = Mathf.Max(0, dmg);
        HP = HP- dmg;
    }
    public void MovingOnTiles(Queue<Pole> v) {
        CanMove = false;
        StartCoroutine(Steps(v));
    }
    public void CastSkill(Skill s) {
        AP -= s.Cost;
        CanAct = false;
        CanMove = false;
        anim.SetTrigger(s.trigger);
        StartCoroutine(WaitForAnim());
    }
    IEnumerator Steps(Queue<Pole> v) {//użyć passible
        if (v != null) {
            if (animated) {
                map.tiles[tileX, tileY].Unit = null;
                tileX = v.Peek().X;
                tileY = v.Peek().Y;
                map.tiles[tileX, tileY].Unit = this;
                Vector3 vec = map.tiles[tileX, tileY].transform.position;
                while (v.Count > 0) {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(vec - transform.position), 8f);
                    anim.SetFloat("Forward", 1f, 0.1f, Time.deltaTime);
                    if (Vector3.Distance(vec, transform.position) < 0.2f) {
                        v.Dequeue();
                        if (v.Count > 0) {
                            map.tiles[tileX, tileY].Unit = null;
                            tileX = v.Peek().X;
                            tileY = v.Peek().Y;
                            map.tiles[tileX, tileY].Unit = this;
                            vec = map.tiles[tileX, tileY].transform.position;
                        }
                    }
                    yield return 0;
                }
                map.GenerateMap(this);
                map.ClearTiles();
                while (anim.GetFloat("Forward") > 0.01f) {
                    anim.SetFloat("Forward", 0f, 0.3f, Time.deltaTime);
                    yield return 0;
                }
                anim.SetFloat("Forward", 0);
            } else {
                while (v.Count > 0) {
                    map.tiles[tileX, tileY].Unit = null;
                    tileX = v.Peek().X;
                    tileY = v.Peek().Y;
                    map.tiles[tileX, tileY].Unit = this;
                    transform.position = map.tiles[tileX, tileY].transform.position;
                    v.Dequeue();
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
        map.wait = false;
    }


    public IEnumerator WaitForAnim() {
        AnimatorClipInfo cl = anim.GetCurrentAnimatorClipInfo(0)[0];
        while (anim.GetCurrentAnimatorClipInfo(0)[0].clip == cl.clip) {
            yield return 0;
        }
        float d = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length /5;
        yield return new WaitForSeconds(d);
        map.wait = false;
    }
}
