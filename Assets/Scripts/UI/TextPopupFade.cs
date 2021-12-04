using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextPopupFade : MonoBehaviour
{
    public float timeToFade = 1f;
    public bool doFadeIn = false;
    
    public void CreatePopup(Text text)
    {
        if (doFadeIn)
        {
            StartCoroutine(FadeIn(text));
        }
        else
        {
            StartCoroutine(FadeOut(text));
        }
    }
    
    public void CreatePopup(TMP_Text text)
    {
        if (doFadeIn)
        {
            StartCoroutine(FadeIn(text));
        }
        else
        {
            StartCoroutine(FadeOut(text));
        }
    }
    
    
    // TextMesh
    private IEnumerator FadeOut(TMP_Text text)
    {
        text.gameObject.SetActive(true);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        while (text.color.a > 0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / timeToFade));
            yield return null;
        }
        text.gameObject.SetActive(false);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
    }
    
    // Regular Text
    private IEnumerator FadeOut(Text text)
    {
        text.gameObject.SetActive(true);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        while (text.color.a > 0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / timeToFade));
            yield return null;
        }
        text.gameObject.SetActive(false);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
    }
    
    // TextMesh
    private IEnumerator FadeIn(TMP_Text text)
    {
        text.gameObject.SetActive(true);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
        while (text.color.a < 1f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / timeToFade));
            yield return null;
        }
    }
    
    // Regular Text
    private IEnumerator FadeIn(Text text)
    {
        text.gameObject.SetActive(true);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
        while (text.color.a < 1f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / timeToFade));
            yield return null;
        }
    }
}
