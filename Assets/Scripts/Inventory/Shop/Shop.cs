using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    [SerializeField] GameObject inventory;
    [SerializeField] List<Item> items;
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject shop;


    bool isInRange;
    Ray ray;
    RaycastHit hit;
	// Use this for initialization
	void Start () {
        shop.SetActive(false);
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
                    //canvasGroup.alpha = 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //isInRange = true;
        Debug.Log("In range");
        shop.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
        shop.SetActive(false);
    }

   
}

