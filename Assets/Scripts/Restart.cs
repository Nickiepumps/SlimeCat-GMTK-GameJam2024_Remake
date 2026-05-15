using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] private string currentScene;
    public void RestartScene()
    {
        SceneManager.LoadScene(currentScene);
    }
}
