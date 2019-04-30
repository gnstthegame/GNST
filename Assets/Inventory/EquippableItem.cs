using UnityEditor;
using UnityEngine;

public enum EquipmentType {
    Helmet,
    Chest,
    Melee,
    Ranged,
    Usable1,
    Usable2,
    Usable3
}


[CreateAssetMenu]
public class EquippableItem : Item {
    public int Strength;
    public int Agility;
    public int Stamina;
    public int Luck;
    public int Armor;
    public EquipmentType equipmentType;
    public bool Weapon;
    [System.Serializable]
    public class weapo {
        public Vector2[] Area;
        public Vector2 Dmg;
        public int Cost;
        public string AnimTrigger;
        public int AttackRange;
        public GameObject Model3d;

        public bool positive;
        Effect.EffectType Efekt, OnDestroy;
        int EfektPower;
        Effect.trig EffectTriggered;
        int CzasTrwania;
    }
    public weapo[] w;
    //public Skill(Vector2[] AtackArea, Vector2 Damage, int cost, string AnimationTrigger, Effect.trig trigg, int duration = 0, int value = 0, Effect.DelegationType Function = 0, Effect.DelegationType onDestroy = 0, bool Positive = false, int Range = 0) {


    //Tutaj beda modyfikatory statystyk
    public void Equip(InventoryManager c) {
        if (Strength != 0)
            c.Strength.AddModifier(new StatModifier(Strength, StatModType.Flat, this));
        if (Agility != 0)
            c.Agility.AddModifier(new StatModifier(Agility, StatModType.Flat, this));
        if (Stamina != 0)
            c.Stamina.AddModifier(new StatModifier(Stamina, StatModType.Flat, this));
        if (Luck != 0)
            c.Luck.AddModifier(new StatModifier(Luck, StatModType.Flat, this));
        if (Armor != 0)
            c.Armor.AddModifier(new StatModifier(Armor, StatModType.Flat, this));
    }
    //public override Skill getskill() {
    //    Skill skl = new Skill(Area, Dmg, Cost, AnimTrigger Sprite, Model3d,effec
    //    };
    //    return skl;
    //}

    public void Unequip(InventoryManager c) {
        c.Strength.RemoveAllModifiersFromSource(this);
        c.Agility.RemoveAllModifiersFromSource(this);
        c.Stamina.RemoveAllModifiersFromSource(this);
        c.Luck.RemoveAllModifiersFromSource(this);
        c.Armor.RemoveAllModifiersFromSource(this);
    }
}
