using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class KitButton : MonoBehaviour
{
    public int kitNumber;
    public Text label;
    public CanvasManager canvasManager;
    public KitSelector kitSelector;

    private Button button;
    private KitData currentKit;

    void Awake()
    {
        button = GetComponent<Button>();

        // One listener for the whole life of the button.
        // It checks the current state when pressed, so refreshing never stacks listeners.
        button.onClick.AddListener(OnPressed);
    }

    private void OnPressed()
    {
        if (currentKit == null)
        {
            return;
        }

        kitSelector.KitSelected(kitNumber);

        if (currentKit.available == 0)
        {
            canvasManager.toDuration();
        }
        else if (currentKit.available == 1)
        {
            canvasManager.toCharging();
        }
        else if (currentKit.available == 2)
        {
            kitSelector.ShowAvailableFrom();   // fills the date labels
            canvasManager.toNoAvailable();
        }
        else if (currentKit.available == 3)
        {
            canvasManager.toNoComplete();
        }
    }

    public void Apply(KitData kit)
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        currentKit = kit;
        ColorBlock colors = button.colors;
        Color c = Color.white;

        if (kit != null)
        {
            if (kit.available == 0)
            {
                c = Color.green;
            }
            else if (kit.available == 1)
            {
                c = Color.yellow;
            }
            else if (kit.available == 2)
            {
                c = Color.red;
            }
            else if (kit.available == 3)
            {
                c = new Color(1f, 0.5f, 0f); // orange
            }
        }

        colors.normalColor = c;
        colors.selectedColor = c;
        button.colors = colors;

        if (label != null)
        {
            label.text = "Kit " + kitNumber;
        }
    }
}