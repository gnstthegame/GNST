using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardMenu : MonoBehaviour {
    public Text EXP,Plusz;
    //quest menu
	
	// Update is called once per frame
	void Update () {

    }
    public void Show() {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
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
        EXP.text = exp.ToString();
        Plusz.text = pl.ToString();
    }
    public void Hide() {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}
