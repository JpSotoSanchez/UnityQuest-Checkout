# Design Justification

This document justifies three interaction decisions of the kit checkout kiosk and compares the original Figma design with the Unity implementation.

**Unity version:** 6000.5.7f1

**Related documents:** [testing report](testing.md), [AI use](ai-use.md)

The Unity kiosk was built from the team's own Figma design. Most of the design was kept, with some changes caused by interactions and by how easy each section was to implement.

---

## 1. Interaction decisions

### Decision 1: A kit that is not available can still be reserved for after the current reservation

| | |
|---|---|
| **User problem** | When the user presses a kit that is not available, they would be left with no options. They also need to know when the kit is free again. |
| **HCI principle** | Visibility of System Status |
| **How the design addresses it** | Pressing a kit that is not available opens the NOT AVAILABLE panel, which shows the time when the kit is released and lets the user continue with the reservation (Next) or go back (Menu). If the user continues, the reservation starts when the previous one ends: the new end date is the date the kit already has plus the chosen duration. A free kit still gets the current time plus the duration. The flow is the same as in Figma, where this screen already offered a Next button to reserve for later. Showing the release time on this screen is new in Unity. |


| Figma | Unity |
|---|---|
| <img src="images/figmaNoAvailable.png" alt="Figma: NOT AVAILABLE panel" width="260"> | <img src="images/unityNoAvailable.png" alt="Unity: NOT AVAILABLE panel with the release time" width="260"> |

### Decision 2: In the duration panel the user only adjusts the duration, not the start of the reservation

| | |
|---|---|
| **User problem** | In the Figma design the user could adjust both the start and the duration of the reservation. This made reserving a kit more complicated than it needed to be. |
| **HCI principle** | Minimalist Design |
| **How the design addresses it** | The duration panel only asks for the duration. The start is not chosen by the user: it is the current time if the kit is free, or the time when the kit is released if it is occupied (see Decision 1). The user makes fewer choices to reserve a kit, and the implementation is simpler. The same change was made in Order Check, which no longer shows the pick-up date and time. |

| Figma | Unity |
|---|---|
| <img src="images/figmaDuration.png" alt="Figma: duration panel with pickup time and duration" width="260"> | <img src="images/unityDuration.png" alt="Unity: duration panel with only the duration" width="260"> |

### Decision 3: Large, interactive-looking, consistent buttons, and few buttons per panel

| | |
|---|---|
| **User problem** | Users struggled to tell which elements were clickable, small or inconsistent buttons were hard to hit accurately. |
| **HCI principle** | Affordance and consistency: the buttons are easy to see, select and handle. |
| **How the design addresses it** | The buttons are large and look interactive, there are only a few in each panel, and they are consistent with each other. For example, all the Next and Back buttons are in the same positions in every panel: Back is in the top-left corner and Next is a green button at the bottom. |

| Unity: duration | Unity: order check | Unity: not available |
|---|---|---|
| <img src="images/unityDuration.png" alt="Unity: duration panel, Back at the top left and Next at the bottom" width="220"> | <img src="images/unityOrderCheck.png" alt="Unity: order check panel, Back at the top left and Next at the bottom" width="220"> | <img src="images/unityNoAvailable.png" alt="Unity: not available panel, Back at the top left and Menu and Next at the bottom" width="220"> |

---

## 2. Figma vs Unity

### Implementation context

These parts of the implementation shaped some of the changes:

- **Data:** the kits are stored in three JSON files: one for scenario A, one for scenario B, and one that is updated constantly with the changes.
- **Managers in the scene:**
  - A panel manager (`CanvasManager`) that changes the Unity panels.
  - `KitManager`, which saves the data and finishes modifying it.
  - A combo manager, which reads the interaction and resets the data according to the command.
  - `KitSelector`, which stores the data while the user moves through the panels so it is saved at the end.

### Overview of all screens

- Figma: [figmaComplete](images/figmaComplete.pdf)
- Unity: [unityComplete](images/unityComplete.pdf)

### Screen comparison

| Screen | Figma | Unity |
|---|---|---|
| Kit selection (scenario A) | <img src="images/figmaSelectionA.png" alt="Figma: selection A" width="200"> | <img src="images/unitySelectionA.png" alt="Unity: selection A" width="200"> |
| Kit selection (scenario B) | <img src="images/figmaSelectionB.png" alt="Figma: selection B" width="200"> | <img src="images/unitySelectionB.png" alt="Unity: selection B" width="200"> |
| Duration | <img src="images/figmaDuration.png" alt="Figma: duration" width="200"> | <img src="images/unityDuration.png" alt="Unity: duration" width="200"> |
| Kit check | <img src="images/figmaKitCheck.png" alt="Figma: kit check" width="200"> | <img src="images/unityKitCheck.png" alt="Unity: kit check" width="200"> |
| Order check | <img src="images/figmaOrderCheck.png" alt="Figma: order check" width="200"> | <img src="images/unityOrderCheck.png" alt="Unity: order check" width="200"> |
| Confirmation | <img src="images/figmaConfirmation.png" alt="Figma: confirmation" width="200"> | <img src="images/unityConfirmation.png" alt="Unity: confirmation" width="200"> |
| Not available | <img src="images/figmaNoAvailable.png" alt="Figma: not available" width="200"> | <img src="images/unityNoAvailable.png" alt="Unity: not available" width="200"> |
| Charging | <img src="images/figmaCharging.png" alt="Figma: charging" width="200"> | <img src="images/unityCharging.png" alt="Unity: charging" width="200"> |
| Incomplete | <img src="images/figmaIncomplete.png" alt="Figma: incomplete" width="200"> | <img src="images/unityIncomplete.png" alt="Unity: incomplete" width="200"> |

### What changed and why

| # | Screen | What Changed | Reason |
|---|---|---|---|
| 1 | Kit selection | Numbers on the buttons for an image of the Oculus | So users know what they are reserving and gives a better design than just a number. |
| 2 | Duration | We deleted the Pick Up Time | Simpler decision, users just need to select for how much time they want it. |
| 3 | Kit check | Until all checkboxes are selected, the button is Incomplete, after all of them are selected it becomes a Next button | Poka-Yoke |
| 4 | Order check | We deleted the Pick Up Time | Simpler decision, users just need to check for how much time they will borrow the kit. |
| 4 | Charging | Deleted the Next button | As there is no Pick Up Time, if the kit is not available at the moment user needs to wait till it's available. |