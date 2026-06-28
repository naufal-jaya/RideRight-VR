using UnityEngine;
using System.Collections;

public class SceneFlow : MonoBehaviour
{
    [Header("Panels")]
    public GameObject homeScreen;
    public GameObject terminalScreen;
    public GameObject cardTap;
    public GameObject busInterior;
    public GameObject stationScreen;
    public GameObject endScreen;

    [Header("Skyboxes")]
    public Material terminalSkybox;
    public Material homeSkybox;
    public Material busSkybox;
    public Material stationSkyBox;

    [Header("World Canvases")]
    public GameObject terminalHotspots;
    public GameObject busHotspots;

    [Header("Settings")]
    public float busRideDuration = 30f;

    [Header("Auto Rotate")]
    public AutoRotate autoRotate;

    [Header("Camera Start Rotations")]
    public Vector3 terminalRotation = new Vector3(72, 72, 0);
    public Vector3 busRotation = new Vector3(0, 0, 0);
    public Vector3 stationRotation = new Vector3(0, 0, 0);

    void Start()
    {
        ShowHome();
    }

    public void ShowHome()
    {
        SetAll(false);
        homeScreen.SetActive(true);
        RenderSettings.skybox = homeSkybox;
        autoRotate.StartRotating();
        Camera.main.GetComponent<CameraLook>().enabled = false;
    }

    public void ShowTerminal()
    {
        SetAll(false);
        terminalScreen.SetActive(true);
        RenderSettings.skybox = terminalSkybox;
        autoRotate.StopRotating(); 
        terminalHotspots.SetActive(true);
        busHotspots.SetActive(false);
        Camera.main.GetComponent<CameraLook>().enabled = true;
        Camera.main.GetComponent<CameraLook>().SetInitialRotation(terminalRotation);
    }

    public void ShowCardTap()
    {
        SetAll(false);
        cardTap.SetActive(true);
        RenderSettings.skybox = null;
        terminalHotspots.SetActive(false);
        busHotspots.SetActive(false);
    }

    public void ShowBus()
    {
        SetAll(false);
        busInterior.SetActive(true);
        RenderSettings.skybox = busSkybox;
        busHotspots.SetActive(true);
        terminalHotspots.SetActive(false);
        GetComponent<BusRoute>().StartRoute(); 
    }

    public void ShowStation()
    {
        SetAll(false);
        stationScreen.SetActive(true);
        RenderSettings.skybox = stationSkyBox;
        busHotspots.SetActive(false);
        terminalHotspots.SetActive(false);
    }

    public void ShowEnd()
    {
        SetAll(false);
        endScreen.SetActive(true);
        RenderSettings.skybox = null;
    }

    void SetAll(bool state)
    {
        homeScreen.SetActive(state);
        terminalScreen.SetActive(state);
        cardTap.SetActive(state);
        busInterior.SetActive(state);
        stationScreen.SetActive(state);
        endScreen.SetActive(state);
    }
}