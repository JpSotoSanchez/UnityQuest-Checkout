# Testing Report

## 1. Tested build

| Field | Value |
|---|---|
| Project | UnityQuest-Checkout (kit rental app for tablets) |
| Branch / commit | `soto` / [`18b461d`](https://github.com/JpSotoSanchez/UnityQuest-Checkout/commit/18b461d60a02eecd29a49634f95ec4bded6c7eba) ("Added images and started the desing document") |
| Release / tag | `<fill in, if any>` |
| Unity version | 6000.5.7f1 |
| Device | Unity Editor, Play mode with the play scene (Game view resolution: `1536x2048`) |
| Test date(s) | Usability session: 11/10/2026, about 7:27 pm. Functional cases: `7:29pm` `<fill in: date>` |
| Tester(s) | `José Pablo Soto Sánchez` (ran the functional test cases) |

**Test data.** Unless a case says otherwise, every case starts from this `Kits.json`
(stored in `Application.persistentDataPath`). Delete the file before a case to start from the default.

| Kit | State (`available`) | Date | Purpose |
|---|---|---|---|
| 1, 3, 6 | 2 (not available) | 24/10/2026 18:00 | Rented, not expired |
| 5 | 2 (not available) | 09/10/2026 18:00 | Expired yesterday, `complete: true` |
| 2 | 1 (charging) | 08/10/2026 18:00 | Charging |
| 4 | 3 (incomplete) | 08/10/2026 18:00 | Incomplete, `complete: false` |

State legend: 0 = available, 1 = charging, 2 = not available, 3 = incomplete.

> Note: `KitManager.CheckExpired()` moves any state-2 kit with a past date to 1 (complete) or 3 (incomplete)
> when the app loads. Kit 5 is therefore expected to show as **charging** at startup.

---

## 2. Functional test cases

Six functional cases: checkout, no availability, invalid kit, editing choices, saved data after restart and scenario reset.
These cases are separate from the two usability scenarios of section 3.

Result fields: **Actual**, **Pass/Fail** and **Evidence** are filled in while testing.
Evidence = screenshots saved in `docs/images` (paths below are relative to `docs/`).

### TC-01 Checkout (happy path)

| | |
|---|---|
| **Setup** | At least one kit in state 0. Example: edit `Kits.json` so kit 1 has `available: 0`. |
| **Steps** | 1. Open the app. 2. Tap an available kit. 3. Choose a duration (e.g. 0 d, 1 h, 30 min). 4. Tap Next. 5. Check all four items (headset, right controller, left controller, charger). 6. Tap Next to Order Check. 7. Confirm. |
| **Expected** | Order Check shows the chosen kit and duration. The confirmation screen shows the locker number and the return date/time (current time + duration) in separate fields. `Kits.json` has the kit at `available: 2`, the correct date and `complete: true`. The kit button shows "not available". |
| **Actual** | The test was run through to the end: the user reached the reservation completed screen, and the reservation was confirmed correctly. |
| **Pass/Fail** | **Pass.** The criterion is that the user can reach a correct reservation, and it was completed. |
| **Evidence** | `images/unityComplete.pdf` (reservation completed screen) |

### TC-02 No availability

This case was updated to match the decision that a kit that is not available can still be reserved (see [design.md](design.md), Decision 1). The first version expected that no kit could be selected.

| | |
|---|---|
| **Setup** | Default test data above, with no kit in state 0 (kits in states 1, 2 or 3). |
| **Steps** | 1. Open the app. 2. Tap a kit that is not available (state 2). 3. Read the release time on the NOT AVAILABLE panel and tap Next. 4. Choose a duration, go through the panels and confirm. (Charging and incomplete kits are covered in TC-03.) |
| **Expected** | The NOT AVAILABLE panel shows the date and time when the kit is released and lets the user continue to the duration panel, so the path to a possible reservation is not cut. After confirming, the end date is the date the kit already had plus the chosen duration; the kit stays in state 2 and its `complete` value does not change. |
| **Actual** | Selecting a kit in the not available state shows the NOT AVAILABLE panel, and the user can follow the complete scenario until reaching a reservation. |
| **Pass/Fail** | **Pass.** The criterion is that selecting one of these kits shows the NOT AVAILABLE panel and the user can continue the complete scenario to a reservation. |
| **Evidence** | `images/unityNoAvailable.png` |

### TC-03 Invalid kit / invalid input

| | |
|---|---|
| **Setup** | Kit 1 in state 0. |
| **Steps** | A. Press Confirm without selecting a kit. B. Select a kit and set duration 0 d / 0 h / 0 min, then Confirm. C. Try to reserve a kit in state 1 (charging) or state 3 (incomplete). |
| **Expected** | A: Confirm cannot be pressed without a kit. B: a duration of 0 d / 0 h / 0 min is accepted and the reservation ends immediately, so the kit has to be returned at that moment. C: the app shows the Charging or Incomplete panel, which only offers going back to the menu, and does not allow a reservation. (Case C used to be "a kit that is not in state 0"; kits in state 2 can now be reserved, see TC-02.) |
| **Actual** | This is controlled. A: Confirm cannot be pressed without selecting a kit, because selecting a kit is what takes the user to the next panel. B: with a duration of 0 d / 0 h / 0 min the reservation is confirmed and, when it ends, the kit simply has to be returned at that moment. C: when the user tries to reserve a kit that is charging or incomplete, the app shows the respective panel, which only gives the option to go back to the menu. |
| **Pass/Fail** | **Pass.** |
| **Evidence** | `images/unityDuration.png`, `images/unityCharging.png`, `images/unityIncomplete.png` |

### TC-04 Editing choices

| | |
|---|---|
| **Setup** | Kit 1 in state 0. |
| **Steps** | 1. Select kit 1, set duration 1 d / 2 h / 10 min, go to the checklist. 2. Check all items, go to Order Check. 3. Change the duration in the Order Check dropdowns (e.g. to 0 d / 3 h / 0 min). 4. Confirm. 5. Repeat, but go back and uncheck one checklist item. |
| **Expected** | The summary and the saved date use the **edited** values, not the original ones. Unchecking an item shows the incomplete button and the kit is stored as incomplete (state 3, `complete: false`). |
| **Actual** | In the Order Check panel the user can see the order and change the duration, and the data updates with the change. The result was correct. There was a problem before (the edited duration was ignored) but it was fixed; see Problem 2. |
| **Pass/Fail** | **Pass** (after the fix of Problem 2). |
| **Evidence** | `images/unityOrderCheck.png` `<fill in: capture showing the edited value>` |

### TC-05 Saved data after restart

| | |
|---|---|
| **Setup** | Kit 1 in state 0. |
| **Steps** | 1. Complete a checkout for kit 1. 2. Note the return date shown. 3. Close the app completely. 4. Reopen it. 5. Repeat with a kit reported as incomplete. |
| **Expected** | Kit 1 is still "not available" with the same return date. The incomplete kit is still in state 3. `Kits.json` matches what the UI shows. |
| **Actual** | The information is saved after restarting. This is because of how the JSON works and how the app reads it. |
| **Pass/Fail** | **Pass.** The data is saved. |
| **Evidence** | `<fill in: copy of Kits.json before/after, or a capture>` |

### TC-06 Scenario reset

| | |
|---|---|
| **Setup** | Several kits modified by earlier cases. |
| **Steps** | 1. Enter the reset combo (handled by the `comboTrial` script): up, up, down, down, Q, A (`uuddqa`) resets scenario A, and up, up, up, down, Q, A (`uuudqa`) resets scenario B. 2. Observe all kit buttons. 3. Close and reopen the app. |
| **Expected** | `Kits.json` is overwritten with the JSON of the chosen scenario (scenario A or scenario B), every button shows the original state of that scenario, and the original state is still there after reopening. |
| **Actual** | The reset works. The `comboTrial` script reads the combo: `uuddqa` calls `ResetPrototypeA()` and `uuudqa` calls `ResetPrototypeB()`, restarting the data according to the scenario. |
| **Pass/Fail** | **Pass.** |
| **Evidence** | `<fill in>` |

### Summary

| Case | Result |
|---|---|
| TC-01 Checkout | Pass |
| TC-02 No availability | Pass |
| TC-03 Invalid kit | Pass |
| TC-04 Editing choices | Pass |
| TC-05 Saved data after restart | Pass |
| TC-06 Scenario reset | Pass |

---

## 3. Usability testing with users outside the team

**Method.** Three people who are not part of the team each attempt both scenarios with no click-by-click guidance.
The facilitator only reads the task aloud and does not point at the screen. Participants are identified
only by anonymous IDs (P1, P2, P3); no names are recorded.

**Session log**

| Participant | Date and time | Build | Environment |
|---|---|---|---|
| P1 | 11/10/2026, about 7:27 pm | `18b461d` | Unity Editor, Play mode, play scene |
| P2 | `<fill in>` | `<fill in>` | `<fill in>` |
| P3 | `<fill in>` | `<fill in>` | `<fill in>` |

So far only P1 has been tested. P2 and P3 are pending.

**Scenarios**

Only two scenarios are used with the participants. They are not extra test cases.

| | Scenario A | Scenario B |
|---|---|---|
| **Task read aloud** | Reserve a Quest kit for 90 minutes. Before reserving, check that the kit is complete. Continue until the reservation is complete. | Try to reserve a kit that is not available. |
| **Success criterion** | The participant completes a successful reservation of an available kit for 90 minutes. | The participant selects a kit that is not available and can continue along the path to a possible reservation, so the path is never cut off. |
| **Evidence (screen to reach)** | `images/unityComplete.pdf` (reservation completed) | `images/unityNoAvailable.png` (NOT AVAILABLE panel) |

The captures of each participant reaching these screens are `<fill in>`, because the other participants have not been tested yet.

**Results**

| Participant | Scenario | Completed (Y/N) | Time (mm:ss) | Errors | Help needed | Observations |
|---|---|---|---|---|---|---|
| P1 | A | Y | 01:10 | 1. The participant pressed "Incomplete" by accident, so had to go back to the start and repeat the scenario to reserve the kit as complete. | Yes, help with the reservation. | Hesitated a lot about what to do in each panel. Did not know how to move through the flow. Only simple task instructions were given. The time (01:10) includes the repeated attempt. |
| P1 | B | Y | 00:34 | None | No | Selected a kit that was not available and followed the path to the end, also reserving it for 90 minutes. Faster than scenario A because the participant had already seen how to select a kit. |
| P2 | A | | | | | |
| P2 | B | | | | | |
| P3 | A | | | | | |
| P3 | B | | | | | |

**Totals:** completion rate 2/2 recorded so far (final: `<x/6>`), average time A `<>`, average time B `<>`, total errors `<>`, help requests `<>`. (Only P1 recorded so far: A 01:10, B 00:34, 1 error and 1 help request.)

**Patterns observed:** The participant got stuck when the panels did not give clear instructions about what to do, and pressed Incomplete by accident in scenario A. The proposed changes for the first one are in Problem 1 (section 4). `<fill in after P2 and P3>`

---

## 4. Problems found, fixes and retests

At least two problems must be fixed and retested. If fewer than two appear in sections 2 and 3, expand
testing (extra devices, longer durations, rapid taps, restart in the middle of a flow) and document them here.

### Problem 1: The panels do not tell the user what to do

| | |
|---|---|
| **Found in** | P1, scenarios A and B (usability session of 11/10/2026, build `18b461d`) |
| **Description** | The participant hesitated a lot about what to do in each panel. Scenario B (00:34) was faster than scenario A (01:10) because the participant had already seen how to select a kit. In scenario A the participant did not know how to move through the flow. The participant was only given simple task instructions. |
| **Before (evidence)** | `docs/evidence/p1-before.png` |
| **Fix** | Status: planned, not implemented yet. Planned changes: <br>1. Change the title "Duration" to "Select a duration", and do the same in every panel so it is clear that the user has to choose something. In the kit check, add a text saying the user has to confirm that the items are complete in order to continue. <br>2. Add an explicit instruction in every panel about what to tap, for example in the review panel: "Check that the kit is complete". <br>3. Show clearly the order of the steps (what is done first and what follows). <br>4. Review that the whole flow is in order and consistent between panels. <br>`<fill in: file and commit when done>` |
| **After (evidence)** | `docs/evidence/p1-after.png` |
| **Retest result** | `<Pass/Fail>` |

### Problem 2: The duration edited in Order Check was ignored

| | |
|---|---|
| **Found in** | TC-04 (editing choices), found by the team while testing |
| **Description** | When the user changed the duration in the Order Check panel, the change was ignored and the reservation was saved with the duration chosen in the earlier duration panel. `ConfirmReservation()` in `KitSelector.cs` read the duration with `ReadDuration()` instead of `ReadReviewDuration()`. |
| **Before (evidence)** | `<fill in: capture or note showing the ignored edit>` |
| **Fix** | `ConfirmReservation()` now uses `ReadReviewDuration()`, so the values edited in Order Check are the ones saved. File: `KitSelector.cs`. Commit: `<fill in>` |
| **After (evidence)** | `<fill in: capture showing the edited duration in the saved date>` |
| **Retest result** | Pass: the edit is respected and the data updates. |

### Candidate problems to verify

These were spotted while reading the code or during the sessions. Keep only the ones that actually reproduce in testing.

- **Accidental "Incomplete" press (seen in P1, scenario A).** The participant pressed Incomplete by accident and had to go back to the start. There is no confirmation or undo for this button, and the kit is reported as incomplete. Relates to TC-04.
- **Reservation does not check kit state.** `ReserveKit()` sets state 2 without checking the kit is in state 0.
  Relates to TC-02 and TC-03.
- **Zero duration is accepted.** A duration of 0 d / 0 h / 0 min can be confirmed, so the kit has to be returned
  immediately. Decide if this is acceptable or should be blocked. Relates to TC-03.
- **Incomplete reset.** `ResetSelection()` does not clear the review dropdowns or the Order Check text, so the
  next order may show stale values. Relates to TC-04.
- **Locker number offset.** The kit number shown is `kitSelected` as-is; if `KitSelected(n)` is 0-based and
  `kitNumber` in the JSON is 1-based, the locker number shown could be off by one. Relates to TC-01.

---

## 5. Unresolved issues and limitations

| ID | Description | Severity | Reason not fixed / planned action |
|---|---|---|---|
| | | | |

**Limitations of this testing**

- The tests were run in the Unity Editor (Play mode, Game view), not on a physical tablet.
- Only P1 has been tested so far. P2 and P3 are pending.
- `<fill in: other limitations>`

## 6. Conclusion

`<2-4 sentences: what passed, what was fixed, what remains, confidence in the build.>`