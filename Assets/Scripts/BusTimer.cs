using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class BusRoute : MonoBehaviour
{
    [Header("Bus Stops")]
    public BusStop[] stops;
    public float timeBetweenStops = 10f;
    public float audioBeforeArrival = 5f;

    [Header("Progress Bar")]
    public Image progressBar;

    [Header("UI")]
    public TextMeshProUGUI stopFeedback;
    public GameObject completionPopup;
    public GameObject goToStationButton;  

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip completionAudio;     

    [Header("Scene Flow")]
    public SceneFlow sceneFlow;

    private int currentStop = 0;
    private int totalStops;

    void Start()
    {
        totalStops = stops.Length;
        progressBar.fillAmount = 0f;
        stopFeedback.gameObject.SetActive(false);
        completionPopup.SetActive(false);
        goToStationButton.SetActive(false);
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
            yield return new WaitForSeconds(timeBetweenStops - audioBeforeArrival);

            if (stops[currentStop].arrivalAudio != null)
                audioSource.PlayOneShot(stops[currentStop].arrivalAudio);

            yield return new WaitForSeconds(audioBeforeArrival);

            ArriveAtStop(currentStop);
            currentStop++;
        }
    }

    void ArriveAtStop(int index)
    {
        progressBar.fillAmount = (float)(index + 1) / totalStops;
        StartCoroutine(ShowStopFeedback(stops[index].stopName));

        if (index == totalStops - 1)
            StartCoroutine(ShowCompletion());
    }

    IEnumerator ShowStopFeedback(string stopName)
    {
        stopFeedback.text = "Sampai di " + stopName;
        stopFeedback.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        stopFeedback.gameObject.SetActive(false);
    }

    IEnumerator ShowCompletion()
    {
        yield return new WaitForSeconds(2f);

        // Play completion audio
        if (completionAudio != null)
            audioSource.PlayOneShot(completionAudio);

        // Show popup and button
        completionPopup.SetActive(true);
        goToStationButton.SetActive(true);
    }

    public void GoToStation()
    {
        sceneFlow.ShowStation();
    }
}