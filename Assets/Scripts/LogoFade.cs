using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoFade : MonoBehaviour {

    private float fadeDuration = 5.0f;
    public Image logo;
    public Image controls;
    public Camera myCam;

    private Coroutine myCoroutine;

	// Use this for initialization
    void Start () {

        if (logo == null) 
        {
            logo = gameObject.GetComponent<Image>();
        }

        logo.canvasRenderer.SetAlpha(0.0f);
        controls.canvasRenderer.SetAlpha(0.0f);
        myCam.gameObject.SetActive(true);
        myCoroutine = StartCoroutine((FadeCoroutine()));

	}

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (myCoroutine != null)
            {
                StopCoroutine(myCoroutine);
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
        }
    }

    IEnumerator FadeCoroutine() {
        yield return new WaitForSeconds(1.0f);
        logo.CrossFadeAlpha(1.0f, fadeDuration / 2f, false);
        controls.CrossFadeAlpha(1.0f, fadeDuration / 2f, false);
        yield return new WaitForSeconds(fadeDuration / 2f);
        yield return new WaitForSeconds(3.0f);
        logo.CrossFadeAlpha(0.0f, fadeDuration / 2f, false);
        controls.CrossFadeAlpha(0.0f, fadeDuration / 2f, false);
        yield return new WaitForSeconds(fadeDuration / 2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
	
}
