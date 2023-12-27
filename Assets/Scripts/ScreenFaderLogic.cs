using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFaderLogic : MonoBehaviour
{
    public static ScreenFaderLogic shared;

    private void Awake()
    {
        if (shared == null)
            shared = this;
    }

    public void FadeIntoScene(string sceneName)
    {
       StartCoroutine(SceneChangeAfterAnimation(sceneName));
    }

    IEnumerator SceneChangeAfterAnimation(string sceneName)
    {
        GetComponent<Animator>().SetTrigger("Fadeout");

        yield return new WaitForEndOfFrame();

        while (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }

        SceneManager.LoadSceneAsync(sceneName);
    }
}
