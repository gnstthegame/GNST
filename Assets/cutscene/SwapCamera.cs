using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCamera : MonoBehaviour {

    public Camera mainCamera;
    public Camera cutsceneCamera;

    public void ShowCutsceneView()
    {
        mainCamera.enabled = false;
        cutsceneCamera.enabled = true;
    }

    public void ShowMainView()
    {
        mainCamera.enabled = true;
        cutsceneCamera.enabled = false;
    }
}
