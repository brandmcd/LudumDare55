using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class OpeningSequence2 : DialogueUser
{
    [SerializeField] private string _name;
    [SerializeField] private ScriptableObject[] dialogue;
    public GameObject blackoutCover;
    public AudioClip blackoutSound;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        //make sure the player can't move
        player.GetComponent<Player>().SetMovement(false);
        name = _name;
        assets = dialogue;
        base.Start();
        //disable the blackout cover
        blackoutCover.SetActive(true);
        StartCoroutine(OpeningSequenceRoutine());
    }

    IEnumerator OpeningSequenceRoutine()
    {
        
        //fade out the black screen
        for (float i = 1; i > 0; i -= Time.deltaTime)
        {
            blackoutCover.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, i);
            yield return new WaitForSeconds(0.01f);
        }
        StartSpeaking();
        //wait until the text box is done
        yield return new WaitUntil(() => !dialogueManager.textBox.isActiveAndEnabled);
        yield return new WaitForSeconds(1);
        //fade in the black screen
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            blackoutCover.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, i);
            yield return new WaitForSeconds(0.01f);
        }
        //load the real
    }
    void StartSpeaking()
    {
        //might need to add something here idk yet
        OnBeginDialogue();
    }


   
}
