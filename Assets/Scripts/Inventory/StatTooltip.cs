using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class StatTooltip : MonoBehaviour {
    [SerializeField] Text StatNameText;
    [SerializeField] Text StatModifiers;

    private StringBuilder sb = new StringBuilder();

    //POPRAWIC DODAWANIE STATYSTYK
    //Wyswietlanie statystyk
    public void ShowTooltip(CharacterStat stat, string statName)
    {
        StatNameText.text = GetStatText(stat, statName);
        StatModifiers.text = GetStatModifiersText(stat);
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    //To po dodaniu statystyk
    private string GetStatText(CharacterStat stat, string statName)
    {
        sb.Length = 0;
        sb.Append(statName);
        sb.Append(" ");
        sb.Append(stat.Value);

        if(stat.Value != stat.BaseValue)
        {
            sb.Append(" (");
            sb.Append(stat.BaseValue);

            if (stat.Value > stat.BaseValue)
                sb.Append("+");

            sb.Append(System.Math.Round(stat.Value - stat.BaseValue, 4));
            sb.Append(")");
        }
        

        return sb.ToString();
    }

    private string GetStatModifiersText(CharacterStat stat)
    {
        sb.Length = 0;

        foreach(StatModifier modifier in stat.StatModifiers)
        {
            if(sb.Length > 0)
                sb.AppendLine();
           
            if (modifier.Value > 0)
                sb.Append("+");

            if (modifier.Type == StatModType.Flat)
            {
                sb.Append(modifier.Value);
            }
            else
            {
                sb.Append(modifier.Value * 100);
                sb.Append("%");
            }

            EquippableItem item = modifier.Source as EquippableItem;
            if(item != null)
            {
                sb.Append(" ");
                sb.Append(item.Name);
            }
            else
            {
                Debug.Log("Blad StatTooltip");
            }

        }

        return sb.ToString();
    }
}
