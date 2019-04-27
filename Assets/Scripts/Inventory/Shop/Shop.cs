using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    [SerializeField] Inventory inventory;
    [SerializeField] public List<Item> sellItems;
    [SerializeField] Transform playerTransform;
    [SerializeField] ShopManager shop;


    bool isInRange;
    Ray ray;
    RaycastHit hit;

    private void OnValidate()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    // Use this for initialization
    void Start () {
        shop.canvasGroup.alpha = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (isInRange && Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
                //do pomyslenia
                if (hit.transform.tag == "Shop")
                {
                    Debug.Log("Test");
                    shop.canvasGroup.alpha = 1;
                    //canvasGroup.alpha = 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isInRange = true;
        Debug.Log("In range");
        
    }

    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
        shop.canvasGroup.alpha = 0; 
    }

   
}

