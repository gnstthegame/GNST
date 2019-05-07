using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public Sprite fist;

    [SerializeField] StatPanel statPanel;
    [SerializeField] ItemTooltip itemTooltip;
    //ostatnia rzecz
    [SerializeField] Image draggableItem;

    private ItemSlot draggedSlot;

    private void OnValidate()
    {
        if(itemTooltip == null)
        {
            itemTooltip = FindObjectOfType<ItemTooltip>();
        }
    }

    private void Awake()
    {
        statPanel.SetStats(Level, Strength, Agility, Stamina, Luck, Armor);
        statPanel.UpdateStatValues();
        //statPanel.UpdateStatNames();

        inventory.OnRightClickEvent += InventoryRightClick;
        equipmentPanel.OnRightClickEvent += EquipmentPanelRightClick;

        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;

        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;

        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;

        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;

        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;

        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
    }

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
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Sprite;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void EndDrag(ItemSlot itemSlot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(ItemSlot itemSlot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }

    }
    public List<Skill> GetSkills() {
        List<EquipmentSlot> eq = new List<EquipmentSlot>(equipmentPanel.equipmentSlots);
        eq = eq.FindAll(item => item.Item != null && (item.equipmentType == EquipmentType.Melee || item.equipmentType == EquipmentType.Ranged));

        List<Skill> skils = new List<Skill>();
        foreach (EquipmentSlot i in eq) {
            Skill s = i.Item.getskill();
            s.Dmg += new Vector2(Agility.Value, Strength.Value);
            if (s.Dmg.x > s.Dmg.y) {
                s.Dmg.y = s.Dmg.x;
            }
            skils.Add(s);
        }
        if (skils.Count < 3) {
            Skill sk = new Skill(new Vector2(1, 1)) {
                Icon = fist
            };
            skils.Add(sk);
        }
        return skils;
    }
    public List<Item> GetItems() {
        List<EquipmentSlot> eq = new List<EquipmentSlot>(equipmentPanel.equipmentSlots);
        eq = eq.FindAll(item => item.Item != null && (item.equipmentType == EquipmentType.Usable1 || item.equipmentType == EquipmentType.Usable2 || item.equipmentType == EquipmentType.Usable3));

        List<Item> skils = new List<Item>();
        foreach (EquipmentSlot i in eq) {
            skils.Add(i.Item);
        }
        return skils;
    }

    private void Drop(ItemSlot dropItemSlot)
    {
        if(draggedSlot.Item != null)
        {
            if (dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item))
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
                draggedSlot.Item = dropItemSlot.Item;
                dropItemSlot.Item = draggedItem;
            }
        }
    }
    void Update() {
        //if (Input.GetButtonDown("Inventory")){
        //    GetComponent<CanvasGroup>().alpha = 1 - GetComponent<CanvasGroup>().alpha;
        //    GetComponent<CanvasGroup>().blocksRaycasts = !GetComponent<CanvasGroup>().blocksRaycasts;
        //}
    }
    public void Show() {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    public void Hide() {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
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
        }
    }
}
