using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardMenu : MonoBehaviour {
    public Text EXP,Plusz;
    InventoryManager IM;
    private void Awake() {
        IM = FindObjectOfType<InventoryManager>();
    }
    //quest menu
    /// <summary>
    /// wyświetl panel
    /// </summary>
    public void Show() {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    /// <summary>
    /// wyświetl panel z informacjami
    /// </summary>
    /// <param name="rew">nagroda</param>
    public void Show(List<Ai.Loot> rew) {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        int exp = 0,pl=0;
        foreach(Ai.Loot l in rew) {
            exp += l.EXP;
            pl += l.Plush;
            //quest rew.name
        }
        //inventory +exp +pl
        IM.inventory.money += pl;
        IM.Exp += exp;
        EXP.text = exp.ToString();
        Plusz.text = pl.ToString();
    }
    /// <summary>
    /// ukryj panel
    /// </summary>
    public void Hide() {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}
