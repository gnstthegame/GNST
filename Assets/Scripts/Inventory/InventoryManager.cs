using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using UnityEngine.UI;

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
    [SerializeField] ItemTooltip itemTooltip;
    //ostatnia rzecz
    [SerializeField] Image draggableItem;

    private ItemSlot draggedSlot;
=======
using System;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] Image draggableItem;
    [SerializeField] Text money;

    private ItemSlot dragItemSlot;
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d

    private void OnValidate()
    {
        if(itemTooltip == null)
        {
            itemTooltip = FindObjectOfType<ItemTooltip>();
        }
    }

    private void Awake()
    {
<<<<<<< HEAD
        statPanel.SetStats(Level, Strength, Agility, Stamina, Luck, Armor, Crit, Hit);
        statPanel.UpdateStatValues();
        //statPanel.UpdateStatNames();

        inventory.OnRightClickEvent += InventoryRightClick;
        equipmentPanel.OnRightClickEvent += EquipmentPanelRightClick;
=======

        inventory.OnRightClickEvent += Equip;
        equipmentPanel.OnRightClickEvent += Unequip;
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d

        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;

        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;

        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;

<<<<<<< HEAD
        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;

        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;

=======
        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;

        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;

>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
    }

<<<<<<< HEAD
    private void InventoryRightClick(ItemSlot itemSlot)
    {
        if(itemSlot.Item is EquippableItem)
        {
            Equip((EquippableItem)itemSlot.Item);
        }
        else if(itemSlot.Item is UsableItem)
        {
            UsableItem usableItem = itemSlot.Item as UsableItem;
            usableItem.Use(this);

            if (usableItem.isConsumable)
            {
                inventory.RemoveItem(usableItem.ID);
            }
        }
    }

    private void EquipmentPanelRightClick(ItemSlot itemSlot)
    {
        if (itemSlot.Item is EquippableItem)
        {
            Unequip((EquippableItem)itemSlot.Item);
        }
    }

    private void ShowTooltip(ItemSlot itemSlot)
    {
        if (itemSlot.Item != null && itemSlot.Item is EquippableItem)
        {
            itemTooltip.ShowTooltip((EquippableItem)itemSlot.Item);
        }
        else if(itemSlot.Item != null && itemSlot.Item is UsableItem)
        {
            itemTooltip.ShowTooltip((UsableItem)itemSlot.Item);
=======
    private void Start()
    {
        this.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }

        money.text = "Plusz: " + inventory.money.ToString();
    }

    private void Equip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if(equippableItem != null)
        {
            Equip(equippableItem);
        }
    }

    private void Unequip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if (equippableItem != null)
        {
            Unequip(equippableItem);
        }
    }

    private void ShowTooltip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if (equippableItem != null)
        {
            itemTooltip.ShowTooltip(equippableItem);
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
        }
    }

    private void HideTooltip(ItemSlot itemSlot)
    {
        itemTooltip.HideTooltip();
    }

    private void BeginDrag(ItemSlot itemSlot)
    {
        if(itemSlot.Item != null)
        {
<<<<<<< HEAD
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Sprite;
=======
            dragItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

<<<<<<< HEAD
    private void EndDrag(ItemSlot itemSlot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }

=======
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
    private void Drag(ItemSlot itemSlot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
<<<<<<< HEAD
        
    }

    private void Drop(ItemSlot dropItemSlot)
    {
        if(draggedSlot.Item != null)
        {
            if (dropItemSlot.CanAddStack(draggedSlot.Item))
            {
                NewMethod(dropItemSlot);
            }
            else if (dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item))
            {
                SwapItems(dropItemSlot);
            }
        }      
    }

    private void SwapItems(ItemSlot dropItemSlot)
    {
        EquippableItem dragItem = draggedSlot.Item as EquippableItem;
        EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

        if (draggedSlot is EquipmentSlot)
        {
            if (dragItem != null) dragItem.Unequip(this);
            if (dropItem != null) dropItem.Equip(this);
        }

        if (dropItemSlot is EquipmentSlot)
        {
            if (dragItem != null) dragItem.Equip(this);
            if (dropItem != null) dropItem.Unequip(this);
        }
        statPanel.UpdateStatValues();

        Item draggedItem = draggedSlot.Item;
        int draggedItemAmount = draggedSlot.Amount;

        draggedSlot.Item = dropItemSlot.Item;
        draggedSlot.Amount = dropItemSlot.Amount;

        dropItemSlot.Item = draggedItem;
        dropItemSlot.Amount = draggedItemAmount;
    }

    private void NewMethod(ItemSlot dropItemSlot)
    {
        int numAddableStacks = dropItemSlot.Item.MaximumStacks;
        int stacksToAdd = Mathf.Min(numAddableStacks, draggedSlot.Amount);
        dropItemSlot.Amount += stacksToAdd;
        draggedSlot.Amount -= stacksToAdd;
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
        if (inventory.RemoveItem(item.ID))
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
=======
    }

    private void EndDrag(ItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            dragItemSlot = null;
           
        }
        draggableItem.enabled = false;
    }

    private void Drop(ItemSlot dropItemSlot)
    {
        if (dragItemSlot == null) return;

        if (dropItemSlot.CanReceiveItem(dragItemSlot.Item) && dragItemSlot.CanReceiveItem(dropItemSlot.Item))
        {

            EquippableItem dragItem = dragItemSlot.Item as EquippableItem;
            EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

            if(dragItemSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Unequip(this);
                if (dropItem != null) dropItem.Equip(this);
            }
            
            if(dropItemSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Equip(this);
                if (dropItem != null) dragItem.Unequip(this);
            }

            Item draggedItem = dragItemSlot.Item;
            dragItemSlot.Item = dropItemSlot.Item;
            dropItemSlot.Item = draggedItem;
        }       
    }


    public void Equip(EquippableItem item)
    {
        if (inventory.RemoveItem(item.ID))
        {
            EquippableItem previousItem;
            if(equipmentPanel.AddItem(item, out previousItem))
            {
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                }
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }

<<<<<<< HEAD
    public void Unequip(EquippableItem item)
    {
        //sprawdzenie czy jest miejsce w ekwipunku
        if(!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
=======

    public void Unequip(EquippableItem item)
    {
        if(!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
            inventory.AddItem(item);
        }
    }
}
