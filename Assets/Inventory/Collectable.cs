using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Collectable : MonoBehaviour {
    public Inventory inventory;
    public Item item;
    public Transform playerTransform;
    public float distance = 1f;
    private bool isInRange;
    private bool isMouseOver;
    Ray ray;
    RaycastHit hit;


    private void Update()
    {
        if (isInRange && Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
                //do pomyslenia
                if(hit.transform.name == "Cube")
                {
                    Destroy(gameObject);
                    inventory.AddItem(Instantiate(item));
                }
            }         
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Elo");
        isInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
    }

}
