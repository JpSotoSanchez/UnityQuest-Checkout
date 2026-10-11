using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KitSelector : MonoBehaviour
{
    [Header("References")]
    public KitManager kitManager;

    [Header("Stage 2: duration (existing dropdowns in the scene)")]
    [SerializeField] private TMP_Dropdown dayDropdown;
    [SerializeField] private TMP_Dropdown hourDropdown;
    [SerializeField] private TMP_Dropdown minuteDropdown;

    [Header("Stage 3: completeness checklist")]
    [SerializeField] private Toggle headsetToggle;
    [SerializeField] private Toggle controlRToggle;
    [SerializeField] private Toggle controlLToggle;
    [SerializeField] private Toggle chargerToggle;
    [SerializeField] private GameObject buttonComplete;
    [SerializeField] private GameObject buttonIncomplete;


    [Header("Stage 4: Order Check panel")]
    [SerializeField] private TMP_Text orderCheckKitText;
    [SerializeField] private Text summaryText;
    [SerializeField] private TMP_Dropdown reviewDayDropdown;
    [SerializeField] private TMP_Dropdown reviewHourDropdown;
    [SerializeField] private TMP_Dropdown reviewMinuteDropdown;
    [Header("Order Check panel")]

    [Header("Stage 5: Complete Order")]
    [SerializeField] private TMP_Text kitConfirmedSet;
    [SerializeField] private TMP_Text dayConfirmed;
    [SerializeField] private TMP_Text monthConfirmed;
    [SerializeField] private TMP_Text yearConfirmed;
    [SerializeField] private TMP_Text hourConfirmed;
    [SerializeField] private TMP_Text minuteConfirmed;




    [Header("Data")]
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
    private int GetDropdownNumber(TMP_Dropdown dropdown)
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

    public void ReadDuration()
    {
        days = GetDropdownNumber(dayDropdown);
        hours = GetDropdownNumber(hourDropdown);
        minutes = GetDropdownNumber(minuteDropdown);
    }

    public TimeSpan GetDuration()
    {
        return new TimeSpan(days, hours, minutes, 0);
    }

    public void ReadChecklist()
    {
        headset = headsetToggle.isOn;
        controlR = controlRToggle.isOn;
        controlL = controlLToggle.isOn;
        charger = chargerToggle.isOn;

        if (headset == true && controlR == true && controlL == true && charger == true)
        {
            complete = true;
            buttonComplete.gameObject.SetActive(true);
            buttonIncomplete.gameObject.SetActive(false);
        }
        else
        {
            complete = false;
            buttonComplete.gameObject.SetActive(false);
            buttonIncomplete.gameObject.SetActive(true);
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

    // Writes the selected kit into the Order Check panel text
    public void UpdateOrderCheckKit()
    {
        if (orderCheckKitText == null)
        {
            Debug.LogError("kitSelector: orderCheckKitText is not assigned.");
            return;
        }

        if (HasKit() == false)
        {
            orderCheckKitText.text = "No kit selected";
            return;
        }

        // Kits are stored from 0, but people usually count from 1.
        // Remove the "+ 1" if your kit numbers already start at 1.
        orderCheckKitText.text = "" + (kitSelected);
    }

    // Copies the selection of the stage 2 dropdowns into the review panel dropdowns
    public void CopyDurationToReview()
    {
        ReadDuration();

        SetDropdownNumber(reviewDayDropdown, days);
        SetDropdownNumber(reviewHourDropdown, hours);
        SetDropdownNumber(reviewMinuteDropdown, minutes);
    }

    // Selects the option whose number matches 'number' ("6" or "6 h" both match 6)
    private void SetDropdownNumber(TMP_Dropdown dropdown, int number)
    {
        if (dropdown == null)
        {
            Debug.LogError("kitSelector: a review dropdown is not assigned.");
            return;
        }

        for (int i = 0; i < dropdown.options.Count; i++)
        {
            if (ParseNumber(dropdown.options[i].text) == number)
            {
                // Does not fire onValueChanged, so no other listener reacts to this copy
                dropdown.SetValueWithoutNotify(i);
                dropdown.RefreshShownValue();
                return;
            }
        }

        Debug.LogWarning("kitSelector: no option with the value " + number + " in " + dropdown.name);
    }

    // Same parsing logic as GetDropdownNumber, but working on a plain string
    private int ParseNumber(string text)
    {
        string digits = "";
        for (int i = 0; i < text.Length; i++)
        {
            if (char.IsDigit(text[i]))
            {
                digits += text[i];
            }
            else if (digits.Length > 0)
            {
                break;
            }
        }

        int result;
        if (digits.Length > 0 && int.TryParse(digits, out result))
        {
            return result;
        }
        return -1;
    }

    private string YesNo(bool value)
    {
        if (value == true)
        {
            return "Yes";
        }
        return "No";
    }

    // Reads the review panel dropdowns (in case the user changed them there)
    public void ReadReviewDuration()
    {
        if (reviewDayDropdown == null || reviewHourDropdown == null || reviewMinuteDropdown == null)
        {
            Debug.LogError("kitSelector: a review dropdown is not assigned.");
            return;
        }

        days = GetDropdownNumber(reviewDayDropdown);
        hours = GetDropdownNumber(reviewHourDropdown);
        minutes = GetDropdownNumber(reviewMinuteDropdown);
    }
    
    // ---------- Stage 5: confirmed order ----------

    // Writes only the numbers, each one in its own text
    private void ShowConfirmation(int kit, DateTime until)
    {
        if (kitConfirmedSet == null || dayConfirmed == null || monthConfirmed == null ||
            yearConfirmed == null || hourConfirmed == null || minuteConfirmed == null)
        {
            Debug.LogError("kitSelector: a Stage 5 text is not assigned.");
            return;
        }

        kitConfirmedSet.text = kit.ToString();      // locker number
        dayConfirmed.text = until.ToString("dd");   // 01 - 31
        monthConfirmed.text = until.ToString("MM"); // 01 - 12
        yearConfirmed.text = until.ToString("yy");  // 26
        hourConfirmed.text = until.ToString("HH");  // 00 - 23
        minuteConfirmed.text = until.ToString("mm"); // 00 - 59
    }
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
        
        ShowConfirmation(kitSelected, until);   // before the reset

        ResetSelection();
    }

    // Leaves everything ready for the next reservation
    
    // Called by the "incomplete" button in the checklist panel
    public void ReportIncomplete()
    {
        if (kitManager == null)
        {
            Debug.LogError("kitSelector: kitManager is not assigned.");
            return;
        }

        if (HasKit() == false)
        {
            Debug.LogWarning("kitSelector: no kit selected.");
            return;
        }

        kitManager.SetIncomplete(kitSelected);

        ResetSelection();   // leaves everything ready for the next order
    }
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