using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSoundTrigger : MonoBehaviour {
    public AudioManager am;
    bool played = false;
    public int whichToPlay;

	// Use this for initialization
	void Start () {
        //am.PlayMusic("1");
        //StartCoroutine(PlayAllTutorial());
        am = FindObjectOfType<AudioManager>();
        //StartCoroutine(FightWon());

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER");
        if(other.tag == "Player" && !played)
        {
            if (whichToPlay == 0)
            {

                am.PlayMusic("Battle");
                StartCoroutine(PlayAllTutorial());
                played = true;
            }                
        }      
    }

    private IEnumerator PlayAllTutorial()
    {
        
        for (int i = 0; i <= 13; i++)
        {
            yield return new WaitForSeconds(am.PlayMusicLengthBase(i.ToString()));// + 0.25f);
        }
    }

    public IEnumerator FightWon()
    {
        for(int i = 14; i < 17; i++)
        {
            yield return new WaitForSeconds(am.PlayMusicLengthBase(i.ToString()));
        }
    }
}
