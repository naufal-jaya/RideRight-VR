using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class BusRoute : MonoBehaviour
{
    [Header("Bus Stops")]
    public string[] stopNames = { "Bubulak", "Stop 2", "Stop 3", "Stop 4", "Stop 5", "Stop 6", "Terminal" };
    public float timeBetweenStops = 10f;

    [Header("Progress Bar")]
    public Image progressBar;           // the yellow fill image

    [Header("UI")]
    public TextMeshProUGUI stopFeedback;  // "Arriving at X" text
    public GameObject completionPopup;    // final popup

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip arrivalSound;

    private int currentStop = 0;
    private int totalStops;

    void Start()
    {
        totalStops = stopNames.Length;
        progressBar.fillAmount = 0f;
        stopFeedback.gameObject.SetActive(false);
        completionPopup.SetActive(false);
    }

    public void StartRoute()
    {
        currentStop = 0;
        progressBar.fillAmount = 0f;
        StartCoroutine(RouteTimer());
    }

    IEnumerator RouteTimer()
    {
        while (currentStop < totalStops)
        {
            yield return new WaitForSeconds(timeBetweenStops);
            ArriveAtStop(currentStop);
            currentStop++;
        }
    }

    void ArriveAtStop(int index)
    {
        // Update progress bar
        progressBar.fillAmount = (float)(index + 1) / totalStops;

        // Show feedback
        StartCoroutine(ShowStopFeedback(stopNames[index]));

        // Play sound
        if (arrivalSound != null)
            audioSource.PlayOneShot(arrivalSound);

        // Show completion popup on last stop
        if (index == totalStops - 1)
        {
            StartCoroutine(ShowCompletion());
        }
    }

    IEnumerator ShowStopFeedback(string stopName)
    {
        stopFeedback.text = "Arriving at " + stopName;
        stopFeedback.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        stopFeedback.gameObject.SetActive(false);
    }

    IEnumerator ShowCompletion()
    {
        yield return new WaitForSeconds(3f);
        completionPopup.SetActive(true);
    }
}