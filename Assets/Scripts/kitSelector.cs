using System;
using UnityEngine;
using UnityEngine.UI;

public class kitSelector : MonoBehaviour
{
    [Header("References")]
    public KitManager kitManager;

    [Header("Stage 2: duration (existing dropdowns in the scene)")]
    [SerializeField] private Dropdown dayDropdown;
    [SerializeField] private Dropdown hourDropdown;
    [SerializeField] private Dropdown minuteDropdown;

    [Header("Stage 3: completeness checklist")]
    [SerializeField] private Toggle headsetToggle;
    [SerializeField] private Toggle controlRToggle;
    [SerializeField] private Toggle controlLToggle;
    [SerializeField] private Toggle chargerToggle;

    [Header("Stage 4: review (optional)")]
    [SerializeField] private Text summaryText;

    // Current selection (other scripts can read these)
    public int kitSelected = -1;   // -1 means no kit selected
    public int days;
    public int hours;
    public int minutes;
    public bool headset;
    public bool controlR;
    public bool controlL;
    public bool charger;
    public bool complete;          // true only if all four items are checked

    // ---------- Stage 1: kit ----------

    // Called by the kit button
    public void KitSelected(int n)
    {
        this.kitSelected = n;
    }

    public bool HasKit()
    {
        return kitSelected >= 0;
    }

    // ---------- Reading the UI ----------

    // Converts the text of the selected option into a number ("6" or "6 h" both give 6)
    private int GetDropdownNumber(Dropdown dropdown)
    {
        string text = dropdown.options[dropdown.value].text;
        int result;

        if (int.TryParse(text, out result) == true)
        {
            return result;
        }

        // The text has extra characters: keep the first group of digits
        string digits = "";
        for (int i = 0; i < text.Length; i++)
        {
            if (char.IsDigit(text[i]) == true)
            {
                digits = digits + text[i];
            }
            else if (digits.Length > 0)
            {
                break;
            }
        }

        if (digits.Length > 0 && int.TryParse(digits, out result) == true)
        {
            return result;
        }

        Debug.LogWarning("kitSelector: could not read a number from the option '" + text + "'.");
        return 0;
    }

    private void ReadDuration()
    {
        days = GetDropdownNumber(dayDropdown);
        hours = GetDropdownNumber(hourDropdown);
        minutes = GetDropdownNumber(minuteDropdown);
    }

    private TimeSpan GetDuration()
    {
        return new TimeSpan(days, hours, minutes, 0);
    }

    private void ReadChecklist()
    {
        headset = headsetToggle.isOn;
        controlR = controlRToggle.isOn;
        controlL = controlLToggle.isOn;
        charger = chargerToggle.isOn;

        if (headset == true && controlR == true && controlL == true && charger == true)
        {
            complete = true;
        }
        else
        {
            complete = false;
        }
    }

    // ---------- Stage 4: review (optional) ----------
    
    public void PrepareReview()
    {
        ReadDuration();
        ReadChecklist();

        if (summaryText == null)
        {
            return;
        }

        DateTime until = DateTime.Now + GetDuration();
        string kitText = "none";

        if (HasKit() == true)
        {
            kitText = kitSelected.ToString();
        }

        summaryText.text =
            "Kit: " + kitText + "\n" +
            "Duration: " + days + " d " + hours + " h " + minutes + " min\n" +
            "Available again: " + until.ToString("dd/MM/yyyy HH:mm") + "\n" +
            "Headset: " + YesNo(headset) + "\n" +
            "Right controller: " + YesNo(controlR) + "\n" +
            "Left controller: " + YesNo(controlL) + "\n" +
            "Charger: " + YesNo(charger) + "\n" +
            "Complete: " + YesNo(complete);
    }

    private string YesNo(bool value)
    {
        if (value == true)
        {
            return "Yes";
        }
        return "No";
    }

    // ---------- Confirmation ----------

    public void ConfirmReservation()
    {
        if (kitManager == null)
        {
            Debug.LogError("kitSelector: kitManager is not assigned.");
            return;
        }

        if (dayDropdown == null || hourDropdown == null || minuteDropdown == null)
        {
            Debug.LogError("kitSelector: a duration dropdown is not assigned.");
            return;
        }

        if (headsetToggle == null || controlRToggle == null || controlLToggle == null || chargerToggle == null)
        {
            Debug.LogError("kitSelector: a checklist toggle is not assigned.");
            return;
        }

        if (HasKit() == false)
        {
            Debug.LogWarning("kitSelector: no kit selected.");
            return;
        }

        ReadDuration();
        ReadChecklist();

        if (GetDuration() <= TimeSpan.Zero)
        {
            Debug.LogWarning("kitSelector: the duration must be greater than zero.");
            return;
        }

        // End date: current date and time + the duration read from the dropdowns
        DateTime until = DateTime.Now + GetDuration();

        kitManager.ReserveKit(kitSelected, until, complete);

        ResetSelection();
    }

    // Leaves everything ready for the next reservation
    private void ResetSelection()
    {
        kitSelected = -1;
        days = 0;
        hours = 0;
        minutes = 0;
        headset = false;
        controlR = false;
        controlL = false;
        charger = false;
        complete = false;

        dayDropdown.value = 0;
        hourDropdown.value = 0;
        minuteDropdown.value = 0;

        headsetToggle.isOn = false;
        controlRToggle.isOn = false;
        controlLToggle.isOn = false;
        chargerToggle.isOn = false;

        if (summaryText != null)
        {
            summaryText.text = "";
        }
    }
}