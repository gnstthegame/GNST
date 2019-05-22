using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasa odpowiadająca za włączanie dialogów w odpowiednim momencie
/// </summary>
public class DialogueSoundTrigger : MonoBehaviour {
    //obiekt odtwarzający dźwięk
    public AudioManager am;
    //czy dialog został już odegrany
    bool played = false;
    //który dźwięk odegrać
    public int whichToPlay;

	// Use this for initialization
    /// <summary>
    /// Metoda odnajduje obiekt AudioManager w hierarchii sceny
    /// </summary>
	void Start () {
        //am.PlayMusic("1");
        //StartCoroutine(PlayAllTutorial());
        am = FindObjectOfType<AudioManager>();
        //StartCoroutine(FightWon());

    }

    /// <summary>
    /// Metoda włączająca dialog przy kolizjii gracza z obiektem
    /// </summary>
    /// <param name="other">Informacja o kolizji, z czym dany obiekt koliduje</param>
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
    
    /// <summary>
    /// Metoda odgrywająca dźwięk samouczka
    /// </summary>
    private IEnumerator PlayAllTutorial()
    {
        
        for (int i = 0; i <= 13; i++)
        {
            yield return new WaitForSeconds(am.PlayMusicLengthBase(i.ToString()));// + 0.25f);
        }
    }

    /// <summary>
    /// Metoda odtwarzająca dźwięk po samouczku
    /// </summary>
    public IEnumerator FightWon()
    {
        for(int i = 0; i < 14; i++)
        {
            am.StopMusic(i.ToString());
        }

        for(int i = 14; i < 17; i++)
        {
            yield return new WaitForSeconds(am.PlayMusicLengthBase(i.ToString()));
        }
    }
}
