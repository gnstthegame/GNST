using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMenager : MonoBehaviour {
    bool pause = false, inv = false, BlockP = false, BlockI = false;
    PauseMenu PM;
    InventoryManager IM;

    private void OnValidate() {
        PM = GetComponentInChildren<PauseMenu>();
        IM = GetComponentInChildren<InventoryManager>();
    }


    void Update() {
        if (Input.GetButtonDown("PauseMenu") && !BlockP) {
            if (inv) {
                inv = false;
                IM.Hide();
            } else {
                if (pause) {
                    pause = false;
                    PM.Resume();
                } else {
                    pause = true;
                    PM.Pause();
                }
            }
        }
        if (Input.GetButtonDown("Inventory") && !pause && !BlockI) {
            if (inv) {
                inv = false;
                IM.Hide();
            } else {
                inv = true;
                IM.Show();
            }
        }
    }
    public void BlockInventory() {
        BlockI = true;
        inv = false;
        IM.Hide();
    }
    public void UnblockInventory() {
        BlockI = false;
    }
    public void BlockMenu() {
        BlockP = true;
        PM.Pause();
        pause = true;
    }
    public void UnblockMenu() {
        BlockP = false;
    }
}
