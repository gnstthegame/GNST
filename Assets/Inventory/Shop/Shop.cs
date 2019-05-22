using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasa umożliwiająca interakcję z handlarzem
/// </summary>
public class Shop : Interactable
{

    //Panel ekwipunku
    [SerializeField] Inventory inventory;
    //Lista przedmiotów na sprzedaż
    [SerializeField] public List<Pair> sellItems;
    //pozycja gracza na scenie
    [SerializeField] Transform playerTransform;
    //klasa zarządzająca sklepem
    [SerializeField] ShopManager shop;
    //czy panel powinien byc widoczny
    bool show = false;

    //maksymalna odległość, po jej przekroczeniu nie można wejść w interakcję
    public float distance = 3f;

    /// <summary>
    /// Metoda odnajdująca obiekty w hierarchii projektu
    /// </summary>
    private void Awake()
    {
        shop = FindObjectOfType<ShopManager>();
        inventory = FindObjectOfType<Inventory>();
        playerTransform = FindObjectOfType<CharacterMotor>().transform;
    }

    /// <summary>
    /// Interakcja z handlarzem, otwiera okno handlu
    /// </summary>
    public override void Interact()
    {
        base.Interact();
        if (Vector3.Distance(playerTransform.position, transform.position) < distance)
        {
            if (!show)
            {
                show = true;
                shop.Show();
                return;
            }
            if (show)
            {
                show = false;
                shop.Hide();
            }
        }
    }

    /// <summary>
    /// Metoda sprawdzająca, czy można wejść w interakcję
    /// </summary>
    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }
        if (show && Vector3.Distance(playerTransform.position, transform.position) > distance)
        {
            show = false;
            shop.Hide();
        }
    }
}
