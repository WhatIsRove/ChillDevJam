using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject crossFade;

    public bool isPaused = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused /*&& SceneManager.GetActiveScene().buildIndex != 0*/)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Resume();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        isPaused = true;

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        isPaused = false;

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }

    public void StartNextLevel()
    {
        StartCoroutine(NextLevel());
    }

    public void RestartCurrentLevel()
    {
        StartCoroutine(RestartLevel());
    }

    public void BackOneLevel()
    {
        StartCoroutine(PreviousLevel());
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void BackMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    IEnumerator PreviousLevel()
    {
        crossFade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    IEnumerator NextLevel()
    {
        crossFade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator RestartLevel()
    {
        Time.timeScale = 1f;
        yield return null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
