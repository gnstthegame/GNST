using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour {
    public GameObject buyPanel;
    public GameObject sellPanel;
    public GameObject buyButton;
    public GameObject sellButton;

    public void BuyPanel()
    {
        buyPanel.SetActive(true);
        sellButton.SetActive(true);
        sellPanel.SetActive(false);
        buyButton.SetActive(false);
    }

    public void SellPanel()
    {
        Debug.Log("sell");
        sellPanel.SetActive(true);
        buyButton.SetActive(true);
        buyPanel.SetActive(false);
        sellButton.SetActive(false);
    }
}
