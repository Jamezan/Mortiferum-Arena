using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{

    public string difficultyChosen;

    public void ChangeSceneToLevel1Easy() {
        SceneManager.LoadScene("Level1");
    }

    public void ChangeSceneToLevel1Medium() {
        SceneManager.LoadScene("Level1Medium");

    }

    public void ChangeSceneToLevel1Hard() {
        SceneManager.LoadScene("Level1Hard");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
