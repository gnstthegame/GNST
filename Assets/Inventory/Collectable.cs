using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Klasa przedmiotu podnoszonego z ziemii
/// </summary>
public class Collectable : MonoBehaviour {
    //ekwipunek, do którego przedmiot będzie dodany
    public Inventory inventory;
    //przedmioty, które zostaną otrzymane po interakcji z obiektem
    public Item[] items;
    //aktualna pozycja gracza w świecie gry
    public Transform playerTransform;
    //limit odległości od obiektu, po przekroczeniu którego niemożliwa będzie interakcja
    public float distance = 3f;

    /// <summary>
    /// Odnalezienie ekwipunku, oraz obiektu gracza w hierarchii obiektu 
    /// </summary>
    private void Awake() {
        inventory = FindObjectOfType<Inventory>();
        playerTransform = FindObjectOfType<CharacterMotor>().transform;
    }

    /// <summary>
    /// Sprawdzenie odległości między graczem, a przedmiotem, oraz czy gracz podjął interakcję, jeśli tak, dodaje przedmiot do ekwipunku
    /// </summary>
    private void Update() {
        if (Input.GetButtonDown("Interact") && Vector3.Distance(playerTransform.position, transform.position) < distance) {
            foreach (Item i in items) {
                if(!inventory.IsFull())
                    inventory.AddItem(Instantiate(i));
            }
            Destroy(gameObject);
        }
    }

}
