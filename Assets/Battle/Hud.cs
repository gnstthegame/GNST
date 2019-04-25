using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour {
    public Slider HPBar;
    public Text HPText;
    public Transform Panel;
    Unit u;
    bool Player = false;
    public GameObject ButtonsMenu;
    public GameObject[] Buttons;
    bool act = false;

    public void Activ(Unit uu, bool pla) {
        act = true;
        u = uu;
        Player = pla;
        HPBar.value = u.HP;
        Panel.gameObject.SetActive(true);
        Upd();
        if (Player) {
            for (int i = 0; i < Buttons.Length; i++) {
                if (i < u.Skile.Count) {
                    Buttons[i].SetActive(true);
                    Buttons[i].GetComponent<Image>().sprite = u.Skile[i].Icon;
                } else {
                    Buttons[i].SetActive(false);
                }
            }
            ButtonsMenu.SetActive(false);
        }
    }
    public void Deactiv() {
        act = false;
        Panel.gameObject.SetActive(false);
        ButtonsMenu.SetActive(false);
    }
    public void Select() {
        ButtonsMenu.SetActive(true);
    }
    public void Unselect() {
        ButtonsMenu.SetActive(false);
    }

    public void Upd() {
        HPBar.maxValue = u.MaxHP;
        HPText.text = u.HP.ToString();
        StopCoroutine(HPChange(0));
        StartCoroutine(HPChange(u.HP));
    }

    void Update() {
        if(act)
        Panel.transform.position = Camera.main.WorldToScreenPoint(u.transform.position);
    }
    IEnumerator HPChange(int hp) {
        HPText.text = hp.ToString();
        float old = HPBar.value;
        float delta = 0;
        while (delta < 1f) {
            HPBar.value = Mathf.Lerp(old, (float)hp, delta);
            delta += Time.deltaTime * 1;
            yield return null;
        }
    }
}
