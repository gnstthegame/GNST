using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour {

    public Button closeButton;
    public PanelOpener panelOpener;
    public Camera main; 

	// Use this for initialization
	void Start () {
        closeButton = GetComponent<Button>();
        closeButton.onClick.AddListener(panelOpener.Close);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
