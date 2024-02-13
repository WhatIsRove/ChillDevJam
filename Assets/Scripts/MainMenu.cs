using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        StartCoroutine(StartNextLevel());
    }

    public void Quit()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Application.Quit();
    }

    IEnumerator StartNextLevel()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        GameObject.FindGameObjectWithTag("CrossFade").GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
