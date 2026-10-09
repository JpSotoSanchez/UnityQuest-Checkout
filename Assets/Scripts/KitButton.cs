using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class KitButton : MonoBehaviour
{
    public int kitNumber;            
    public Text label;               
    private Button button;

    public CanvasManager canvasManager;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void Apply(KitData kit)
    {
        var colors = button.colors;   

        Color c = Color.white;
        if (kit != null)
        {
            switch (kit.available)
            {
                case 0: 
                    c = Color.green;  
                    button.onClick.AddListener(delegate{canvasManager.toMenu();});
                break;
                case 1: 
                    c = Color.yellow; 
                break;
                case 2: 
                    c = Color.red;
                break;
            }
        }

        colors.normalColor = c;
        colors.selectedColor = c;     
        button.colors = colors;

        if (label != null) label.text = "Kit " + kitNumber;
    }
}