using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeOnApproachDialogue : DialogueUser
{
    [SerializeField] string _name;
    [SerializeField] ScriptableObject[] _assets;
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
        //when the player is close enough to the object, start speaking
        if (Vector3.Distance(transform.position, player.transform.position) < 1f && !hasSpoken)
        {
            StartSpeaking();
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
