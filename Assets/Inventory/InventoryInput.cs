using UnityEngine;

public class InventoryInput : MonoBehaviour {
    [SerializeField] GameObject inventoryGameObject;
    [SerializeField] KeyCode[] toggleInventoryKeys;


	// Update is called once per frame
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
