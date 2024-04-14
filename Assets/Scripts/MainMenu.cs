using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameObject diamond;

    // Start is called before the first frame update
    void Start()
    {
        //turn off diamond animation
        diamond.GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //functino to start the game
    public void StartGame()
    {
        diamond.GetComponent<Animator>().enabled = true;
        diamond.GetComponent<AudioSource>().Play();
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainArea");
    }
}
