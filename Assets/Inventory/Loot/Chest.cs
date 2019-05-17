using System.Collections;
using System.Collections.Generic;
using UnityEngine;


<<<<<<< Updated upstream
public class Chest : MonoBehaviour {
    LootPanel lootPanel;
    Transform playerTransform;
    public int plusz;

    [SerializeField] public List<Pair> lootItems;
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

    public override void Interact()
    {
        base.Interact();
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

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }
        if (isShown && Vector3.Distance(playerTransform.position, transform.position) > distance)
        {
            isShown = false;
            lootPanel.Hide();
            lootPanel.chest = null;
        }
    }
}
