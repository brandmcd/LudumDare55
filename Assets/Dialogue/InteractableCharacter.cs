using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCharacter : DialogueUser
{
    [SerializeField] string _name;
    [SerializeField] ScriptableObject[] _assets;
    [SerializeField] GameObject promptPrefab;
    GameObject prompt;
    bool hasSpoken;
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        name = _name;
        assets = _assets;
        base.Start();
        dialogueManager.OnDialogueEnd += EndSpeaking;
    }


    private void FixedUpdate()
    {
        //when the player is close enough to the object, display the prompt
        if (Vector3.Distance(transform.position, player.transform.position) < 2f && !hasSpoken && prompt == null)
        {
            //change this troy
           prompt = Instantiate(promptPrefab, player.transform.position + new Vector3(-1, -1, 0), Quaternion.identity);

        }
        //if player walks away from the object, destroy the prompt
        else if (Vector3.Distance(transform.position, player.transform.position) > 2f && prompt != null)
        {
            Destroy(prompt);
            prompt = null;
        }
        //when the player is close enough to the object and presses the interact button, start speaking
        if (Vector3.Distance(transform.position, player.transform.position) < 2f && !hasSpoken && Input.GetButton("Interact"))
        {
            StartSpeaking();
            Destroy(prompt);
            prompt = null;
            hasSpoken = true;
        }
    }

    void StartSpeaking()
    {
        print("start speaking");
        OnBeginDialogue();
        //disable player movement
        player.GetComponent<Player>().SetMovement(false);
    }

    void EndSpeaking()
    {
        player.GetComponent<Player>().SetMovement(true);
    }
}
