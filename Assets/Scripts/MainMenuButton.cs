using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private UnityEngine.UI.Image spriteRender;
    [SerializeField] private Sprite spriteOn;
    [SerializeField] private Sprite spriteOff;
     public void LoadSceneByIndex(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
    public void ToggleMusic()
    {
        if( spriteRender.sprite == spriteOn ) spriteRender.sprite = spriteOff;
        else { spriteRender.sprite = spriteOn; }
        musicSource.mute = !musicSource.mute;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}