using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TyerRex : DialogueUser
{
    [SerializeField] private string _name;
    [SerializeField] private ScriptableObject[] initialTestimony;
    [SerializeField] private ScriptableObject[] idle;
    [SerializeField] private string leoPref;
    [SerializeField] private string valPref;
    [SerializeField] private ScriptableObject[] secondTestimonyLeoSolo;
    [SerializeField] private ScriptableObject[] secondTestimonyValSolo;
    [SerializeField] private ScriptableObject[] secondTestimonyLeoAndVal;
    [SerializeField] private ScriptableObject[] thirdTestimony;
    [SerializeField] private GameObject promptPrefab;
    private GameObject prompt;
    public bool interactable = true;
    private bool hasSpoken;
    private bool hasRevealed;
    private bool isIdle;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        string thing = PlayerPrefs.GetString(gameObject.name + "hasSpoken", "false");
        string thing2 = PlayerPrefs.GetString(gameObject.name + "hasRevealed", "false");
        //set hasSpoken to true if the player has spoken to this suspect before
        if (thing == "true")
        {
            hasSpoken = true;
            isIdle = true;
        }
        else
        {
            isIdle = false;
            hasSpoken = false;
        }

        //set hasRevealed to true if the player has revealed the contradiction to this suspect before
        if (thing2 == "true")
        {
            hasRevealed = true;
        }
        else
        {
            hasRevealed = false;
        }
        player = GameObject.Find("Player");
        name = _name;
        assets = initialTestimony;
        base.Start();
        dialogueManager.OnDialogueEnd += EndSpeaking;
    }

    private void FixedUpdate()
    {
        if (!interactable) return;

        HandlePrompt();
        HandleInteraction();
    }

    private void HandlePrompt()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2f && prompt == null)
        {
            //check if is speaking
            if (dialogueManager.textBox.isActiveAndEnabled)
            {
                return;
            }
            prompt = Instantiate(promptPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > 2f && prompt != null)
        {
            Destroy(prompt);
            prompt = null;
        }
    }

    private void HandleInteraction()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2f && Input.GetButton("Interact"))
        {
            StartSpeaking();
            Destroy(prompt);
            prompt = null;
            interactable = false;
        }
    }

    void StartSpeaking()
    {
        if (!hasSpoken)
        {
            assets = initialTestimony;
        }
        else
        {
            
             if (PlayerPrefs.GetString(leoPref) == "true" && PlayerPrefs.GetString(valPref) == "true")
            {
                assets = secondTestimonyLeoAndVal;
                //add the third testimony
                assets = assets.Concat(thirdTestimony).ToArray();
            }
            else if (PlayerPrefs.GetString(leoPref) == "true")
            {
                assets = secondTestimonyLeoSolo;
                //add the third testimony
                assets = assets.Concat(thirdTestimony).ToArray();
            }
            else if (PlayerPrefs.GetString(valPref) == "true")
            {
                assets = secondTestimonyValSolo;
                //add the third testimony
                assets = assets.Concat(thirdTestimony).ToArray();
            }
            else if (isIdle)
            {
                assets = idle;
            }
            hasRevealed = true;
            PlayerPrefs.SetString(gameObject.name + "hasRevealed", "true");
        }

        print("assets: " + assets.Length);
        OnBeginDialogue();
        player.GetComponent<Player>().SetMovement(false);
    }

   
    void EndSpeaking()
    {
        if (!hasSpoken)
        {
            PlayerPrefs.SetString(gameObject.name + "hasSpoken", "true");
            hasSpoken = true;
            isIdle = true;
        }
        player.GetComponent<Player>().SetMovement(true);
        StartCoroutine(ResetInteractability());
    }

    IEnumerator ResetInteractability()
    {
        yield return new WaitForSeconds(3);
        interactable = true;
    }
}
