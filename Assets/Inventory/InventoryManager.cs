using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Zarzadzanie calym ekwipunkiem
/// </summary>
/// Character
public class InventoryManager : MonoBehaviour {
    //panel z przedmiotami posiadanymi
    [SerializeField] public Inventory inventory;
    //panel ekwipunku
    [SerializeField] public EquipmentPanel equipmentPanel;

    //przyciski zwiększające statystyki
    public Button[] statButtons;
    public GameObject ex;

    /// <summary>
    /// <list type="bullet">
    /// <item>
    /// <description>Poziom postaci</description>
    /// </item>
    /// <item>
    /// <description>Siła postaci</description>
    /// </item>
    /// <item>
    /// <description>Zwinność postaci</description>
    /// </item>
    /// <item>
    /// <description>Wytrzymałość postaci</description>
    /// </item>
    /// <item>
    /// <description>Szczęście postaci</description>
    /// </item>
    /// <item>
    /// <description>Pancerz postaci</description>
    /// </item>
    /// </list>
    /// </summary>
    public CharacterStat Level;
    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Stamina;
    public CharacterStat Luck;
    public CharacterStat Armor;
    int exp = 0;

    public int statPoints = 0;

    public Sprite fist;

    //panel statystyk postaci
    [SerializeField] StatPanel statPanel;
    //panel z informacjami o przedmiocie
    [SerializeField] ItemTooltip itemTooltip;
    //ikonka przeciąganego przedmiotu(do drag and drop)
    [SerializeField] Image draggableItem;

    //slot z którego przemieszczamy przedmiot(drag and drop)
    private ItemSlot draggedSlot;

    public int Exp {
        get {
            return exp;
        }
        set {
            int e = value;
            while (5 * Mathf.Pow(2, Level.Value) < e) {
                e -= 5 * (int)Mathf.Pow(2, Level.Value);
                statPoints++;
                Level.BaseValue++;
            }
            CheckButtons();
            exp = e;
            ex.GetComponent<Text>().text = exp.ToString();
        }
    }


    /// <summary>
    /// Odszukanie obiektu panelu do informacji o przedmiotach
    /// Ustawienie statystyk
    /// Dodanie obsługi zdarzeń
    /// </summary>
    private void Awake() {
        if (itemTooltip == null) {
            itemTooltip = FindObjectOfType<ItemTooltip>();
        }
        statPanel.SetStats(Level, Strength, Agility, Stamina, Luck, Armor);
        ReloadStats();
        statPanel.UpdateStatNames();
        
        statPanel.UpdateStatValues();
    
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

    private void OnValidate()
    {
        statButtons = statPanel.GetComponentsInChildren<Button>();
    }

    private void Update()
    {
        CheckButtons();
    }

    /// <summary>
    /// Metoda sprawdzająca, czy przyciski zwiększające statystyki powinny być aktywne
    /// </summary>
    void CheckButtons()
    {
        if (statPoints <= 0)
        {
            foreach (Button b in statButtons)
            {
                b.GetComponent<Image>().enabled = false;
            }
        }
        else if(statPoints > 0)
        {
            foreach (Button b in statButtons)
            {
                b.GetComponent<Image>().enabled = true;
            }
        }
    }

    public void IncreaseStrength()
    {
        if(statPoints > 0)
        {
            IncreaseStat(Strength);
            statPoints--;
            ReloadStats();
            //Debug.Log("BV: " + Strength.BaseValue);
            //Debug.Log("V:" + Strength.Value);
        }
        
    }

    public void IncreaseAgility()
    {
        if(statPoints > 0)
        {
            IncreaseStat(Agility);
            statPoints--;
            ReloadStats();
        }     
    }

    public void IncreaseStamina()
    {
        if (statPoints > 0)
        {
            IncreaseStat(Stamina);
            statPoints--;
            ReloadStats();
        }       
    }

    public void IncreaseLuck()
    {
        if (statPoints > 0)
        {
            IncreaseStat(Luck);
            statPoints--;
            ReloadStats();
        }      
    }

    public void IncreaseStat(CharacterStat stat)
    {
        stat.BaseValue++;
        //stat.AddModifier(new StatModifier(1f, StatModType.Flat));
    }

    /// <summary>
    /// Metoda wykonywana po wciśnięciu w jeden ze slotów panelu posiadanych przedmiotów
    /// Sprawdza typ przedmiotu w slocie, a następnie na jego podstawie wykonuje odpowiednią metodę 
    /// zależną od typu przemiotu
    /// </summary>
    /// <param name="itemSlot">Wybrany slot</param>
    private void InventoryRightClick(ItemSlot itemSlot) {
        if (itemSlot.Item is EquippableItem) {
            Equip((EquippableItem)itemSlot.Item);
        } else if (itemSlot.Item is UsableItem) {
            UsableItem usableItem = itemSlot.Item as UsableItem;
            usableItem.Use(this);

            if (usableItem.isConsumable) {
                inventory.RemoveItem(usableItem.ID);
                usableItem.Destroy();
            }
        }
    }

    /// <summary>
    /// Metoda wywołana po wciśnięciu na jeden ze slotów panelu ekwipunku, odpowiada za
    /// zdjęcie aktualnie wyposażonego przemiotu
    /// </summary>
    /// <param name="itemSlot">Wybrany slot</param>
    private void EquipmentPanelRightClick(ItemSlot itemSlot) {
        if (itemSlot.Item is EquippableItem) {
            Unequip((EquippableItem)itemSlot.Item);
        }
    }

    /// <summary>
    /// Metoda wyświetlająca panel z informacjami o przedmiocie(po najechaniu myszką na odpowiedni slot)
    /// </summary>
    /// <param name="itemSlot">Wybrany slot</param>
    private void ShowTooltip(ItemSlot itemSlot) {
        if (itemSlot.Item != null && itemSlot.Item is EquippableItem) {
            itemTooltip.ShowTooltip((EquippableItem)itemSlot.Item);
        } else if (itemSlot.Item != null && itemSlot.Item is UsableItem) {
            itemTooltip.ShowTooltip((UsableItem)itemSlot.Item);
        }
    }

    /// <summary>
    /// Metoda chowająca panel z informacjami o przedmiocie
    /// </summary>
    private void HideTooltip(ItemSlot itemSlot) {
        itemTooltip.HideTooltip();
    }


    /// <summary>
    /// Metoda obsługująca początek procesu przeciągania.
    /// </summary>
    /// <param name="itemSlot">Slot, z którego przeciąganie się rozpoczęło.</param>
    private void BeginDrag(ItemSlot itemSlot) {
        if (itemSlot.Item != null) {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Sprite;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    /// <summary>
    /// Metoda wywoływana po zakończeniu procesu przeciągania
    /// </summary>
    private void EndDrag(ItemSlot itemSlot) {
        draggedSlot = null;
        draggableItem.enabled = false;
    }

    /// <summary>
    /// Metoda obsługująca proces przeciągania
    /// </summary>
    private void Drag(ItemSlot itemSlot) {
        if (draggableItem.enabled) {
            draggableItem.transform.position = Input.mousePosition;
        }

    }

    /// <summary>
    /// Metoda wywoływana po upuszczeniu obiektu na slocie
    /// Sprawdza, czy przedmioty należy zamienić, czy zwiększyc ich ilość(jeżeli są takie same)
    /// </summary>
    /// <param name="dropItemSlot">slot na którym upuszczono obiekt</param>
    private void Drop(ItemSlot dropItemSlot) {
        if (draggedSlot.Item != null) {
            if (dropItemSlot.CanAddStack(draggedSlot.Item)) {
                NewMethod(dropItemSlot);
            } else if (dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item)) {
                SwapItems(dropItemSlot);
            }
        }
    }
    public List<Skill> GetSkills() {
        List<EquipmentSlot> eq = new List<EquipmentSlot>(equipmentPanel.equipmentSlots);
        eq = eq.FindAll(item => item.Item != null && (item.equipmentType == EquipmentType.Melee || item.equipmentType == EquipmentType.Ranged || item.equipmentType == EquipmentType.Defence));

        List<Skill> skils = new List<Skill>();
        foreach (EquipmentSlot i in eq) {
            Skill s = i.Item.getskill();
            if (!s.positive) {
                s.Dmg += new Vector2(Agility.Value, Strength.Value);
                if (s.Dmg.x > s.Dmg.y) {
                    s.Dmg.y = s.Dmg.x;
                }
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
        eq = eq.FindAll(item => item.Item != null && (item.equipmentType == EquipmentType.Usable1 || item.equipmentType == EquipmentType.Usable2));

        List<Item> skils = new List<Item>();
        foreach (EquipmentSlot i in eq) {
            skils.Add(i.Item);
        }
        return skils;
    }

    /// <summary>
    /// Metoda zamieniająca przedmioty na dwóch slotach(przy drag and drop)
    /// </summary>
    /// <param name="dropItemSlot">Slot na którym zakończono przeciąganie</param>
    private void SwapItems(ItemSlot dropItemSlot) {
        EquippableItem dragItem = draggedSlot.Item as EquippableItem;
        EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

        if (draggedSlot is EquipmentSlot) {
            if (dragItem != null) dragItem.Unequip(this);
            if (dropItem != null) dropItem.Equip(this);
        }

        if (dropItemSlot is EquipmentSlot) {
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

    /// <summary>
    /// Metoda wyliczająca ilość przedmiotów w slocie po drag and drop(ile z nich można umieścić w slocie)
    /// </summary>
    /// <param name="dropItemSlot">Slot na którym upuszczono przedmiot</param>
    private void NewMethod(ItemSlot dropItemSlot) {
        int numAddableStacks = dropItemSlot.Item.MaximumStacks;
        int stacksToAdd = Mathf.Min(numAddableStacks, draggedSlot.Amount);
        dropItemSlot.Amount += stacksToAdd;
        draggedSlot.Amount -= stacksToAdd;
    }

    /// <summary>
    /// Metoda pokazująca panel ekwipunku
    /// </summary>
    public void Show() {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    /// <summary>
    /// Metoda ukrywająca panel ekwipunku
    /// </summary>
    public void Hide() {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //potrzebne bo equip mozna wywolac tylko na equippableitem
    private void EquipFromInventory(Item item) {
        if (item is EquippableItem) {
            Equip((EquippableItem)item);
        }
    }

    private void UnequipFromEquipPanel(Item item) {
        if (item is EquippableItem) {
            Unequip((EquippableItem)item);
        }
    }

    /// <summary>
    /// Metoda wyposażająca przedmiot
    /// </summary>
    /// <param name="item">Przedmiot do wyposażenia</param>
    public void Equip(EquippableItem item) {
        //Usuniecie z panelu ekwipunku
        if (inventory!=null && item!=null && inventory.RemoveItem(item.ID)!=null) {
            //przedmiot ktory poprzednio byl zalozony
            EquippableItem previousItem;
            //dodanie do panelu ekwipunku
            if (equipmentPanel.AddItem(item, out previousItem)) {
                //gdy jakis przedmiot byl w slocie dodac go do ekwipunku
                if (previousItem != null) {
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            } else {
                inventory.AddItem(item);
            }
        }
    }

    /// <summary>
    /// Metoda sprawdza, czy możliwe jest zdjęcie przedmiotu, oraz odświeża wartości statystyk
    /// </summary>
    /// <param name="item">Założony przedmiot</param>
    public void Unequip(EquippableItem item) {
        //sprawdzenie czy jest miejsce w ekwipunku
        if (!inventory.IsFull() && equipmentPanel.RemoveItem(item)) {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }
    /// <summary>
    /// Metoda ustawiająca wartości statystyk w panelu z nimi
    /// </summary>
    public void ReloadStats() {
        statPanel.UpdateStatValues();
    }
}
