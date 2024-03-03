using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	AudioSource startingNoise;

    private void Awake()
    {
        startingNoise = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ButtonHandlerPlay()
	{
        StartCoroutine(PlaySoundAndStartGame());	
	}

    IEnumerator PlaySoundAndStartGame()
    {
        startingNoise.Play();
        yield return new WaitForSeconds(startingNoise.clip.length);

        SceneManager.LoadSceneAsync(1);
    }
}
