using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chest : MonoBehaviour {
    [SerializeField] GameObject chestObject;
    [SerializeField] LootPanel lootPanel;
    [SerializeField] Transform playerTransform;
    public int plusz;

    [SerializeField] public List<Pair> lootItems;
    bool isInRange = false;
    bool isShown = false;

    public float distance = 3f;

    // Use this for initialization
    void Start () {
		
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
