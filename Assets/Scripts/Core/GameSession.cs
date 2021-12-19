using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    GameSession[] gameSessionArray;
    int currentSceneIndex;

    private void Awake() {
        gameSessionArray = FindObjectsOfType<GameSession>();
        if (gameSessionArray.Length > 1)
        {
            Destroy(this);
        }
        else
        {
          DontDestroyOnLoad(this);
        }
    }

    public void RestartScene()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
