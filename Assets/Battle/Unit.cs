using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Klasa jednostek bojowych
/// </summary>
public class Unit : MonoBehaviour {
    internal bool freezRotation = false;
    public float BreakDistance = 0.22f;
    public int tileX;
    public int tileY;
    public int MaxHP = 5, MaxAP = 10;
    public int ArmorBase = 0, APaccBase = 2, luckBase = 0;
    public int ArmorMod = 0, APaccMod = 0;
    public int LuckMod = 0, MovMod = 0;
    public int ap = 0;
    public int MovRange = 3;
    public Transform Hand;
    public GameObject hudPrefab;
    public Hud hud;

    internal TileMap map;
    public Pole[,] mapa;
    internal List<Effect> Effects = new List<Effect>();
    public bool CanMove = true;
    public bool CanAct = true;
    internal int lastUsedSkill;
    internal Animator anim;
    internal int bestDist = 0;
    public bool agresive = true;//defense offens
    public List<Skill> Skile = new List<Skill>();
    public List<Item> Items = new List<Item>();
    GameObject weapon;
    Coroutine rut1, rut2, rut3;

    public int AP {
        get { return ap; }
        set { ap = Mathf.Clamp(value, 0, MaxAP); }
    }
    public int Luck {
        get { return Mathf.Max(0, luckBase + LuckMod); }
        set { luckBase = Mathf.Max(value, 0); }
    }
    public int hp = 5;//private
    public int HP {
        get { return hp; }
        set {
            hp = Mathf.Clamp(value, 0, MaxHP);
            if (hp <= 0) Die();
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
    public int Movement {
        get { return Mathf.Max(0, MovRange + MovMod); }
        set { MovRange = Mathf.Max(0, value); }
    }
    /// <summary>
    /// kończy ture i blokuje ruchy jednostki
    /// </summary>
    public void EndRound() {
        CanMove = false;
        CanAct = false;
    }
    private void Awake() {
        anim = GetComponent<Animator>();
    }
    public void createHud() {
        GameObject go = Instantiate(hudPrefab);
        hud = go.GetComponent<Hud>();
    }
    public virtual void Activ() {
    }

    /// <summary>
    /// nowa runda odnawia Punkty Akcji umożliwia ruch jednostki i aktywuje niektóre nałożone efekty
    /// </summary>
    public void NewRound() {
        if (HP > 0) hud.Upd();
        foreach (Effect i in Effects) {
            if (i.Triger == Effect.trig.OnTurnStart) {
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
        if (HP > 0) hud.Upd();
    }
    /// <summary>
    /// zwraca umiejętność z pozycja k
    /// </summary>
    /// <param name="k">pozycja</param>
    /// <returns>umiejętność</returns>
    public Skill GetSkill(int k) {
        if (k > 8) {
            return null;
        }
        if (k > 2) {
            return Items[k - 3].getskill();
        }
        lastUsedSkill = k;
        return Skile[k];
    }
    /// <summary>
    /// jednostka umiera
    /// </summary>
    void Die() {
        anim.SetTrigger("Die");
        hud.Deactiv();
        //Destroy(hud.gameObject);
        map.UnitDie(this);
    }
    /// <summary>
    /// funkcja otrzymywania obrażeń i efektów
    /// </summary>
    /// <param name="dmg">obrażenia</param>
    /// <param name="e">efekt</param>
    public void GetAttack(int dmg, Effect e = null) {
        if (e != null) {
            Effect prim = Effects.Find(item => item.Func == e.Func);
            if (prim != null) {
                prim.OnDestroy(0, this);
                Effects.Remove(prim);
            }
            Effects.Add(e);
            if (e.Triger == Effect.trig.OnApply) {
                e.Func(dmg, this);
            }
        }
        foreach (Effect i in Effects) {
            if (i.Triger == Effect.trig.BeforeGetHit && i.Func != null) {
                dmg = i.Func(dmg, this);
            }
        }
        int bdmg = dmg;
        dmg -= Armor;
        dmg = Mathf.Max(0, dmg);
        HP = HP - dmg;
        if (dmg > 0 && HP > 0) {
            anim.SetTrigger("Hit");
        }
        if (HP > 0) hud.Upd();

    }
    /// <summary>
    /// zwraca prawdopodobieństwo zadania obrażeń krytycznych
    /// </summary>
    /// <returns>prawdopodobieństwo</returns>
    public float TestLuck() {
        float RandomValue = Random.value;
        float luckNorm = (float)(10 - Luck) / 10f;
        if (RandomValue > luckNorm) {
            return 1.5f;
        }
        return RandomValue / luckNorm;
    }
    /// <summary>
    /// rozpoczyna rutyne przejścia po polach walki
    /// </summary>
    /// <param name="v">kolejka pól</param>
    public void MovingOnTiles(Queue<Pole> v) {
        CanMove = false;
        if (rut1 != null) StopCoroutine(rut1);
        rut1 = StartCoroutine(Steps(v));
    }
    /// <summary>
    /// prezentacja wizualna wykorzystania umiejętności i blokowanie dalszych działań do jej końca
    /// </summary>
    /// <param name="p">dostępna możliwość</param>
    public void CastSkill(Posibilieties p) {
        AP -= p.useSkill.Cost;
        CanAct = false;
        CanMove = false;
        map.ClearTiles(true);
        hud.Upd();
        Destroy(weapon);
        if (p.useSkill.Model != null) weapon = Instantiate(p.useSkill.Model, Hand.position, Hand.rotation, Hand);
        if (p.useSkill.trigger != "Wait") {
            anim.SetTrigger(p.useSkill.trigger);
            if (rut3 != null) StopCoroutine(rut3);
            rut3 = StartCoroutine(RotateTowards(map.tiles[(int)p.target.x, (int)p.target.y].transform.position));
            StartCoroutine(WaitForAnim());
        } else {
            map.wait = false;
        }
    }
    /// <summary>
    /// rutyna przejścia po kolejce pól
    /// </summary>
    /// <param name="v">kolejka pól</param>
    IEnumerator Steps(Queue<Pole> v) {
        if (v != null) {
            if (v.Count > 0) {
                if (map != null && map.tiles != null && map.tiles[tileX, tileY] != null) {
                    map.tiles[tileX, tileY].Unit = null;
                }
                tileX = v.Peek().X;
                tileY = v.Peek().Y;
                map.tiles[tileX, tileY].Unit = this;
                Vector3 vec = map.tiles[tileX, tileY].transform.position;
                if (rut2 != null) StopCoroutine(rut2);
                rut2 = StartCoroutine(GoTo(vec));
                while (v.Count > 0) {
                    vec.y = transform.position.y;
                    if (Vector3.Distance(vec, transform.position) < BreakDistance) {
                        v.Dequeue();
                        if (v.Count > 0) {
                            map.tiles[tileX, tileY].Unit = null;
                            tileX = v.Peek().X;
                            tileY = v.Peek().Y;
                            map.tiles[tileX, tileY].Unit = this;
                            vec = map.tiles[tileX, tileY].transform.position;
                            if (rut2 != null) StopCoroutine(rut2);
                            rut2 = StartCoroutine(GoTo(vec));
                        }
                    }
                    yield return 0;
                }
                map.GenerateMap(this);
                map.ClearTiles();
            } else {
                map.tiles[tileX, tileY].Unit = this;
                if (!freezRotation) {
                    transform.position = map.tiles[tileX, tileY].transform.position;
                }
            }
        }
        map.wait = false;
    }
    /// <summary>
    /// rozpoczyna rutyne przejścia do pozycji w świecie
    /// </summary>
    /// <param name="pos">pozycja</param>
    public void MoveToPos(Vector3 pos) {
        if (rut2 != null) StopCoroutine(rut2);
        rut2 = StartCoroutine(GoTo(pos));
    }
    /// <summary>
    /// rutyna przejścia do pozycji w świecie
    /// </summary>
    /// <param name="pos">pozycja</param>
    IEnumerator GoTo(Vector3 pos) {
        pos.y = transform.position.y;
        if (anim == null) {
            anim = GetComponent<Animator>();
        }
        List<AnimatorControllerParameter> v = new List<AnimatorControllerParameter>(anim.parameters);
        if (v.Find(item => item.name == "Forward") != null) {

            while (Vector3.Distance(pos, transform.position) > BreakDistance) {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(pos - transform.position), 8f);
                anim.SetFloat("Forward", 1f, 0.1f, Time.deltaTime);
                yield return 0;
            }
            while (anim.GetFloat("Forward") > 0.01f) {
                anim.SetFloat("Forward", 0f, 0.1f, Time.deltaTime);
                yield return 0;
            }
            anim.SetFloat("Forward", 0);
        } else {
            map.wait = false;
        }
    }
    /// <summary>
    /// rutyna blokująca inne działania do czasu wykonania połowy animacji
    /// </summary>
    IEnumerator WaitForAnim() {
        AnimatorClipInfo cl = anim.GetCurrentAnimatorClipInfo(0)[0];
        while (anim.GetCurrentAnimatorClipInfo(0)[0].clip == cl.clip) {
            yield return 0;
        }
        float d = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length / 5;
        yield return new WaitForSeconds(d);
        Destroy(weapon);
        map.wait = false;
    }
    /// <summary>
    /// rutyna obracająca postać w kierunku celu
    /// </summary>
    /// <param name="target">cel</param>
    IEnumerator RotateTowards(Vector3 target) {
        if (!freezRotation) {
            Quaternion to = Quaternion.LookRotation(target - transform.position);
            while (Quaternion.Angle(transform.rotation, to) > 1f) {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, to, 8f);
                yield return 0;
            }
        }
    }
}
