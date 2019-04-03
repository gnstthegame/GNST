using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Zarzadzanie calym ekwipunkiem
/// </summary>
/// Character
public class InventoryManager : MonoBehaviour {
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    public CharacterStat Level;
    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Stamina;
    public CharacterStat Luck;
    public CharacterStat Armor;
    public CharacterStat Crit;
    public CharacterStat Hit;
    [SerializeField] StatPanel statPanel;

    private void Awake()
    {
        //obsluga zdarzenie prawoklik
        statPanel.SetStats(Level, Strength, Agility, Stamina, Luck, Armor, Crit, Hit);
        statPanel.UpdateStatValues();
        statPanel.UpdateStatNames();
        inventory.OnItemRightClickEvent += EquipFromInventory;
        equipmentPanel.OnItemRightClickEvent += UnequipFromEquipPanel;
    }

    //potrzebne bo equip mozna wywolac tylko na equippableitem
    private void EquipFromInventory(Item item)
    {
        if(item is EquippableItem)
        {
            Equip((EquippableItem)item);
        }
    }

    private void UnequipFromEquipPanel(Item item)
    {
        if(item is EquippableItem)
        {
            Unequip((EquippableItem)item);
        }
    }

    public void Equip(EquippableItem item)
    {
        //Usuniecie z panelu ekwipunku
        if (inventory.RemoveItem(item))
        {
            //przedmiot ktory poprzednio byl zalozony
            EquippableItem previousItem;
            //dodanie do panelu ekwipunku
            if(equipmentPanel.AddItem(item, out previousItem))
            {
                //gdy jakis przedmiot byl w slocie dodac go do ekwipunku
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void Unequip(EquippableItem item)
    {
        //sprawdzenie czy jest miejsce w ekwipunku
        if(!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
            //update stat
        }
    }
}
