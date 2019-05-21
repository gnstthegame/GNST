using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpriteChange : MonoBehaviour {

    public Sprite[] images;
    public Button image_set;
    int count = 0;

     void Awake()
    {
        images = new Sprite[7];
        images = Resources.LoadAll<Sprite>("Comics");
        image_set.image.sprite = images[count];
    }

    public void On_Click()
    {
        count++;
        if(count == images.Length)
        {
            LevelLoader load = gameObject.GetComponent<LevelLoader>();
            load.LoadLevel(1);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else {
            image_set.image.sprite = images[count];
        }

    }
}
