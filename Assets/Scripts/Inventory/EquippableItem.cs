using UnityEngine;

public enum EquipmentType
{
    Helmet,
    Chest,
    Meele,
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
    public int Crit;
    public int Hit;

    public Vector2 Dmg;
    public Vector2[] Area;
    public int Cost;
    public int AttackRange = 0;
    public string AnimTrigger = "Dash";
    public bool positive = false;
    public EquipmentType equipmentType;
    

    public override Skill getskill() {
        Skill skl= new Skill(Area, Dmg, Cost, AnimTrigger);
        skl.Icon = Sprite;
        return skl;
    }


    //Tutaj beda modyfikatory statystyk
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
        if (Crit != 0)
            c.Crit.AddModifier(new StatModifier(Crit, StatModType.Flat, this));
        if (Hit != 0)
            c.Hit.AddModifier(new StatModifier(Hit, StatModType.Flat, this));
    }

    public void Unequip(InventoryManager c)
    {
        c.Strength.RemoveAllModifiersFromSource(this);
        c.Agility.RemoveAllModifiersFromSource(this);
        c.Stamina.RemoveAllModifiersFromSource(this);
        c.Luck.RemoveAllModifiersFromSource(this);
        c.Armor.RemoveAllModifiersFromSource(this);
        c.Crit.RemoveAllModifiersFromSource(this);
        c.Hit.RemoveAllModifiersFromSource(this);
    }
}
