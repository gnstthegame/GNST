using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Collectable : MonoBehaviour {
    public Inventory inventory;
    public Item[] items;
    public Transform playerTransform;
    public float distance = 3f;
    private void Awake() {
        inventory = FindObjectOfType<Inventory>();
        playerTransform = FindObjectOfType<CharacterMotor>().transform;
    }

    private void Update() {
        if (Input.GetButtonDown("Interact") && Vector3.Distance(playerTransform.position, transform.position) < distance) {
            foreach (Item i in items) {
                inventory.AddItem(Instantiate(i));
            }
            Destroy(gameObject);
        }
    }

}
