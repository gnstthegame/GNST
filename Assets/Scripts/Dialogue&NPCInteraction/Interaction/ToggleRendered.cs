using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRendered : MonoBehaviour {

	public void ToggleVisibility(bool x)
    {

        //Renderer rend = this.GetComponent<Renderer>();
        if (x == true) {
            GetComponent<CanvasGroup>().alpha = 1;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else {
            GetComponent<CanvasGroup>().alpha = 0;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
            
    }
}
