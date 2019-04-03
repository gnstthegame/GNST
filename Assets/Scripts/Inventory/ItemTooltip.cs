using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ItemTooltip : MonoBehaviour {
    [SerializeField] Text ItemNameText;
    [SerializeField] Text ItemSlotText;
    [SerializeField] Text ItemStatsText;

    private StringBuilder sb = new StringBuilder();


    //Wyswietlanie informacji o przedmiocie
    public void ShowTooltip(EquippableItem item)
    {
        ItemNameText.text = item.Name;
        ItemSlotText.text = item.equipmentType.ToString();

        sb.Length = 0;
        AddStat(item.Strength, "Strength");
        AddStat(item.Strength, "Agility");
        AddStat(item.Strength, "Luck");
        AddStat(item.Strength, "Armor");
        AddStat(item.Strength, "CritChance");
        AddStat(item.Strength, "HitRatio");

        ItemStatsText.text = sb.ToString();

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void AddStat(float value, string statName)
    {
        if(value != 0)
        {
            if(sb.Length > 0)
            {
                sb.AppendLine();
            }

            sb.Append(value);
            sb.Append(" ");
            sb.Append(statName);
        }        
    }
}
