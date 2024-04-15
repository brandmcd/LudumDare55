using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class SuspectBase : DialogueUser
{
    [SerializeField] private string _name;
    [SerializeField] private ScriptableObject[] initialTestimony;
    [SerializeField] private ScriptableObject[] idle;
    [SerializeField] private string[] contradictionPlayerPrefs;
    [SerializeField] private ScriptableObject[] secondTestimony;
    [SerializeField] private GameObject promptPrefab;
    private GameObject prompt;
    public bool interactable = true;
    private bool hasSpoken = false;
    private bool hasRevealed = false;
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
        if(!interactable) return;

        HandlePrompt();
        HandleInteraction();
    }

    private void HandlePrompt()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2f &&  prompt == null)
        {
            //check if is speaking
            if(dialogueManager.textBox.isActiveAndEnabled)
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
        print("haslayerSpoken: " + hasSpoken);  
        if (!hasSpoken)
        {
            
          assets = initialTestimony;
            PlayerPrefs.SetString(gameObject.name + "hasSpoken", "true");
            hasSpoken = true;
            isIdle = true;
            
        }
        else if (CheckContradictions() && !hasRevealed)
        {
            assets = secondTestimony;
            PlayerPrefs.SetString(gameObject.name + "hasRevealed", "true");
        }
        else if (isIdle)
        {
            assets = idle;
        }
        print("assets: " + assets.Length);
        OnBeginDialogue();
        player.GetComponent<Player>().SetMovement(false);
    }

    bool CheckContradictions()
    {
        return contradictionPlayerPrefs.Any(pref => PlayerPrefs.GetString(pref) == "true");
    }

    void EndSpeaking()
    {
        print("End speaking");
        
        player.GetComponent<Player>().SetMovement(true);
        StartCoroutine(ResetInteractability());
    }

    IEnumerator ResetInteractability()
    {
        yield return new WaitForSeconds(3);
        interactable = true;
    }
}
