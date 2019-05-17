using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chest : MonoBehaviour {
    LootPanel lootPanel;
    Transform playerTransform;
    public int plusz;

    [SerializeField] public List<Pair> lootItems;
    bool isInRange = false;
    bool isShown = false;
    public bool DestroyEmpty = true;

    public float distance = 4f;

    private void Awake() {
        lootPanel = FindObjectOfType<LootPanel>();
        playerTransform = FindObjectOfType<CharacterMotor>().transform;
    }
    public void Destr() {
        if (DestroyEmpty) {
            lootPanel.Hide();
            lootPanel.chest = null;
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Interact"))
        {
            if (Vector3.Distance(playerTransform.position, transform.position) < distance)
            {
                lootPanel.chest = this;
                if (!isShown)
                {
                    isShown = true;
                    lootPanel.Show();
                    return;
                }
            }
            if (isShown)
            {
                isShown = false;
                lootPanel.Hide();
                lootPanel.chest = null;
            }
        }
        if (isShown && Vector3.Distance(playerTransform.position, transform.position) > distance)
        {
            isShown = false;
            lootPanel.Hide();
            lootPanel.chest = null;
        }
    }
}
