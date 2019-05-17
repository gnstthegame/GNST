using UnityEngine;

[System.Serializable]
//Typ wyliczeniowy definiujący typy przedmiotów możliwych do wyposażenia
public enum EquipmentType
{
    Helmet,
    Chest,
    Melee,
    Ranged,
    Defence,
    Usable1,
    Usable2,
    Sticker,
    Quest1,
    Quest2,
    Quest3,
    Quest4
}

/// <summary>
/// Klasa definiująca przedmiot możliwy do wyposażenia
/// </summary>
[CreateAssetMenu]
[System.Serializable]
public class EquippableItem : Item {
    //statystyki
    public int Strength;
    public int Agility;
    public int Stamina;
    public int Luck;
    public int Armor;
    public EquipmentType equipmentType;
    public string Opis;

    public Vector2[] Area;
    public Vector2 Dmg;
    public int Cost;
    public string AnimTrigger;
    public int AttackRange;
    public GameObject Model3d;

    public bool positive;
    public Effect.EffectType Efekt, OnDestroy;
    public int EfektPower;
    public Effect.trig EffectTriggered;
    public int CzasTrwania;
    //public Skill(Vector2[] AtackArea, Vector2 Damage, int cost, string AnimationTrigger, Effect.trig trigg, int duration = 0, int value = 0, Effect.DelegationType Function = 0, Effect.DelegationType onDestroy = 0, bool Positive = false, int Range = 0) {


    public override void Destroy()
    {
        Destroy(this);
    }
    /// <summary>
    /// Metoda modyfikująca statystyki po wyposażeniu przedmiotu
    /// </summary>
    /// <param name="c">Klasa przechowująca statystyki postaci</param>
    public void Equip(InventoryManager c)
    {
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

    public override Skill getskill() {
        Skill skl = new Skill(Area, Dmg, Cost, AnimTrigger, Sprite, Model3d, EffectTriggered, CzasTrwania, EfektPower, Efekt, OnDestroy, positive, AttackRange);
        return skl;
    }

    /// <summary>
    /// Metoda modyfikująca statystyki postaci po zdjęciu aktualnie wyposażonego przedmiotu
    /// </summary>
    /// <param name="c">Klasa przechowująca statystyki postaci</param>
    public void Unequip(InventoryManager c)
    {
        c.Strength.RemoveAllModifiersFromSource(this);
        c.Agility.RemoveAllModifiersFromSource(this);
        c.Stamina.RemoveAllModifiersFromSource(this);
        c.Luck.RemoveAllModifiersFromSource(this);
        c.Armor.RemoveAllModifiersFromSource(this);
    }
}
