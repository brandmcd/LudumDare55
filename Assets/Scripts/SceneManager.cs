using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public string upSceneName;
    public string downSceneName;
    public string leftSceneName;
    public string rightSceneName;
    GameObject player;
    static Direction lastDirection;
    static Vector2 lastPosition;

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    private void Start()
    {
        player = GameObject.Find("Player");
        print(lastDirection);
        //move player to opposite side of screen as they entered from
        if(lastDirection == Direction.Up)
        {
            player.transform.position = new Vector3(lastPosition.x, -4.5f, 0);
        }
        else if (lastDirection == Direction.Down)
        {
            player.transform.position = new Vector3(lastPosition.x, 4.5f, 0);
        }
        else if (lastDirection == Direction.Left)
        {
            player.transform.position = new Vector3(8.5f, lastPosition.y,0);
        }
        else if (lastDirection == Direction.Right)
        {
            player.transform.position = new Vector3(-8.5f, lastPosition.y, 0);
        }
    }

    private void Update()
    {
        //if player moves of screen bounds and that direction has a scene to load then load that scene
        if (player.transform.position.y > 5 && upSceneName != "")
        {
            lastDirection = Direction.Up;
            lastPosition = player.transform.position;
            UnityEngine.SceneManagement.SceneManager.LoadScene(upSceneName);
        }
        else if (player.transform.position.y < -5 && downSceneName != "")
        {
            lastDirection = Direction.Down;
            lastPosition = player.transform.position;
            UnityEngine.SceneManagement.SceneManager.LoadScene(downSceneName);
        }
        else if (player.transform.position.x < -9 && leftSceneName != "")
        {
            lastDirection = Direction.Left;
            lastPosition = player.transform.position;
            UnityEngine.SceneManagement.SceneManager.LoadScene(leftSceneName);
        }
        else if (player.transform.position.x > 9 && rightSceneName != "")
        {
            lastDirection = Direction.Right;
            lastPosition = player.transform.position;
            UnityEngine.SceneManagement.SceneManager.LoadScene(rightSceneName);
        }

    }
}
