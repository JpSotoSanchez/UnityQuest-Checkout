using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    [Header("Panels")]
    public Canvas canvas;

    [Header("Panels")]
    public GameObject selection; 
    public GameObject duration; 
    public GameObject kitCheck; 
    public GameObject orderCheck; 
    public GameObject confirmation; 
    public GameObject noAvailable; 
    public GameObject charging; 
    public GameObject noComplete; 

    // 1. Declaras el array vacío
    private GameObject[] panels;

    void Awake()
    {
        // 2. Lo inicializas con las referencias cuando el script despierta
        panels = new GameObject[] { 
            selection, duration, kitCheck, orderCheck, 
            confirmation, noAvailable, charging, noComplete 
        };
    }





    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetScreen()
    {
        for(int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
    }

    public void toMenu()
    {
        resetScreen();
        selection.SetActive(true);
    }


}
