using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardTap : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform dropZone;
    public GameObject feedbackText;
    public SceneFlow sceneFlow;
    public float autoAdvanceDelay = 2f;

    private Vector2 startPosition;

    void Start()
    {
        startPosition = GetComponent<RectTransform>().anchoredPosition;
        feedbackText.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        GetComponent<RectTransform>().anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform cardRect = GetComponent<RectTransform>();

        if (RectOverlaps(cardRect, dropZone))
        {
            feedbackText.SetActive(true);
            Invoke("GoToBus", autoAdvanceDelay);
        }
        else
        {
            cardRect.anchoredPosition = startPosition;
        }
    }

    bool RectOverlaps(RectTransform a, RectTransform b)
    {
        Rect aRect = GetWorldRect(a);
        Rect bRect = GetWorldRect(b);
        return aRect.Overlaps(bRect);
    }

    Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        return new Rect(corners[0].x, corners[0].y,
            corners[2].x - corners[0].x,
            corners[2].y - corners[0].y);
    }

    void GoToBus()
    {
        sceneFlow.ShowBus();
    }
}