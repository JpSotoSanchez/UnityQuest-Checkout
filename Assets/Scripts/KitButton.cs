using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class KitButton : MonoBehaviour
{
    public int kitNumber;
    public Text label;
    public CanvasManager canvasManager;

    private Button button;
    private KitData currentKit;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Apply(KitData kit)
    {
        currentKit = kit;
        ColorBlock colors = button.colors;
        Color c = Color.white;

        if (kit != null)
        {
            if (kit.available == 0)
            {
                c = Color.green;
                button.onClick.AddListener(delegate {canvasManager.toDuration();});
            }
            else if (kit.available == 1)
            {
                c = Color.yellow;
                button.onClick.AddListener(delegate {canvasManager.toCharging();});

            }
            else if (kit.available == 2)
            {
                c = Color.red;
                button.onClick.AddListener(delegate {canvasManager.toNoAvailable();});

            }
            else if (kit.available == 3)
            {
                c = new Color(1f, 0.5f, 0f); // orange
                button.onClick.AddListener(delegate {canvasManager.toNoComplete();});

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