using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    [SerializeField] GameObject inventory;
    [SerializeField] List<Item> items;
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject shop;
    [SerializeField] Camera main;
    [SerializeField] Vector3 tradesmanPosition = new Vector3(-12.9f, 6.11f, -31.6f);
    [SerializeField]Vector3 rotation = new Vector3(25.6f, 30.9f, 0f);

    public Vector3 originalPosition;
    public Quaternion originalRotation;

    bool isInRange;
    Ray ray;
    RaycastHit hit;
	// Use this for initialization
	void Start () {
        //main = Camera.main;
        originalPosition = main.transform.position;
        originalRotation = main.transform.rotation;
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
                if (hit.transform.name == "jack_model")
                {
                    Debug.Log("Test");
                    main.transform.position = tradesmanPosition;
                    main.transform.rotation = Quaternion.Euler(rotation);
                    shop.SetActive(true);
                    //canvasGroup.alpha = 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isInRange = true;
        Debug.Log("In range");  
        //shop.SetActive(true);

    }

    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
        shop.SetActive(false);
        Restart();
    }

    public void Restart()
    {
        main.transform.position = originalPosition;
        main.transform.rotation = originalRotation;
    }
   
}

