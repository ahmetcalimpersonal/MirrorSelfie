using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGameRunning;
    public GameObject completePanel;
    private void Start()
    {
        isGameRunning = true;
    }
    public void Complete()
    {
        if (isGameRunning)
        {
            isGameRunning = false;
            completePanel.SetActive(true);
        }
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
}
