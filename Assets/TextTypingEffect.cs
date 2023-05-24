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
    bool hasStarted = false;

    public AudioClip typeSound;
    AudioSource audioSource;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        fullText = textMeshPro.text;
        textMeshPro.text = "";  // Clear the initial text
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && !hasStarted)
        {
            hasStarted = true;
            audioSource.Play();
            StartCoroutine(ShowText());
            //StartCoroutine(PlaySound());
        }
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textMeshPro.text = currentText;
            
            yield return new WaitForSeconds(typingSpeed);
        }
        audioSource.Stop();
    }

    //IEnumerator PlaySound()
    //{
    //    for (int i = 0; i <= fullText.Length / 8; i++)
    //    {
    //        Debug.Log("TAKK");
    //        if (audioSource != null)
    //        {
    //            audioSource.Play();
    //        }

    //        yield return new WaitForSeconds(typingSpeed * 4);
    //    }
    //}
}
