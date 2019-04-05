using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    [SerializeField] BuyButton[] buyButtons;
    [SerializeField] SellButton[] sellButtons;
    [SerializeField] Transform buttonsParent;
    //stad sprobowac pobrac obrazki i je ustawic
    [SerializeField] Inventory inventory;
    //Zrobic ustawianie obrazkow tu chyba
    private void OnValidate()
    {
        buyButtons = buttonsParent.GetComponentsInChildren<BuyButton>();
        sellButtons = buttonsParent.GetComponentsInChildren<SellButton>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
