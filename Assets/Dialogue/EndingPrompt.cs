using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPrompt : MonoBehaviour
{
    public GameObject promptPrefab;
    public GameObject blackout;
    GameObject prompt;
    GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        blackout.SetActive(false);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //if player is within 2 units of the object, show the prompt
        if (Vector3.Distance(transform.position, player.transform.position) < 2f && prompt == null)
        {
            print("show prompt");
            //show the prompt
            prompt = Instantiate(promptPrefab);
            prompt.transform.position = new Vector3(4.5f, 0, 0);
        }
        else if(Vector3.Distance(transform.position, player.transform.position) > 2f && prompt != null)
        {
            //destroy the prompt
            Destroy(prompt);
            prompt = null;
        }

        if(prompt != null && Input.GetKeyDown(KeyCode.O))
        {
           //go to ending scene
           StartCoroutine(LeaveScene());
        }
    }

IEnumerator LeaveScene()
    {
        //disable player movement
        player.GetComponent<Player>().SetMovement(false);
        //fade in the screen
        blackout.SetActive(true);
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            blackout.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, i);
            yield return null;
        }
        //load the ending scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Final Lounge");

    }
}
