using UnityEngine;

/// <summary>
/// Klasa obsługująca skróy klawiszowe dla panelu ekwipunku
/// </summary>
public class InventoryInput : MonoBehaviour {
    //panel ekwipunku
    [SerializeField] GameObject inventoryGameObject;
    //tablica skrótów otwierających/zamykających panel ekwipunku
    [SerializeField] KeyCode[] toggleInventoryKeys;


	/// <summary>
    /// Metoda sprawdzająca, czy wciśnięto jeden z przycisków tablicy.
    /// Jeżeli tak, to otwiera/zamyka panel.
    /// </summary>
	void Update () {
        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                inventoryGameObject.GetComponent<CanvasGroup>().alpha = (inventoryGameObject.GetComponent<CanvasGroup>().alpha == 0) ? 1 : 0;
                inventoryGameObject.GetComponent<CanvasGroup>().blocksRaycasts = (inventoryGameObject.GetComponent<CanvasGroup>().alpha == 0) ? false : true;
                break;
            }
        }
	}
}
