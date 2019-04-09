using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Unit : MonoBehaviour {
    public int tileX;
    public int tileY;
    public int MaxHP = 5, MaxAP = 10;//private
    public int ArmorBase = 0, APaccBase = 2;//private
    public int ArmorMod = 0, APaccMod = 2;
    public int luck = 0;
    public int ap = 2;//private
    public int AP {
        get { return ap; }
        set { ap = Mathf.Clamp(value, 0, MaxAP); }
    }
    public int Luck {
        get { return luck; }
        set { luck = Mathf.Max(value, 0); }
    }
    public int hp = 5;//private
    public int HP {
        get { return hp; }
        set {
            hp = Mathf.Clamp(value, 0, MaxHP);
            StartCoroutine(HPChange(hp));
            if (hp == 0) Die();
        }
    }
    public int APacc {
        get { return Mathf.Max(0, APaccBase + APaccMod); }
        set { APaccBase = Mathf.Max(0, value); }
    }
    public int Armor {
        get { return Mathf.Max(0, ArmorBase + ArmorMod); }
        set { ArmorBase = Mathf.Max(0, value); }
    }

    public TileMap map;
    public Pole[,] mapa;
    public Slider HPBar;
    public Text HPText;
    public Transform Panel;
    internal List<Effect> Effects = new List<Effect>();
    public int MovementRange = 3;
    public bool animated = false;
    public bool CanMove = true;
    public bool CanAct = true;
    internal int lastUsedSkill;
    internal Animator anim;
    public bool agresive = true;//defense offens
    public List<Skill> Skile = new List<Skill>();
    Coroutine rut;

    public void EndRound() {
        CanMove = false;
        CanAct = false;
    }
    private void Start() {
        if (animated) {
            anim = GetComponent<Animator>();
        }
        HP = MaxHP;
        HPBar.maxValue = MaxHP;
        HPText.text = HP.ToString();
    }
    private void Update() {
        Panel.transform.position = Camera.main.WorldToScreenPoint(transform.position);
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
    public Skill GetSkill(int k) {
        if (k >= Skile.Count) {
            return null;
        }
        lastUsedSkill = k;
        return Skile[k];
    }
    void Die() {
        map.UnitDie(this);
        anim.SetTrigger("Die");
        Destroy(this);
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
        anim.SetTrigger("Hit");
        dmg -= Armor;
        dmg = Mathf.Max(0, dmg);
        HP = HP - dmg;

    }
    public void MovingOnTiles(Queue<Pole> v) {
        CanMove = false;
        StartCoroutine(Steps(v));
    }
    public void CastSkill(Posibilieties p) {
        AP -= p.useSkill.Cost;
        CanAct = false;
        CanMove = false;
        anim.SetTrigger(p.useSkill.trigger);
        map.ClearTiles();
        StartCoroutine(RotateTowards(map.tiles[(int)p.target.x, (int)p.target.y].transform.position));
        StartCoroutine(WaitForAnim());
    }
    IEnumerator Steps(Queue<Pole> v) {
        if (v != null) {
            if (animated) {
                if (map != null && map.tiles != null && map.tiles[tileX, tileY] != null) {
                    map.tiles[tileX, tileY].Unit = null;
                }
                tileX = v.Peek().X;
                tileY = v.Peek().Y;
                map.tiles[tileX, tileY].Unit = this;
                Vector3 vec = map.tiles[tileX, tileY].transform.position;
                rut = StartCoroutine(GoTo(vec));
                while (v.Count > 0) {
                    vec.y = transform.position.y;
                    if (Vector3.Distance(vec, transform.position) < 0.20f) {
                        v.Dequeue();
                        if (v.Count > 0) {
                            map.tiles[tileX, tileY].Unit = null;
                            tileX = v.Peek().X;
                            tileY = v.Peek().Y;
                            map.tiles[tileX, tileY].Unit = this;
                            vec = map.tiles[tileX, tileY].transform.position;
                            StopCoroutine(rut);
                            rut = StartCoroutine(GoTo(vec));
                        }
                    }
                    yield return 0;
                }
                map.GenerateMap(this);
                map.ClearTiles();
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
    public void MoveToPos(Vector3 pos) {
        rut = StartCoroutine(GoTo(pos));
    }
    IEnumerator GoTo(Vector3 pos) {
        pos.y = transform.position.y;
        while (Vector3.Distance(pos, transform.position) > 0.2f) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(pos - transform.position), 8f);
            anim.SetFloat("Forward", 1f, 0.1f, Time.deltaTime);
            yield return 0;
        }
        while (anim.GetFloat("Forward") > 0.01f) {
            anim.SetFloat("Forward", 0f, 0.3f, Time.deltaTime);
            yield return 0;
        }
        anim.SetFloat("Forward", 0);
    }

    IEnumerator HPChange(int hp) {
        HPText.text = hp.ToString();
        float old = HPBar.value;
        float delta = 0;
        while (delta < 1f) {
            HPBar.value = Mathf.Lerp(old, (float)hp, delta);
            delta += Time.deltaTime * 1;
            yield return null;
        }
    }

    IEnumerator WaitForAnim() {
        AnimatorClipInfo cl = anim.GetCurrentAnimatorClipInfo(0)[0];
        while (anim.GetCurrentAnimatorClipInfo(0)[0].clip == cl.clip) {
            yield return 0;
        }
        float d = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length / 5;
        yield return new WaitForSeconds(d);
        map.wait = false;
    }
    IEnumerator RotateTowards(Vector3 target) {
        Quaternion to = Quaternion.LookRotation(target - transform.position);
        while (Quaternion.Angle(transform.rotation, to)>1f){
            transform.rotation = Quaternion.RotateTowards(transform.rotation, to, 8f);
            yield return 0;
        }

    }
}
