using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour {
    public Slider HPBar;
    public Text HPText,ArmorText,APtext;
    public Transform Panel;
    Unit u;
    bool Player = false;
    public GameObject ButtonsMenu,Armor;
    public GameObject[] Buttons;
    bool act = false;

    /// <summary>
    /// aktywuje HUD(Head-Up Display)
    /// </summary>
    /// <param name="uu">jednostka</param>
    /// <param name="pla">gracz</param>
    public void Activ(Unit uu, bool pla) {
        act = true;
        u = uu;
        Player = pla;
        HPBar.value = u.HP;
        Panel.gameObject.SetActive(true);
        Upd();
    }
    /// <summary>
    /// deaktywuje HUD
    /// </summary>
    public void Deactiv() {
        act = false;
        Panel.gameObject.SetActive(false);
        if (Player) {
            ButtonsMenu.SetActive(false);
        }
    }
    /// <summary>
    /// aktywuje przyciski umiejętności
    /// </summary>
    public void Select() {
        ButtonsMenu.SetActive(true);
    }
    /// <summary>
    /// deaktywuje przyciski umiejętności
    /// </summary>
    public void Unselect() {
        ButtonsMenu.SetActive(false);
    }
    /// <summary>
    /// auktualnij wyświetlane informacje
    /// </summary>
    public void Upd() {
        HPBar.maxValue = u.MaxHP;
        HPText.text = u.HP.ToString();
        int ap = u.AP;
        APtext.text = ap.ToString();
        StopCoroutine(HPChange(0));
        StartCoroutine(HPChange(u.HP));
        int arm = u.Armor;
        if (arm == 0) {
            Armor.SetActive(false);
        } else {
            Armor.SetActive(true);
            ArmorText.text = arm.ToString();
        }
        if (Player) {
            for (int i = 0; i < 3; i++) {
                if (i < u.Skile.Count) {
                    Buttons[i].SetActive(true);
                    Buttons[i].GetComponent<Image>().sprite = u.Skile[i].Icon;
                    if (ap < u.Skile[i].Cost || !u.CanAct) {
                        Buttons[i].GetComponent<Image>().color = Color.gray;
                    } else {
                        Buttons[i].GetComponent<Image>().color = Color.white;
                    }
                } else {
                    Buttons[i].SetActive(false);
                }
            }
            //ButtonsMenu.SetActive(false);

            for (int i = 3; i < 6; i++) {
                if (i < u.Items.Count + 3) {
                    Buttons[i].SetActive(true);
                    Buttons[i].GetComponent<Image>().sprite = u.Items[i - 3].Sprite;
                } else {
                    Buttons[i].SetActive(false);
                }
            }
        }
    }
    /// <summary>
    /// uaktualnij pozycje na ekranie
    /// </summary>
    void Update() {
        if(act)
        Panel.transform.position = Camera.main.WorldToScreenPoint(u.transform.position);
    }
    /// <summary>
    /// rutyna zmieniająca pasek życia
    /// </summary>
    /// <param name="hp">życie</param>
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
