using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class TutorialManager : MonoBehaviour
{
    public GameOverManager gameOverManager;

    public AudioManager audioManager;

    public AudioMixer masterMixer;

    private bool isAnimating = false;

    public void OnPausePressed(CallbackContext context)
    {
        if (context.canceled)
        {
            Debug.Log("Pause pressed");
            if (!gameOverManager.isFinished && !isAnimating)
            {

                StartCoroutine(PauseMenuAnimation());

            }
        }

    }

    private IEnumerator PauseMenuAnimation()
    {
        isAnimating = true;
        bool isPaused = Time.timeScale == 0;
        if (isPaused)
        {
            Time.timeScale = isPaused ? 1 : 0;
            audioManager.PaperClose();
            LeanTween.value(700, 22000, 0.5f).setOnUpdate((val) =>
            {
                masterMixer.SetFloat("CutoffLowpass", val);
            });
            LeanTween.value(-10, 0, 0.5f).setOnUpdate((val) =>
            {
                masterMixer.SetFloat("MasterVolume", val);
            });
            transform.GetChild(2).gameObject.SetActive(!isPaused);
            Image bolaSr = transform.GetChild(1).GetComponent<Image>();
            yield return new WaitForSeconds(0.2f);
            bolaSr.transform.eulerAngles = new Vector3(0, 0, 70);
            yield return new WaitForSeconds(0.2f);
            bolaSr.transform.eulerAngles = new Vector3(0, 0, 10);
            yield return new WaitForSeconds(0.2f);
            bolaSr.transform.eulerAngles = new Vector3(0, 0, 260);
            transform.GetChild(0).gameObject.SetActive(!isPaused);
            transform.GetChild(1).gameObject.SetActive(!isPaused);
        }
        else
        {
            audioManager.PaperOpen();
            LeanTween.value(22000, 700, 0.5f).setOnUpdate((val) =>
            {
                masterMixer.SetFloat("CutoffLowpass", val);
            });
            LeanTween.value(0, -10, 0.5f).setOnUpdate((val) =>
            {
                masterMixer.SetFloat("MasterVolume", val);
            });
            Image bolaSr = transform.GetChild(1).GetComponent<Image>();
            transform.GetChild(0).gameObject.SetActive(!isPaused);
            transform.GetChild(1).gameObject.SetActive(!isPaused);
            yield return new WaitForSeconds(0.2f);
            bolaSr.transform.eulerAngles = new Vector3(0, 0, 170);
            yield return new WaitForSeconds(0.2f);
            bolaSr.transform.eulerAngles = new Vector3(0, 0, 10);
            yield return new WaitForSeconds(0.2f);
            bolaSr.transform.eulerAngles = new Vector3(0, 0, 100);
            transform.GetChild(2).gameObject.SetActive(!isPaused);
            Time.timeScale = isPaused ? 1 : 0;
        }
        isAnimating = false;
        yield return 0;
    }
}
