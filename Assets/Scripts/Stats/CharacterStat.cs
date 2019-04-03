using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


[Serializable]
public class CharacterStat {
    public float BaseValue;
    //czy trzeba obliczac wartosci statystyk ponownie
    protected bool isDirty = true;
    //ostatnia wyliczona wartosc statystyki
    protected float _value;
    protected float lastBaseValue = float.MinValue;

    public float Value {
        get {
            if (isDirty || BaseValue != lastBaseValue) {
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }
    protected readonly List<StatModifier> statModifiers;

    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    public CharacterStat()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    public CharacterStat(float baseValue)
    {
        isDirty = true;
        BaseValue = baseValue;
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    public virtual void AddModifier(StatModifier modifier)
    {
        isDirty = true;
        statModifiers.Add(modifier);
        //aby modyfikatory byly dobrze nadawane
        statModifiers.Sort(CompareModifierOrder);
    }

    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0;// a.order == b.order
    }

    public bool RemoveModifier(StatModifier modifier)
    {
        if (statModifiers.Remove(modifier))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    public bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if(statModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0;

        for(int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];
            if(mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if(mod.Type == StatModType.PercentAdd)
            {
                sumPercentAdd += mod.Value;

                if(i + 1 >= statModifiers.Count || statModifiers[i].Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + mod.Value;
                    sumPercentAdd = 0;
                }
            }
            else if(mod.Type == StatModType.PercentMult)
            {
                finalValue *= 1 + mod.Value;
            }
            //Wrocic do tut character stat i zobaczyc gdzie to jest
            //finalValue += statModifiers[i].Value;
        }

        return (float)Math.Round(finalValue, 4);
    }
}
