using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    [SerializeField] Inventory inventory;
    //Pair
    [SerializeField] public List<Pair> sellItems;
    [SerializeField] Transform playerTransform;
    [SerializeField] ShopManager shop;
    bool show = false;

    public float distance = 3f;

    private void Awake() {
        shop = FindObjectOfType<ShopManager>();
        inventory = FindObjectOfType<Inventory>();
        playerTransform = FindObjectOfType<CharacterMotor>().transform;
    }

    private void Update() {
        if (Input.GetButtonDown("Interact")) {
            if (Vector3.Distance(playerTransform.position, transform.position) < distance) {
                if (!show) {
                    show = true;
                    shop.Show();
                    return;
                }
            }
            if (show) {
                show = false;
                shop.Hide();
            }
        }
        if (show && Vector3.Distance(playerTransform.position, transform.position) > distance) {
            show = false;
            shop.Hide();
        }
    }
}
