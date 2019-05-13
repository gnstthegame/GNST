using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class change : MonoBehaviour {

    public Sprite[] images;
    Image image_set;
    int count = 0;

    void Awake() {
        image_set = GetComponent<Image>();

    }

    private void Update() {
        if (Input.anyKeyDown) {
            count++;
            if (count == images.Length) {
                image_set.sprite = null;
                gameObject.SetActive(true);
                LevelLoader load = gameObject.GetComponent<LevelLoader>();
                load.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
            } else {
                image_set.sprite = images[count];
            }
        }
    }
}
