using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //functino to start the game
    public void StartGame()
    {
        //load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainArea");
    }
}
