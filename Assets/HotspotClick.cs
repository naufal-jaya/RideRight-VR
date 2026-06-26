using UnityEngine;
using UnityEngine.EventSystems;

public class HotspotClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject infoCard;

    public void OnPointerClick(PointerEventData eventData)
    {
        infoCard.SetActive(true);
    }
}