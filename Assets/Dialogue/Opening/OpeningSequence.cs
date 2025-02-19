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
    public AudioSource music;
    public GameObject blackoutCover;
    public AudioClip blackoutSound;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        name = _name;
        assets = preBlackout;
        base.Start();
        //disable the blackout cover
        blackoutCover.SetActive(false);
        dialogueManager.OnDialogueEnd += EndSpeaking;
        StartCoroutine(OpeningSequenceRoutine());
    }

    IEnumerator OpeningSequenceRoutine()
    {
        yield return new WaitForSeconds(1);
        StartSpeaking();
        //wait until the text box is done
        yield return new WaitUntil(() => !dialogueManager.textBox.isActiveAndEnabled);
        yield return new WaitForSeconds(1);
        blackoutCover.SetActive(true);
        //pause the music
        music.Pause();
        AudioSource audioSource = Camera.main.GetComponent<AudioSource>();
        yield return new WaitForSeconds(0.5f);
        if (blackoutSound != null)
        { audioSource.PlayOneShot(blackoutSound); }
        assets = inBlackout;
        yield return new WaitForSeconds(1);
        StartSpeaking();
        yield return new WaitUntil(() => !dialogueManager.textBox.isActiveAndEnabled);
        //remove blackout
        yield return new WaitForSeconds(1);
        music.UnPause();
        blackoutCover.SetActive(false);
        assets = postBlackout;
        yield return new WaitForSeconds(1);
        StartSpeaking();
        yield return new WaitUntil(() => !dialogueManager.textBox.isActiveAndEnabled);
        //fade in the black screen

        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            blackoutCover.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, i);
            blackoutCover.SetActive(true);
            yield return new WaitForSeconds(0.01f);
        }
        //load the fake main area
        UnityEngine.SceneManagement.SceneManager.LoadScene("Fake MainArea");
    }
    void StartSpeaking()
    {
        //might need to add something here idk yet
        OnBeginDialogue();
    }

    void EndSpeaking()
    {
        //why the f*$@ does this need to be here
    }

   
}
