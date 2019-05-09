using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ItemTooltip : MonoBehaviour {
<<<<<<< HEAD
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
        AddStat(item.Armor, "Armor");
        AddStat(item.Strength, "Strength");
        AddStat(item.Agility, "Agility");
        AddStat(item.Stamina, "Stamina");
        AddStat(item.Luck, "Luck");
        AddStat(item.Crit, "CritChance");
        AddStat(item.Hit, "HitRatio");
        AddStat(item.BuyValue, "BuyValue");
        AddStat(item.SellValue, "SellValue");

        ItemStatsText.text = sb.ToString();

        gameObject.SetActive(true);
    }

    public void ShowTooltip(UsableItem item)
    {
        ItemNameText.text = item.Name;
        ItemSlotText.text = "Usable";
        sb.Length = 0;

        sb.Insert(0, item.description);
        sb.Insert(sb.Length,"\n" + item.BuyValue + " BuyValue\n");
        sb.Insert(sb.Length, item.SellValue + " SellValue\n");
        ItemStatsText.text = sb.ToString();
        gameObject.SetActive(true);
    }

=======
    [SerializeField]
    private Text ItemNameText;
    [SerializeField]
    private Text ItemSlotText;
    [SerializeField]
    private Text ItemStatsText;

    private StringBuilder sb = new StringBuilder();

    public void ShowTooltip(EquippableItem item)
    {
        ItemNameText.text = item.ItemName;
        ItemSlotText.text = item.EquipmentType.ToString();

        sb.Length = 0;
        AddStat(item.StrengthBonus, "Strength");
        AddStat(item.AgilityBonus, "Agility");
        AddStat(item.IntelligenceBonus, "Intelligence");
        AddStat(item.VitalityBonus, "Vitality");

        AddStat(item.StrengthPercentBonus, "Strength", isPercent: true);
        AddStat(item.AgilityPercentBonus, "Agility", isPercent: true);
        AddStat(item.IntelligencePercentBonus, "Intelligence", isPercent: true);
        AddStat(item.VitalityPercentBonus, "Vitality", isPercent: true);

        ItemStatsText.text = sb.ToString();

        gameObject.SetActive(true);
    }
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

<<<<<<< HEAD
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
=======
    private void AddStat(float value, string statName, bool isPercent = false)
    {
        if(value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
                sb.Append("+");

            if (isPercent)
            {
                sb.Append(value * 100);
                sb.Append("% ");
            }
            else
            {
                sb.Append(value);
                sb.Append(" ");
            }
            
            sb.Append(statName);
        }
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
    }
}
