using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
public class Pole {
    public int X, Y;
    public Queue<Pole> path;
    public float dist;
    public float PosPoints = 0;
    public List<Vector2> toPlayer = new List<Vector2>();
    public List<Vector2> toBot = new List<Vector2>();
    public Pole() { }
    public Pole(int xx, int yy) {
        path = new Queue<Pole>();
        X = xx; Y = yy; dist = 0;
    }
    public Pole(int xx, int yy, Queue<Pole> p, float d) {
        path = new Queue<Pole>(p);
        path.Enqueue(this);
        X = xx; Y = yy; dist = d;
    }
};
public class Posibilieties {
    public int BestDir = 0;
    public float Points = 0;
    public Skill useSkill;
    public Unit executor;
    public Vector2 target;
    public Pole pole;
    public Posibilieties CoordinatedWith;
    public float SumPoints() {
        float odp = Points + pole.PosPoints;
        if (CoordinatedWith != null) {
            odp += CoordinatedWith.Points;
            odp += CoordinatedWith.pole.PosPoints;
        }
        return odp;
    }
    public Posibilieties ShallowCopy() {
        return (Posibilieties)this.MemberwiseClone();
    }
}
public class TileMap : MonoBehaviour {
    readonly Vector2[] neighbor = new Vector2[4] {new Vector2(0, 1),//up
                                            new Vector2(0, -1),//down
                                            new Vector2(-1, 0),//left
                                            new Vector2(1, 0)};//right
    public Unit selectedUnit;
    public GameObject VisualPrefab;
    public ClickableTile[,] tiles;
    List<Unit> Units;
    List<Unit> PlayerUnits = new List<Unit>();
    List<Unit> EnemyUnits = new List<Unit>();
    public Pole[,] map;
    Skill selectedSkill;
    bool playerTurn = true;
    public bool wait = false;
    
    public int mapSizeX = 10;
    public int mapSizeY = 10;
    float far;

    public void UnitDie(Unit u) {
        Units.Remove(u);
        PlayerUnits.Remove(u);
        EnemyUnits.Remove(u);
        tiles[u.tileX, u.tileY].Unit = null;
    }

    private void Start() {
        far = mapSizeX + mapSizeY;
        tiles = new ClickableTile[mapSizeX, mapSizeY];
        MakeTiles();
    }
    public void PlayerEndTurn() {
        playerTurn = false;
        foreach (Unit u in PlayerUnits) {
            u.EndRound();
        }
        foreach (Unit u in EnemyUnits) {
            u.NewRound();
        }
        foreach(Unit u in PlayerUnits) {
            u.hud.Unselect();
        }
        AiBehavior();
    }
    public void EnemyEndTurn() {
        playerTurn = true;
        foreach (Unit u in EnemyUnits) {
            u.EndRound();
        }
        foreach (Unit u in PlayerUnits) {
            u.NewRound();
        }
        if (PlayerUnits.Count > 0) {
            selectedUnit = PlayerUnits[0];
            selectedUnit.hud.Select();
            GenerateMap(selectedUnit);
            ClearTiles();
        }
    }
    void Update() {
        if (Input.GetButtonDown("Fire2")) {
            selectedSkill = null;
            ClearTiles();
        }
    }
    void MakeTiles() {// tworzy objekty pól
        Quaternion rem = transform.rotation;
        transform.rotation = Quaternion.identity;
        for (int x = 0; x < mapSizeX; x++) {
            for (int y = 0; y < mapSizeY; y++) {
                GameObject go = (GameObject)Instantiate(VisualPrefab, new Vector3(x * VisualPrefab.transform.localScale.x * 1.1f, VisualPrefab.transform.localScale.y / 2, y * VisualPrefab.transform.localScale.z * 1.1f) + transform.position, Quaternion.identity, transform);
                ClickableTile ct = go.GetComponent<ClickableTile>();
                ct.tileX = x;
                ct.tileY = y;
                ct.map = this;
                tiles[x, y] = ct;
            }
        }
        transform.rotation = rem;
        foreach (ClickableTile t in tiles) {
            t.Init();
        }
        Units = FindObjectsOfType<Unit>().ToList();
        foreach (Unit u in Units) {
            Vector3 vec = u.transform.position;
            Vector3 center = tiles[mapSizeX / 2, mapSizeY / 2].transform.position;
            if (Mathf.Abs(vec.x - center.x) > VisualPrefab.transform.localScale.x * (mapSizeX / 2 + 2) || Mathf.Abs(vec.z - center.z) > VisualPrefab.transform.localScale.z * (mapSizeY / 2 + 2)) {
                Debug.Log("out of battleground");
                continue;
            }
            if (u is Ai) {
                EnemyUnits.Add(u);
                u.hud.Activ(u, false);
            } else {
                PlayerUnits.Add(u);
                PlayerUnit uu = (PlayerUnit)u;
                uu.Activ();
                u.hud.Activ(u, true);
            }
            int ux = Mathf.CeilToInt((vec.x - tiles[0, 0].transform.position.x) / VisualPrefab.transform.localScale.x), uy = Mathf.CeilToInt((vec.z - tiles[0, 0].transform.position.z) / VisualPrefab.transform.localScale.z)+1;
            ux = Mathf.Clamp(ux, 0, mapSizeX - 1);
            uy = Mathf.Clamp(uy, 0, mapSizeY - 1);
            int tx = ux, ty = uy;
            int i = 0;
            while ((tiles[ux, uy].Unit != null || tiles[ux, uy].type > 1) && i < 4) {
                Vector2 a = new Vector2(tx, ty) + neighbor[i];
                if (NotOutOfRange(a)) {
                    ux = (int)a.x;
                    uy = (int)a.y;
                }
                i++;
            }
            u.map = this;
            tiles[ux, uy].Unit = u;
            u.tileX = ux;
            u.tileY = uy;
            u.MoveToPos(new Vector3(tiles[ux, uy].transform.position.x, u.transform.position.y, tiles[ux, uy].transform.position.z));
            Debug.Log("asign" + ux.ToString() + uy.ToString());
        }
        EnemyEndTurn();
    }
    bool NotOutOfRange(Vector2 a) {
        return NotOutOfRange((int)a.x, (int)a.y);
    }
    bool NotOutOfRange(int x, int y) {
        if (x < 0 || y < 0 || x >= mapSizeX || y >= mapSizeY) {
            return false;
        }
        return true;
    }

    public void AtackMode() {
        if (playerTurn == false || selectedUnit == null || selectedSkill == null) {
            return;
        }
        if (selectedSkill.AttackRange > 0) {//atak zasiegowy
            for (int y = 0; y < mapSizeY; y++) {
                for (int x = 0; x < mapSizeX; x++) {
                    if (Mathf.Abs(selectedUnit.tileX - x) + Mathf.Abs(selectedUnit.tileY - y) < selectedSkill.AttackRange) {
                        tiles[x, y].Stat(4);
                    } else {
                        tiles[x, y].Stat(0);
                    }
                    if (selectedSkill.Area.Length > 1 && (Mathf.Abs(selectedUnit.tileX - x) == selectedSkill.AttackRange - 1) || Mathf.Abs(selectedUnit.tileY - y) == selectedSkill.AttackRange - 1) {
                        tiles[x, y].Stat(0);
                    }
                }
            }
        } else {//atak mele
            for (int y = 0; y < mapSizeY; y++) {
                for (int x = 0; x < mapSizeX; x++) {
                    tiles[x, y].Stat(0);
                }
            }
            for (int i = 0; i < 4; i++) {
                Vector2 a = new Vector2(selectedUnit.tileX, selectedUnit.tileY) + neighbor[i];
                if (NotOutOfRange(a)) {
                    tiles[(int)a.x, (int)a.y].Stat(4);
                }
            }
        }
    }

    public void AtackMode(int k) {
        if (selectedUnit == null) {
            return;
        }
        selectedSkill = selectedUnit.GetSkill(k);
        AtackMode();
    }
    public void MarkAtack(int x, int y) {//zaznacza obraca odpowiednio atakowany obszar na czerwono

        if (playerTurn == false || selectedUnit == null || selectedSkill == null) {
            return;
        }
        int xx, yy;
        Vector2 l = new Vector2(x - selectedUnit.tileX, y - selectedUnit.tileY);
        int a = GetQuoter(l);
        foreach (Vector2 v in selectedSkill.DirectedArea[a]) {
            xx = x + (int)v.x;
            yy = y + (int)v.y;
            if (NotOutOfRange(xx, yy)) {
                tiles[xx, yy].Stat(5);
            }
        }
    }
    public void TileClicked(int x, int y) {
        if (playerTurn == false) {
            return;
        }
        if (selectedSkill != null && selectedUnit != null && selectedUnit.CanAct) {
            if (selectedSkill.AttackRange > 0) {//check for attack
                if (Mathf.Abs(selectedUnit.tileX - x) + Mathf.Abs(selectedUnit.tileY - y) >= selectedSkill.AttackRange) {
                    return;
                }
            } else {
                bool check = false;
                Vector2 a = new Vector2(selectedUnit.tileX - x, selectedUnit.tileY - y);
                for (int i = 0; i < 4; i++) {
                    if (a == neighbor[i]) {
                        check = true;
                    }
                }
                if (!check) {
                    Debug.Log("atack canceled");
                    return;
                }
            }

            Posibilieties p = new Posibilieties() {
                target = new Vector2(x, y),
                BestDir = GetQuoter(new Vector2(x, y) - new Vector2(selectedUnit.tileX, selectedUnit.tileY)),
                executor = selectedUnit,
                useSkill = selectedSkill
            };
            StartCoroutine(DealAttack(p));
        }
        if (selectedSkill == null) {//select or go
            if (tiles[x, y].Unit != null) {
                if (PlayerUnits.Contains(tiles[x, y].Unit)) {
                    selectedUnit = tiles[x, y].Unit;
                    GenerateMap(selectedUnit);
                    ClearTiles();
                }
            } else {
                if (selectedUnit != null && selectedUnit.CanMove) {
                    GoTo(x, y);
                }
            }
        }
    }
    public static int IntDistance(int x1, int y1, Vector2 b) {
        return IntDistance(x1, y1, (int)b.x, (int)b.y);
    }
    public static int IntDistance(Vector2 a, Vector2 b) {
        return IntDistance((int)a.x, (int)a.y, (int)b.x, (int)b.y);
    }
    public static int IntDistance(int x1, int y1, int x2, int y2) {
        int xx = x1 - x2;
        int yy = y1 - y2;
        return Mathf.Abs(xx) + Mathf.Abs(yy);
    }

    public int GetQuoter(Vector2 a) {
        // 003
        // 2 3
        // 211
        int odp = 1;
        if (a.y >= 0) {
            if (a.x > 0) {
                odp = 3;
            }
        }
        if (a.y > 0) {
            if (a.x <= 0) {
                odp = 0;
            }
        } else {
            if (a.x < 0) {
                odp = 2;
            }
        }
        return odp;
    }
    public void GenerateMap(Unit u, bool Ignore = false, List<Vector2> Obstacle = null) {//generuje mape chodzenia
        int maxDist = 0;
        if (u.CanMove) {
            maxDist = u.Movement;
        }
        u.mapa = new Pole[mapSizeX, mapSizeY];
        map = u.mapa;
        Queue<Pole> tmp = new Queue<Pole>();
        tmp.Enqueue(new Pole(u.tileX, u.tileY));
        while (tmp.Count > 0) {
            Pole temp = tmp.Peek();
            if (temp.dist < maxDist + 1) {
                if (u.mapa[temp.X, temp.Y] == null || u.mapa[temp.X, temp.Y].dist > temp.dist) {
                    u.mapa[temp.X, temp.Y] = temp;
                    for (int i = 0; i < 4; i++) {
                        Vector2 a = new Vector2(temp.X, temp.Y) + neighbor[i];
                        if (NotOutOfRange(a)) {
                            if (Obstacle == null || !Obstacle.Contains(a)) {
                                float cost = temp.dist + tiles[(int)a.x, (int)a.y].MovingCost(Ignore);
                                tmp.Enqueue(new Pole((int)a.x, (int)a.y, temp.path, cost));
                            }
                        }
                    }
                }
            }
            tmp.Dequeue();
        }
        foreach (Pole m in u.mapa) {
            if (m == null) {
                continue;
            }
            foreach (Unit pu in PlayerUnits) {
                Vector2 dif = new Vector2(m.X - pu.tileX, m.Y - pu.tileY);
                m.toPlayer.Add(dif);
                if (u.agresive) {
                    m.PosPoints += (far - dif.magnitude) * 0.01f;
                } else {
                    m.PosPoints += (dif.magnitude) * 0.01f;
                }
            }
            foreach (Unit pu in EnemyUnits) {
                Vector2 dif = new Vector2(m.X - pu.tileX, m.Y - pu.tileY);
                m.toBot.Add(dif);
            }
        }
    }
    public void AiBehavior() {
        List<Posibilieties> filds = new List<Posibilieties>();
        List<Vector2> obstacle = new List<Vector2>();
        foreach (Unit u in PlayerUnits) {
            Vector2 v = new Vector2(u.tileX, u.tileY);
            obstacle.Add(v);
        }
        foreach (Ai u in EnemyUnits) {//generuje mape chodzenia i dodaje vektory do celów
            GenerateMap(u, true, obstacle);
        }
        foreach (Ai u in EnemyUnits) {//self
            foreach (Skill s in u.Skile) {
                if (s.moment == 3 && s.Cost <= u.AP) {
                    Pole maxPosPoints = new Pole();
                    foreach (Pole p in u.mapa) {
                        if (p != null && maxPosPoints.PosPoints < p.PosPoints) {
                            maxPosPoints = p;
                        }
                    }
                    Posibilieties q = new Posibilieties {
                        BestDir = 0,
                        target = new Vector2(maxPosPoints.X, maxPosPoints.Y) + neighbor[0],
                        executor = u,
                        pole = maxPosPoints,
                        useSkill = s,
                        Points = 0
                    };
                    filds.Add(q);
                }
            }
        }

        List<Vector2> pla = new List<Vector2>();
        foreach (Unit u in PlayerUnits) {
            pla.Add(new Vector2(u.tileX, u.tileY));
        }
        List<Vector2> enem = new List<Vector2>();
        foreach (Unit u in EnemyUnits) {
            enem.Add(new Vector2(u.tileX, u.tileY));
        }
        foreach (Ai u in EnemyUnits) {//atack
            foreach (Skill s in u.Skile) {
                if (s.moment < 2 && s.Cost <= u.AP) {
                    if (s.AttackRange > 0) {
                        if (s.positive) {
                            filds.AddRange(RangeAtak(s, u, enem));
                        } else {
                            filds.AddRange(RangeAtak(s, u, pla));
                        }
                    } else {
                        if (s.positive) {
                            filds.AddRange(MeleAtak(s, u, enem));
                        } else {
                            filds.AddRange(MeleAtak(s, u, pla));
                        }
                    }
                }
            }
        }

        foreach (Ai u in EnemyUnits) {//support
            foreach (Skill s in u.Skile) {
                if (s.moment == 2 && s.AttackRange == 0 && s.Cost <= u.AP) {
                    List<Posibilieties> enemEnd = new List<Posibilieties>();
                    foreach (Posibilieties posi in filds) {
                        if (posi.executor != u && posi.executor.agresive) {
                            enemEnd.Add(posi);
                        }
                    }
                    if (enemEnd.Count == 0) {
                        foreach (Posibilieties posi in filds) {
                            if (posi.executor != u) {
                                enemEnd.Add(posi);
                            }
                        }
                    }
                    filds.AddRange(SupportAtak(s, u, enemEnd));
                }
            }
        }
        Debug.Log("sum1");
        var ord = filds.OrderByDescending(item => item.SumPoints());
        List<Posibilieties> Schedule = new List<Posibilieties>();
        obstacle = new List<Vector2>();
        foreach (Unit u in Units) {
            Vector2 v = new Vector2(u.tileX, u.tileY);
            obstacle.Add(v);
        }
        int executors = 0;
        float sh1 = 0, sh2 = 0;
        foreach (Posibilieties p in ord) {
            if (executors == EnemyUnits.Count) {
                break;
            }
            if (Schedule.Find(item => item.executor == p.executor) != null || (p.CoordinatedWith != null && Schedule.Find(item => item.CoordinatedWith.executor == p.executor) != null)) {
                continue;
            }
            GenerateMap(p.executor, true, obstacle);
            if (p.executor.mapa[p.pole.X, p.pole.Y] != null) {
                p.pole = p.executor.mapa[p.pole.X, p.pole.Y];
                obstacle.Add(new Vector2(p.pole.X, p.pole.Y));
                obstacle.Remove(new Vector2(p.executor.tileX, p.executor.tileY));
                if (p.CoordinatedWith != null) {
                    GenerateMap(p.CoordinatedWith.executor, true, obstacle);
                    if (p.CoordinatedWith.executor.mapa[p.CoordinatedWith.pole.X, p.CoordinatedWith.pole.Y] == null) {
                        obstacle.Remove(new Vector2(p.pole.X, p.pole.Y));
                        obstacle.Add(new Vector2(p.executor.tileX, p.executor.tileY));
                        continue;
                    }
                    p.CoordinatedWith.pole = p.CoordinatedWith.executor.mapa[p.CoordinatedWith.pole.X, p.CoordinatedWith.pole.Y];
                    executors++;
                    obstacle.Add(new Vector2(p.CoordinatedWith.pole.X, p.CoordinatedWith.pole.Y));
                    obstacle.Remove(new Vector2(p.CoordinatedWith.executor.tileX, p.CoordinatedWith.executor.tileY));
                }

                executors++;
                Schedule.Add(p);
                Debug.Log("sum2");
                sh1 += p.SumPoints();
            }
        }

        List<Posibilieties> Schedule2 = new List<Posibilieties>();
        obstacle = new List<Vector2>();
        foreach (Unit u in Units) {
            Vector2 v = new Vector2(u.tileX, u.tileY);
            obstacle.Add(v);
        }
        executors = 0;
        foreach (Posibilieties p in ord) {
            if (executors == EnemyUnits.Count) {
                break;
            }
            if (p.CoordinatedWith != null || Schedule2.Find(item => item.executor == p.executor) != null) {
                continue;
            }
            GenerateMap(p.executor, true, obstacle);
            if (p.executor.mapa[p.pole.X, p.pole.Y] != null) {
                p.pole = p.executor.mapa[p.pole.X, p.pole.Y];
                executors++;
                Schedule2.Add(p);
                Debug.Log("sum3");
                sh2 += p.SumPoints();
                obstacle.Add(new Vector2(p.pole.X, p.pole.Y));
                obstacle.Remove(new Vector2(p.executor.tileX, p.executor.tileY));
            }
        }

        foreach (Posibilieties p in Schedule) {
            Debug.Log("schedule" + p.executor.tileX + p.executor.tileY + p.pole.X + p.pole.Y + " " + p.Points + " " + p.pole.PosPoints);
            if (p.CoordinatedWith != null) {
                Debug.Log("coord" + p.CoordinatedWith.executor.tileX + p.CoordinatedWith.executor.tileY + p.CoordinatedWith.pole.X + p.CoordinatedWith.pole.Y);
            }
        }
        foreach (Posibilieties p in Schedule2) {
            Debug.Log("schedule2" + p.executor.tileX + p.executor.tileY + p.pole.X + p.pole.Y);
        }
        if (sh2 > sh1) {
            Schedule = Schedule2;
        }
        StartCoroutine(Execute(Schedule));
    }
    IEnumerator Execute(List<Posibilieties> Schedule) {
        for (int i = 0; i < 4; i++) {
            foreach (Posibilieties p in Schedule) {
                if (p.useSkill.moment == i) {
                    wait = true;
                    p.executor.MovingOnTiles(p.pole.path);
                    yield return new WaitUntil(() => wait == false);
                    StartCoroutine(DealAttack(p));
                    yield return new WaitUntil(() => wait == false);
                }
                if (p.CoordinatedWith != null && p.CoordinatedWith.useSkill.moment == i) {
                    wait = true;
                    p.CoordinatedWith.executor.MovingOnTiles(p.CoordinatedWith.pole.path);
                    yield return new WaitUntil(() => wait == false);
                    StartCoroutine(DealAttack(p.CoordinatedWith));
                    yield return new WaitUntil(() => wait == false);
                }
            }
        }
        EnemyEndTurn();
    }
    IEnumerator DealAttack(Posibilieties p) {
        p.executor.AP -= p.useSkill.Cost;
        p.executor.CanAct = false;
        Vector2 AtackPoint = p.target;
        if (p.executor.animated) {
            wait = true;
            p.executor.CastSkill(p);
        }
        yield return new WaitUntil(() => wait == false);
        float TLuck = p.executor.TestLuck();


        int dmg = (int)Mathf.LerpUnclamped(p.useSkill.Dmg.x, p.useSkill.Dmg.y, TLuck);
        //int dmg = (int)Random.Range(p.useSkill.Dmg.x, p.useSkill.Dmg.y);
        Debug.Log("dmg" + dmg);
        foreach (Vector2 v in p.useSkill.DirectedArea[p.BestDir]) {
            Vector2 asd = AtackPoint + v;
            if (NotOutOfRange(asd)) {
                if (p.useSkill.positive) {
                    tiles[(int)asd.x, (int)asd.y].Stat(2);
                    if ((tiles[(int)asd.x, (int)asd.y].Unit is Ai && p.executor is Ai) || (tiles[(int)asd.x, (int)asd.y].Unit is PlayerUnit && p.executor is PlayerUnit)) {
                        tiles[(int)asd.x, (int)asd.y].Unit.GetAttack(dmg, p.useSkill.efect);
                    }
                } else {
                    tiles[(int)asd.x, (int)asd.y].Stat(5);
                    if (tiles[(int)asd.x, (int)asd.y].Unit != null) {
                        tiles[(int)asd.x, (int)asd.y].Unit.GetAttack(dmg, p.useSkill.efect);
                    }
                }
            }
        }
        selectedSkill = null;
        //ClearTiles(true);
    }

    public List<Posibilieties> RangeAtak(Skill s, Ai bot, List<Vector2> tar) {
        List<Posibilieties> posible = new List<Posibilieties>();
        foreach (Pole sp in bot.mapa) {
            if (sp == null) {
                continue;
            }
            List<Vector2> stor = new List<Vector2>();
            Vector2 pos = new Vector2(sp.X, sp.Y);
            foreach (Vector2 v in tar) {
                if (IntDistance(sp.X, sp.Y, v) <= s.AttackRange) {
                    int a = GetQuoter(v - pos);
                    foreach (Vector2 vv in s.DirectedArea[a]) {
                        stor.Add(v - vv);
                    }
                }
            }
            while (stor.Count > 0) {
                var groups = stor.GroupBy(x => x);
                var largest = (groups.OrderByDescending(x => x.Count()).First());
                int distan = IntDistance(sp.X, sp.Y, largest.Key);
                if (distan > s.AttackRange || distan <= s.maxDist) {
                    stor.Remove(largest.Key);
                    continue;
                }
                Posibilieties posil = new Posibilieties {
                    useSkill = s,
                    pole = sp,
                    BestDir = GetQuoter(largest.Key - pos),
                    target = largest.Key,
                    Points = largest.Count() * s.Dmg.y,
                    executor = bot
                };
                //Debug.Log(sp.X + " " + sp.Y + " " + largest.Key);
                posible.Add(posil);
                stor.Clear();
            }
        }
        return posible;
    }

    public List<Posibilieties> MeleAtak(Skill s, Ai bot, List<Vector2> tar) {
        List<Posibilieties> posible = new List<Posibilieties>();

        int max = 0;
        foreach (Pole sp in bot.mapa) {
            if (sp == null) {
                continue;
            }
            int reachable = 0;
            Posibilieties posil = new Posibilieties();
            foreach (Vector2 v in tar) {
                if (IntDistance(sp.X, sp.Y, v) <= s.maxDist) {
                    reachable++;
                }
            }
            if (reachable > 0) {
                posil.useSkill = s;
                posil.pole = sp;
                posil.executor = bot;
                posil.Points = reachable;
                posible.Add(posil);
            }
            max = Mathf.Max(max, reachable);
        }
        posible.RemoveAll(item => item.Points < max);

        foreach (Posibilieties sp in posible) {
            for (int i = 0; i < 4; i++) {
                float PointsCounter = 0;
                foreach (Vector2 v in tar) {
                    foreach (Vector2 pos in s.DirectedArea[i]) {
                        if (new Vector2(sp.pole.X, sp.pole.Y) + pos + neighbor[i] == v) {
                            PointsCounter += s.Dmg.y;
                            break;
                        }
                    }
                }
                if (PointsCounter > sp.Points) {
                    sp.Points = PointsCounter;
                    sp.target = new Vector2(sp.pole.X, sp.pole.Y) + neighbor[i];
                    sp.BestDir = i;
                    sp.useSkill = s;
                }
            }
        }
        //Debug.Log("asd");
        //foreach (Posibilieties sp in posible) {
        //    Debug.Log(sp.pole.X + " " + sp.pole.Y + " poi: " + sp.Points + " dir " + sp.BestDir);
        //}
        return posible;
    }
    public List<Posibilieties> SupportAtak(Skill s, Ai bot, List<Posibilieties> tar) {
        List<Posibilieties> posible = new List<Posibilieties>();
        foreach (Pole p in bot.mapa) {
            if (p == null) {
                continue;
            }
            List<Posibilieties> lis = new List<Posibilieties>();
            for (int i = 0; i < 4; i++) { // każdy kierunek
                foreach (Posibilieties t in tar) { //każdy sojusznik
                    Vector2 v = new Vector2(t.pole.X - p.X, t.pole.Y - p.Y);
                    float PointsCounter = 0;
                    Posibilieties posil = new Posibilieties();
                    foreach (Vector2 pos in s.DirectedArea[i]) { //vektory skila
                        if (pos + neighbor[i] == v) {
                            PointsCounter += 10;
                            //dmg*...
                            break;
                        }
                    }
                    if (PointsCounter > posil.Points) {
                        posil.BestDir = i;
                        posil.target = new Vector2(p.X, p.Y) + neighbor[i];
                        posil.Points = PointsCounter;
                        if (posil.pole == null) {
                            posil.useSkill = s;
                            posil.executor = bot;
                            posil.pole = p;
                            Posibilieties copy = t.ShallowCopy();
                            copy.CoordinatedWith = posil;
                            lis.Add(copy);
                        }
                    }
                }
            }
            if (lis.Count > 0) {
                var ord = lis.OrderByDescending(item => item.Points);
                posible.Add(ord.First());
            }
        }
        return posible;
    }
    public void ClearTiles(bool All = false) {
        if (map == null) {
            return;
        }
        for (int y = 0; y < mapSizeY; y++) {
            for (int x = 0; x < mapSizeX; x++) {
                if (All || map[x, y] == null) {
                    tiles[x, y].Stat(0);
                } else {
                    tiles[x, y].Stat(1);
                }
            }
        }
    }


    public void ViewPath(int x, int y) {
        if (map == null || map[x, y] == null) {
            return;
        }
        if (selectedSkill != null) {
            return;
        }
        Queue<Pole> q = new Queue<Pole>(map[x, y].path);
        ClearTiles();
        while (q.Count > 0) {
            Pole p = q.Peek();
            tiles[p.X, p.Y].Stat(2);
            q.Dequeue();
        }
    }
    void GoTo(int x, int y) {
        if (map[x, y] == null) {
            return;
        }
        Queue<Pole> q = new Queue<Pole>(map[x, y].path);
        selectedUnit.MovingOnTiles(q);
        map = new Pole[mapSizeX, mapSizeY];
        ClearTiles();
    }
}