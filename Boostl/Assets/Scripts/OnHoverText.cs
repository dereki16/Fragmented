using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OnHoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI theText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = Color.black;
    }
    public void OnDeselect(PointerEventData eventData)
    {
        theText.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = Color.white;
    }
}
