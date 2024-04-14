using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class OpeningSequence : DialogueUser
{
    [SerializeField] private string _name;
    [SerializeField] private ScriptableObject[] preBlackout;
    [SerializeField] private ScriptableObject[] inBlackout;
    [SerializeField] private ScriptableObject[] postBlackout;
    public GameObject blackoutCover;
    public AudioClip blackoutSound;

    // Start is called before the first frame update
    void Start()
    {
        name = _name;
        assets = preBlackout;
        base.Start();
        StartCoroutine(OpeningSequenceRoutine());
    }

    IEnumerator OpeningSequenceRoutine()
    {
        StartSpeaking();
        //wait until the text box is done
        yield return new WaitUntil(() => !dialogueManager.textBox.isActiveAndEnabled);
        yield return new WaitForSeconds(1);
        blackoutCover.SetActive(true);
        AudioSource audioSource = GetComponent<AudioSource>();
        yield return new WaitForSeconds(1);
        audioSource.PlayOneShot(blackoutSound);
        assets = inBlackout;
        yield return new WaitForSeconds(1);
        StartSpeaking();
        yield return new WaitUntil(() => !dialogueManager.textBox.isActiveAndEnabled);
        //remove blackout
        blackoutCover.SetActive(false);
        assets = postBlackout;
        yield return new WaitForSeconds(1);
        StartSpeaking();
        yield return new WaitUntil(() => !dialogueManager.textBox.isActiveAndEnabled);
        yield return new WaitForSeconds(1);
        //load the fake main area
    }
    void StartSpeaking()
    {
        //might need to add something here idk yet
        OnBeginDialogue();
    }


   
}
