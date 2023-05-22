using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class NPCDialogue : MonoBehaviour
{
    public GameObject canvas;
    private Text firstText;
    private Text secondText;

    public float delayBeforeSecondText = 2f; // Adjust the delay time as needed

    private void Start()
    {
        // Get the Text components from the children of the canvas
        firstText = canvas.transform.GetChild(0).GetComponent<Text>();
        secondText = canvas.transform.GetChild(1).GetComponent<Text>();

        // Hide the second text initially
        secondText.gameObject.SetActive(false);
    }

    public void ShowFirstText()
    {
        // Show the first text
        firstText.gameObject.SetActive(true);

        // Start the coroutine to show the second text after a delay
        StartCoroutine(ShowSecondTextDelayed());
    }

    private IEnumerator ShowSecondTextDelayed()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delayBeforeSecondText);

        // Hide the first text
        firstText.gameObject.SetActive(false);

        // Show the second text
        secondText.gameObject.SetActive(true);
    }
}
