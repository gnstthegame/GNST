using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour {
    public GameObject buyPanel;
    public GameObject sellPanel;
    public GameObject shopPanel;
    public Shop shop;
    public Camera main;

    private void Start()
    {
        BuyPanel();
    }



    public void BuyPanel()
    {
        sellPanel.GetComponent<CanvasGroup>().alpha = 0;
        sellPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        buyPanel.GetComponent<CanvasGroup>().alpha = 1;
        buyPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void SellPanel()
    {
        sellPanel.GetComponent<CanvasGroup>().alpha = 1;
        sellPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        buyPanel.GetComponent<CanvasGroup>().alpha = 0;
        buyPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void Close()
    {
        shopPanel.SetActive(false);
        shop.Restart();
    }
}
