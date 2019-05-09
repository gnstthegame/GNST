using UnityEngine;

public class InventoryInput : MonoBehaviour {
<<<<<<< HEAD
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
=======

    [SerializeField] KeyCode[] toggleInventoryKeys;
    [SerializeField] GameObject inventoryGameObject;
    CanvasGroup canvasGroup;

    private void Start()
    {
        //inventoryGameObject.SetActive(false);
        canvasGroup = GetComponentInParent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update () {
		for(int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                //inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);
                if(canvasGroup.alpha == 0)
                {
                    canvasGroup.alpha = 1;
                    canvasGroup.blocksRaycasts = true;
                }
                else if(canvasGroup.alpha == 1)
                {
                    canvasGroup.alpha = 0;
                    canvasGroup.blocksRaycasts = false;
                }

>>>>>>> 923e860fc7ce348d585a8508193aea820623ec1d
                break;
            }
        }
	}
}
