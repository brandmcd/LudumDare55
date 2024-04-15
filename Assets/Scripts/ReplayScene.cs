using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReplayScene : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        //  PlayerPrefs.SetString("ending", correct ? "good" : "bad");
        if(PlayerPrefs.GetString("ending") == "good")
        {
            text.text = "You Win!";
        }
        else
        {
            text.text = "You Lose.";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void Replay()
    {
        GameObject obj = GameObject.Find("Murder Mystery level theme");
        if (obj != null)
        {
            Destroy(obj);
        }
        PlayerPrefs.DeleteAll();
        //load opening scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
