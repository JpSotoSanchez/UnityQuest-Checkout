using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TMP_Text))]
public class ClickableLabel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string firstText = "Text A";
    [SerializeField] private string secondText = "Text B";

    private TMP_Text label;
    private bool showingFirst = true;

    private void Awake()
    {
        label = GetComponent<TMP_Text>();
        label.text = firstText;
    }

    // Called by the EventSystem when this UI element is clicked or tapped
    public void OnPointerClick(PointerEventData eventData)
    {
        showingFirst = !showingFirst;
        label.text = showingFirst ? firstText : secondText;
    }
}