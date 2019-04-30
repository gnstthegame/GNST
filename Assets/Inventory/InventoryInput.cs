using UnityEngine;

public class InventoryInput : MonoBehaviour {
    [SerializeField] GameObject inventoryGameObject;


    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Inventory")){
            inventoryGameObject.GetComponent<CanvasGroup>().alpha = (inventoryGameObject.GetComponent<CanvasGroup>().alpha == 0) ? 1 : 0;
            inventoryGameObject.GetComponent<CanvasGroup>().blocksRaycasts = (inventoryGameObject.GetComponent<CanvasGroup>().alpha == 0) ? false : true;
        }
    }
}
