using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class Ending : DialogueUser
{
    [SerializeField] private string _name;
    [SerializeField] private ScriptableObject[] intro;
    [SerializeField] private ScriptableObject[] Leo;
    [SerializeField] private ScriptableObject[] MrDactyl;
    [SerializeField] private ScriptableObject[] RexNoEvidence;
    [SerializeField] private ScriptableObject[] Rex;
    [SerializeField] private ScriptableObject[] MrsDactyl;
    [SerializeField] private ScriptableObject[] Valorie;
    [SerializeField] private ScriptableObject[] GoodOutro;
    [SerializeField] private ScriptableObject[] BadOutro;
    [SerializeField] private GameObject selection;

    string[] neededPrefs = { "val2", "rex2" };
    bool correct = false;
    string selectedPerson = "";
    public GameObject blackoutCover;

    // Start is called before the first frame update
    void Start()
    {
        name = _name;
        assets = intro;
        base.Start();
        //disable the blackout cover
        blackoutCover.SetActive(true);
        selection.SetActive(false);
        dialogueManager.OnDialogueEnd += EndSpeaking;
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
        blackoutCover.SetActive(false);
        yield return new WaitForEndOfFrame();
        StartSpeaking();
        yield return new WaitForSeconds(1);
        //wait until the text box is done
        yield return new WaitUntil(() => !dialogueManager.textBox.isActiveAndEnabled);
        yield return new WaitForSeconds(1);
        //enable selection
        selection.SetActive(true);
        //wait until a selection is made
        yield return new WaitUntil(() => selectedPerson != "");
        yield return new WaitForSeconds(0.5f);
        selection.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        //run that person's dialogue
        switch (selectedPerson)
        {
            case "Leo":
                assets = Leo;
                break;
            case "Mr. Dactyl":
                assets = MrDactyl;
                break;
            case "Rex":
                //rex is the person so we need to check if the player has the right prefs
                assets = Rex;
                if (neededPrefs.Any(pref => PlayerPrefs.GetString(pref) == "true"))
                {
                    assets = Rex;
                    correct = true;
                }
                else
                {
                    assets = RexNoEvidence;
                }
                break;
            case "Mrs. Dactyl":
                assets = MrsDactyl;
                break;
            case "Valorie":
                assets = Valorie;
                break;
        }
        //make a player pref for the ending
        PlayerPrefs.SetString("ending", correct ? "good" : "bad");
        yield return new WaitForEndOfFrame();
        StartSpeaking();
        yield return new WaitUntil(() => !dialogueManager.textBox.isActiveAndEnabled);
        if(correct)
        {
            assets = GoodOutro;
        }
        else
        {
            assets = BadOutro;
        }
        StartSpeaking();
        yield return new WaitUntil(() => !dialogueManager.textBox.isActiveAndEnabled);
        yield return new WaitForSeconds(1);
        //fade in the black screen
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            blackoutCover.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, i);
            yield return new WaitForSeconds(0.01f);
        }
        //load the fake main area

        UnityEngine.SceneManagement.SceneManager.LoadScene("OfferReplay");
    }
    void StartSpeaking()
    {
        print("assets: " + assets.Length);
        //might need to add something here idk yet
        OnBeginDialogue();
    }

    void EndSpeaking()
    {

    }

    public void SelectLeo()
    {
        selectedPerson = "Leo";
    }
    
    public void SelectMrDactyl()
    {
        selectedPerson = "Mr. Dactyl";
    }

    public void SelectRex()
    {
        selectedPerson = "Rex";
    }

    public void SelectMrsDactyl()
    {
        selectedPerson = "Mrs. Dactyl";
    }

    public void SelectValorie()
    {
        selectedPerson = "Valorie";
    }

}
