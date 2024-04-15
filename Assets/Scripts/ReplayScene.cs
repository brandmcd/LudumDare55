using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Replay()
    {
        //load opening scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Opening");
    }
}
