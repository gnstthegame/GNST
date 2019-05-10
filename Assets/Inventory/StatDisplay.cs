﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] Text nameText;
    [SerializeField] Text valueText;
    [SerializeField] StatTooltip tooltip;

    private CharacterStat _stat;
    public CharacterStat Stat {
        get { return _stat; }
        set {
            _stat = value;
            UpdateStatValue();
        }
    }

    public void UpdateStatValue()
    {
        valueText.text = _stat.Value.ToString();
    }

    private string _name;
    public string Name
    {
        get { return _name; }
        set {
            _name = value;
            nameText.text = _name.ToLower();
        }
    }


    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        valueText = texts[1];

        if(tooltip == null)
        {
            tooltip = FindObjectOfType<StatTooltip>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(Stat, Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }
}
