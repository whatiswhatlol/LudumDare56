using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;  // Ensure DOTween is imported

public class SceneChange : MonoBehaviour
{
    public GameObject fadeCanvas;  // Reference to the canvas with the fade image
    public float fadeDuration = 1f;  // Duration of the fade

    public AudioSource audioSource;

    public void GoToMenu()
    {
        playNoice();
        // Instantiate the fade canvas and get the Image component
        GameObject fadeInstance = Instantiate(fadeCanvas);
        Image fadeImage = fadeInstance.GetComponentInChildren<Image>();

        // Fade the image to black over the specified duration
        fadeImage.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            // Once fade is complete, load the Menu scene
            SceneManager.LoadScene("Menu");

        });
    }

    public void GoToGame()
    {
        playNoice();
        // Instantiate the fade canvas and get the Image component
        GameObject fadeInstance = Instantiate(fadeCanvas);
        Image fadeImage = fadeInstance.GetComponentInChildren<Image>();

        // Fade the image to black over the specified duration
        fadeImage.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            // Once fade is complete, load the Menu scene
            SceneManager.LoadScene("Game");
        });
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void debug()
    {
        Debug.Log("Debug");
    }

    public void playNoice()
    {
        audioSource.Play();
    }

}
