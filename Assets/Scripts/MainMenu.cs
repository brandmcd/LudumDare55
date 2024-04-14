using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameObject diamond, first, second;
    public static bool isStart = true;

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

    //function to swap current display
    public void Swap()
    {
        if (isStart)
        {
            first.SetActive(false);
            second.SetActive(true);
        }
        else
        {
            first.SetActive(true);
            second.SetActive(false);
        }
        isStart = !isStart;
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainArea");
    }
}
