using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextPopupFade : MonoBehaviour
{
    public float timeToFade = 1f;
    
    public void CreatePopup(Text text)
    {
        StartCoroutine(FadeOut(text));
    }
    
    public void CreatePopup(TMP_Text text)
    {
        StartCoroutine(FadeOut(text));
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
}
