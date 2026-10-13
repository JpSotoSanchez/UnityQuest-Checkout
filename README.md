# UnityQuest-Checkout

A kiosk app for tablets, built in Unity, where a user reserves a VR kit (a Meta Quest headset, its two controllers and its charger). Project of the Human Computer Interaction course, Universidad Panamericana.

## Deliveries
### Github Release
Link: https://github.com/JpSotoSanchez/UnityQuest-Checkout/releases/tag/complete
### Demo Video
Link: [docs/JaimeRinconJoseSoto_Video.mp4](docs/JaimeRinconJoseSoto_Video.mp4)
### Design document
Link: https://github.com/JpSotoSanchez/UnityQuest-Checkout/blob/main/docs/design.md
### Testing Report
Link: https://github.com/JpSotoSanchez/UnityQuest-Checkout/blob/main/docs/testing.md
### AI logs
Link: https://github.com/JpSotoSanchez/UnityQuest-Checkout/blob/main/docs/ai-use.md
### Project Build
Link: https://github.com/JpSotoSanchez/UnityQuest-Checkout/tree/main/ProjectBuild

## Project Summary

UnityQuest-Checkout lets a user choose one of six kits, choose how long to keep it, check that the kit is complete and confirm the reservation. The user is never left without options: if the kit is not available, the app shows when it will be free and lets the user reserve it for after the current reservation.

**Main flow (kit available)**

1. **Kit selection:** six kit buttons, each one coloured by the state of its kit.
2. **Duration:** the user only chooses the duration (days, hours and minutes). The start of the reservation is set automatically.
3. **Kit check:** the user checks the four items of the kit (headset, right controller, left controller and charger). If all are checked the kit is saved as complete. If something is missing, the user reports the kit as incomplete.
4. **Order check:** the user sees the order and can still change the duration.
5. **Confirmation:** the app shows the locker number and the date and time when the kit must be returned.

**What the app does for each kit state**

| State | What the user sees |
|---|---|
| Available | The main flow above. The reservation ends at the current time plus the duration. |
| Not available | A NOT AVAILABLE panel with the date and time when the kit is released. The user can go back to the menu or continue the reservation. If the user continues, the reservation ends at the date the kit already had plus the chosen duration. |
| Charging | A panel that tells the user to come back in 2 hours (the usual charging time of a Quest 3) and only offers going back to the menu. |
| Incomplete | A panel that asks the user to return the kit as it is, and only offers going back to the menu. |

**How it works**

- **Data:** the kits are stored in a JSON file in `Application.persistentDataPath/Kits.json`, so the data is kept after closing the app. The project has three JSON files: `Kits.json` (copied the first time the app runs, and updated with every change), `KitsOgA.json` (scenario A) and `KitsOgB.json` (scenario B).
- **Expired rentals:** when the date of a not available kit has passed, the kit changes automatically to charging (if it was complete) or incomplete. This is checked when the data is loaded and every 60 seconds.
- **Managers in the scene:**
  - `CanvasManager`: changes between the Unity panels.
  - `KitManager`: reads, saves and modifies the kit data.
  - `KitSelector`: keeps the data of the order while the user moves through the panels, and saves it at the end.
  - `KitButton`: paints each kit button and opens the correct panel when it is pressed.
  - Combo manager (`comboTrial`): reads the key combos and resets the data of a scenario.

## Original Figma Link
The original prototype in figma can be seen in this link: https://www.figma.com/design/eu0bAdrbu6nYUw9Dp2NCa6/Parcial1?t=4t2JQHFGgsVwUEzA-0

## Team Contributions

| Teammate | Contribution |
|---|---|
| José Pablo Soto Sánchez (UPSoto) | Most of the code. App logic and data: reading and saving the kits in JSON, `KitManager`, `KitSelector`, `KitButton`, rental dates and kit states, reserving a kit that is not available, and the scenario reset. Functional test cases and testing report (`docs/testing.md`), design justification (`docs/design.md`) and his entries of the AI log (`docs/ai-use.md`). |
| Jaime Rincón Burboa | Most of the interface and how the app looks. Interface in Unity: rounded buttons with 9-Slice, larger dropdown options and faster scroll in the duration panel, checkboxes for the Kit Check panel, and the custom fonts (CreatoDisplay Regular and Medium) for TextMeshPro. His entries of the AI log (`docs/ai-use.md`). |
| Both | The work was split between code and interface, but both teammates took part in everything and kept track of each other's work. Only Unity was worked on in this project; the Figma prototype was the original design and was not worked on again. |

## Project parameters
### Unity version
Unity **6000.5.7f1**.

### setup instructions
1. Install Unity Hub and the Unity editor version 6000.5.7f1.
2. Clone the repository:
   ```
   git clone https://github.com/JpSotoSanchez/UnityQuest-Checkout.git
   ```
   The final version is in the `main` branch (final commit: FILL IN). Development was done in the branch `soto`.
3. In Unity Hub, choose **Add > Add project from disk** and select the cloned folder. Open it with version 6000.5.7f1.
4. Open the scene `App`.
5. In the Game view, set the resolution to `1536x2048` (portrait), which is the one used for the tests.
6. Check the `KitManager` component of the scene: `Default Json` is `Kits.json`, `Og Json A` is `KitsOgA.json`, `Og Json B` is `KitsOgB.json`, and the list of buttons has the six kit buttons. Each `KitButton` needs its `Kit Selector` assigned.

### controls
The app is used by touch on a tablet, or with the mouse in the Unity editor.

| Control | What it does |
|---|---|
| Kit button | Selects the kit and opens the panel that matches its state. |
| Duration dropdowns (Day, Hour, Min) | Choose how long the kit will be kept. |
| Checkboxes of the Kit Check panel | Mark each item of the kit as present. |
| Next | Goes to the next panel and saves the data of the current one. |
| Back | Returns to the previous panel. |
| Menu / Home | Returns to the kit selection panel. |
| Incomplete | Reports the kit as incomplete (it appears when an item is not checked). |
| Key combo `Up Up Down Down Q A` | Resets the data to scenario A. For testing. Needs a keyboard. |
| Key combo `Up Up Up Down Q A` | Resets the data to scenario B. For testing. Needs a keyboard. |

## Known Limitations

- **Language:** the labels and instructions are only in English. One participant had trouble reading them. The code for a translation exists but was not implemented because of time.
- **Incomplete button:** it has no confirmation or undo. A participant pressed it by accident and had to start again.
- **Zero duration:** a duration of 0 days, 0 hours and 0 minutes is accepted, so the kit has to be returned immediately.
- **Found when reading the code, not reproduced in the tests:**
  - `ReserveKit()` does not check that the kit is available before reserving it.
  - After an order, the Order Check dropdowns and text are not cleared, so the next order may show old values.
  - The locker number shown could be off by one if the numbering of the buttons and of the JSON differ.
- **One end date per kit:** each kit stores a single end date, so if a kit is reserved twice in a row, only the end of the last reservation is saved.
- **Local data:** the data is a JSON file on the device. There is no server, and two devices do not share the data.
- **Saved data and the project JSON:** `Kits.json` of the project is copied only the first time the app runs. After that, changing the file in the project does not change the saved data until the saved file is deleted (see Reset Data).
- **Dates of the scenarios:** the rented kits of both scenarios end on 24/10/2026 at 18:00. After that date they change to charging by themselves, so the scenarios no longer show occupied kits unless the dates in `KitsOgA.json` and `KitsOgB.json` are updated.
- **Testing:** the tests were run in the Unity editor and not on a physical tablet, with three participants. See [the testing report](docs/testing.md).

## HowTo
### Run
**In the Unity editor**

1. Open the project and the `App` scene (see setup instructions).
2. Press **Play**. The first time, the app copies `Kits.json` to `Application.persistentDataPath/Kits.json` and uses that file from then on.

**From the build**

The build is in the `build` folder and is made for a Windows laptop. The .exe file's name is UnityQuest-Checkout.exe. To reset the data in the build, use the key combos of the Reset Data section, which are up-up-down-down-q-a for Scenario A and up-up-up-down-q-a for Scenario B. 

### Reset Data
Choose one:

1. **With the key combo (while the app is running):**
   - `Up, Up, Down, Down, Q, A` resets the data to scenario A (`KitsOgA.json`).
   - `Up, Up, Up, Down, Q, A` resets the data to scenario B (`KitsOgB.json`).
2. **Deleting the saved file (in the Unity editor):** right-click the header of the `KitManager` component and choose **Delete saved Kits.json**. The next time the app starts, it copies the project's `Kits.json` again. The option **Print saved Kits.json path** writes the location of the saved file in the console.
3. **By hand:** close the app and delete `Kits.json` from `Application.persistentDataPath`. The app creates it again from the project's `Kits.json` when it starts.

### Test scenarios
The two scenarios are the ones used in the usability tests. Reset the data first, then follow the steps.

**Scenario A: reserve an available kit**

1. Reset to scenario A (`Up, Up, Down, Down, Q, A`). Kit 2 is the only available kit (green).
2. Task: reserve a Quest kit for 90 minutes. Before reserving, check that the kit is complete.
3. Steps: tap the available kit, set 0 days, 1 hour and 30 minutes, press Next, check the four items, press Next, review the order and confirm.
4. Expected result: the confirmation shows the locker number and the return date and time (the current time plus 90 minutes), and the kit changes to not available.

**Scenario B: try to reserve a kit that is not available**

1. Reset to scenario B (`Up, Up, Up, Down, Q, A`). No kit is available. Kits 1, 3 and 6 are not available, kit 2 is charging and kit 4 is incomplete.
2. Task: try to reserve a kit that is not available.
3. Steps: tap a kit that is not available. The NOT AVAILABLE panel shows when the kit is released. Press Next, choose a duration, go through the panels and confirm.
4. Expected result: the user can continue until the reservation is complete. The return date is the date the kit already had plus the chosen duration, and the kit stays not available.

To also see the other panels in scenario B: tap the charging kit (kit 2) to see the Charging panel, and the incomplete kit (kit 4) to see the Incomplete panel. Neither one allows a reservation.

The functional test cases and the results of the three participants are in [the testing report](docs/testing.md).