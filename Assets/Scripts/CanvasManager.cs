using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [Header("Canvas Reference")]
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

    private GameObject[] panels;

    void Awake()
    {
        panels = new GameObject[] { 
            selection, duration, kitCheck, orderCheck, 
            confirmation, noAvailable, charging, noComplete 
        };
    }

    public void resetScreen()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i] != null)
            {
                panels[i].SetActive(false);
            }
        }
    }

    private void OpenPanel(GameObject targetPanel)
    {
        resetScreen();

        if (targetPanel != null)
        {
            targetPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("El panel que intentas abrir no está asignado en el Inspector.");
        }
    }


    public void toSelection()
    {
        OpenPanel(selection);
    }

    public void toDuration()
    {
        OpenPanel(duration);
    }

    public void toKitCheck()
    {
        OpenPanel(kitCheck);
    }

    public void toOrderCheck()
    {
        OpenPanel(orderCheck);
    }

    public void toConfirmation()
    {
        OpenPanel(confirmation);
    }

    public void toNoAvailable()
    {
        OpenPanel(noAvailable);
    }

    public void toCharging()
    {
        OpenPanel(charging);
    }

    public void toNoComplete()
    {
        OpenPanel(noComplete);
    }
}