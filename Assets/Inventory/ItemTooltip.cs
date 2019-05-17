﻿using System.Collections;
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
        //ItemSlotText.text = item.equipmentType. .ToString();
        string typ;
        switch (item.equipmentType) {
            case EquipmentType.Helmet:
                typ = "Hełm";
                break;
            case EquipmentType.Chest:
                typ = "Pancerz";
                break;
            case EquipmentType.Melee:
                typ = "Broń Biała";
                break;
            case EquipmentType.Ranged:
                typ = "Broń Dwuręczna";
                break;
            case EquipmentType.Defence:
                typ = "Osłona";
                break;
            case EquipmentType.Usable1:
                typ = "Mikstura";
                break;
            case EquipmentType.Usable2:
                typ = "Przedmiot Rzucany";
                break;
            case EquipmentType.Sticker:
                typ = "Naklejka";
                break;
            default:
                typ = "Przedmiot Fabularny";
                break;
        };
        ItemSlotText.text = typ;

        sb.Length = 0;
        AddStat(item.Armor, "Armor");
        AddStat(item.Strength, "Strength");
        AddStat(item.Agility, "Agility");
        AddStat(item.Stamina, "Stamina");
        AddStat(item.Luck, "Luck");
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
