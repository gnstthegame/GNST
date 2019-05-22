using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasa odpowiadająca za wyświetlanie niektórych paneli ze sceny
/// </summary>
public class PanelOpener : MonoBehaviour
{
    public GameObject buyPanel;
    public GameObject sellPanel;
    public GameObject buyButton;
    public GameObject sellButton;

    /// <summary>
    /// Metoda wyświetla pierwszy panel, a zamyka drugi
    /// </summary>
    public void BuyPanel()
    {
        buyPanel.SetActive(true);
        sellButton.SetActive(true);
        sellPanel.SetActive(false);
        buyButton.SetActive(false);
    }

    /// <summary>
    /// Metoda wyświetla drugi panel, a zamyka pierwszy
    /// </summary>
    public void SellPanel()
    {
        sellPanel.SetActive(true);
        buyButton.SetActive(true);
        buyPanel.SetActive(false);
        sellButton.SetActive(false);
    }
}
