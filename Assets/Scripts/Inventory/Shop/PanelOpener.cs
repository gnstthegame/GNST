using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour {
    public GameObject buyPanel;
    public GameObject sellPanel;

    public void BuyPanel()
    {
        sellPanel.SetActive(false);
        buyPanel.SetActive(true);
    }

    public void SellPanel()
    {
        buyPanel.SetActive(false);
        sellPanel.SetActive(true);
    }
}
