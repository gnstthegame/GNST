using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Playables;

public class CutsceneTrigger : Interactable {

    public PlayableDirector director;
    public Camera mainCamera;
    public float time;
    bool start = false;
    bool start2 = false;
    bool start3 = true;
    Queue<float> timestamps = new Queue<float>();

    Transform playerTransform;

    private void Awake()
    {
        playerTransform = FindObjectOfType<CharacterMotor>().transform;
        timestamps.Enqueue(8);
        timestamps.Enqueue(5);
        timestamps.Enqueue(4);
        timestamps.Enqueue(6);
        timestamps.Enqueue(6);
        timestamps.Enqueue(7);
        timestamps.Enqueue(3);
        timestamps.Enqueue(10);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (director != null)
        {
            isInRange = true;
            director.Play();
            mainCamera.enabled = false;
            if (start2 == false)
            {
                this.GetComponent<DialogueTrigger>().TriggerDialogie();
                time = 7;
                start = true;
                start2 = true;
            }
        }

    }

    public void skip() {
        director.time += (time - 0.1f);
        time = 0.1f;
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0.0f && start == true)
        {
            FindObjectOfType<DialogueMenager>().DisplayNextSentence();
            //counter = false;
            if (timestamps.Count != 0)
            {
                time = timestamps.Dequeue();
            }
            else
            {
                start = false;
                mainCamera.enabled = true;
                start3 = false;
                director = null;
                GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("CutsceneCharacter");

                foreach (GameObject go in gameObjectArray)
                {
                    go.SetActive(false);
                }
            }
        }
    }
}
