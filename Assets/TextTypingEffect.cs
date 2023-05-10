using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTypingEffect : MonoBehaviour
{
    public float typingSpeed = 0.1f;  // The speed at which characters appear
    private TextMeshProUGUI textMeshPro;
    private string fullText;
    private string currentText;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        fullText = textMeshPro.text;
        textMeshPro.text = "";  // Clear the initial text
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textMeshPro.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
