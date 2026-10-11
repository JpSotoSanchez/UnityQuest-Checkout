# Testing Report

## 1. Tested build

| Field | Value |
|---|---|
| Project | UnityQuest-Checkout (kit rental app for tablets) |
| Branch / commit | `soto` / `<run: git rev-parse --short HEAD>` |
| Unity version | `<fill in>` |
| Device | `<tablet model, or Unity Editor + Game view resolution>` |
| Test date(s) | `<fill in>` |
| Tester(s) | `<team members>` |

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

Result fields: **Actual**, **Pass/Fail** and **Evidence** are filled in while testing.
Evidence = screenshot or short video saved under `docs/evidence/`.

### TC-01 Checkout (happy path)

| | |
|---|---|
| **Setup** | At least one kit in state 0. Example: edit `Kits.json` so kit 1 has `available: 0`. |
| **Steps** | 1. Open the app. 2. Tap an available kit. 3. Choose a duration (e.g. 0 d, 1 h, 30 min). 4. Tap Next. 5. Check all four items (headset, right controller, left controller, charger). 6. Tap Next to Order Check. 7. Confirm. |
| **Expected** | Order Check shows the chosen kit and duration. The confirmation screen shows the locker number and the return date/time (current time + duration) in separate fields. `Kits.json` has the kit at `available: 2`, the correct date and `complete: true`. The kit button shows "not available". |
| **Actual** | `<fill in>` |
| **Pass/Fail** | `<fill in>` |
| **Evidence** | `docs/evidence/tc01-*.png` |

### TC-02 No availability

| | |
|---|---|
| **Setup** | Default test data above, with no kit in state 0 (all kits in 1, 2 or 3). |
| **Steps** | 1. Open the app. 2. Try to tap every kit. 3. Try to continue to the duration panel. |
| **Expected** | No kit can be selected. The app does not advance and does not change any kit in `Kits.json`. |
| **Actual** | `<fill in>` |
| **Pass/Fail** | `<fill in>` |
| **Evidence** | `docs/evidence/tc02-*.png` |

### TC-03 Invalid kit / invalid input

| | |
|---|---|
| **Setup** | Kit 1 in state 0. |
| **Steps** | A. Press Confirm without selecting a kit. B. Select a kit and set duration 0 d / 0 h / 0 min, then Confirm. C. Select a kit that is not in state 0 (if the UI allows it) and Confirm. |
| **Expected** | A and B: no reservation is created, nothing is saved, and the user sees a clear message or stays on the panel (the code currently logs a warning in the console only). C: the reservation is rejected. |
| **Actual** | `<fill in>` |
| **Pass/Fail** | `<fill in>` |
| **Evidence** | `docs/evidence/tc03-*.png`, console log |

### TC-04 Editing choices

| | |
|---|---|
| **Setup** | Kit 1 in state 0. |
| **Steps** | 1. Select kit 1, set duration 1 d / 2 h / 10 min, go to the checklist. 2. Check all items, go to Order Check. 3. Change the duration in the Order Check dropdowns (e.g. to 0 d / 3 h / 0 min). 4. Confirm. 5. Repeat, but go back and uncheck one checklist item. |
| **Expected** | The summary and the saved date use the **edited** values, not the original ones. Unchecking an item shows the incomplete button and the kit is stored as incomplete (state 3, `complete: false`). |
| **Actual** | `<fill in>` |
| **Pass/Fail** | `<fill in>` |
| **Evidence** | `docs/evidence/tc04-*.png` |

### TC-05 Saved data after restart

| | |
|---|---|
| **Setup** | Kit 1 in state 0. |
| **Steps** | 1. Complete a checkout for kit 1. 2. Note the return date shown. 3. Close the app completely. 4. Reopen it. 5. Repeat with a kit reported as incomplete. |
| **Expected** | Kit 1 is still "not available" with the same return date. The incomplete kit is still in state 3. `Kits.json` matches what the UI shows. |
| **Actual** | `<fill in>` |
| **Pass/Fail** | `<fill in>` |
| **Evidence** | `docs/evidence/tc05-*.png`, copy of `Kits.json` before/after |

### TC-06 Scenario reset

| | |
|---|---|
| **Setup** | Several kits modified by earlier cases. |
| **Steps** | 1. Trigger the reset (`KitManager.ResetKits()`, from the reset button). 2. Observe all kit buttons. 3. Close and reopen the app. |
| **Expected** | `Kits.json` is overwritten with `ogJson`, every button shows the original state, and the original state is still there after reopening. |
| **Actual** | `<fill in>` |
| **Pass/Fail** | `<fill in>` |
| **Evidence** | `docs/evidence/tc06-*.png` |

### Summary

| Case | Result |
|---|---|
| TC-01 Checkout | `<Pass/Fail>` |
| TC-02 No availability | `<Pass/Fail>` |
| TC-03 Invalid kit | `<Pass/Fail>` |
| TC-04 Editing choices | `<Pass/Fail>` |
| TC-05 Saved data after restart | `<Pass/Fail>` |
| TC-06 Scenario reset | `<Pass/Fail>` |

---

## 3. Usability testing with users outside the team

**Method.** Three people who are not part of the team each attempt both scenarios with no click guidance.
The facilitator only reads the task aloud and does not point at the screen. Participants are identified
only by anonymous IDs (P1, P2, P3); no names are recorded.

**Scenarios**
- **Scenario A:** `<e.g. "Reserve a kit for 2 hours; the kit is complete.">`
- **Scenario B:** `<e.g. "Reserve a kit and report that the controller is missing.">`

**Results**

| Participant | Scenario | Completed (Y/N) | Time (mm:ss) | Errors | Help needed | Observations |
|---|---|---|---|---|---|---|
| P1 | A | | | | | |
| P1 | B | | | | | |
| P2 | A | | | | | |
| P2 | B | | | | | |
| P3 | A | | | | | |
| P3 | B | | | | | |

**Totals:** completion rate `<x/6>`, average time A `<>`, average time B `<>`, total errors `<>`, help requests `<>`.

**Patterns observed:** `<fill in after the sessions>`

---

## 4. Problems found, fixes and retests

At least two problems must be fixed and retested. If fewer than two appear in sections 2 and 3, expand
testing (extra devices, longer durations, rapid taps, restart in the middle of a flow) and document them here.

### Problem 1: `<title>`

| | |
|---|---|
| **Found in** | `<TC-xx or participant ID>` |
| **Description** | `<what happened>` |
| **Before (evidence)** | `docs/evidence/p1-before.png` |
| **Fix** | `<what changed, file and commit>` |
| **After (evidence)** | `docs/evidence/p1-after.png` |
| **Retest result** | `<Pass/Fail>` |

### Problem 2: `<title>`

| | |
|---|---|
| **Found in** | `<TC-xx or participant ID>` |
| **Description** | `<what happened>` |
| **Before (evidence)** | `docs/evidence/p2-before.png` |
| **Fix** | `<what changed, file and commit>` |
| **After (evidence)** | `docs/evidence/p2-after.png` |
| **Retest result** | `<Pass/Fail>` |

### Candidate problems to verify

These were spotted while reading the code. Keep only the ones that actually reproduce in testing.

- **Edited duration ignored.** `ConfirmReservation()` calls `ReadDuration()` (stage 2 dropdowns) instead of
  `ReadReviewDuration()`, so changes made in Order Check do not reach the saved date. Relates to TC-04.
- **Reservation does not check kit state.** `ReserveKit()` sets state 2 without checking the kit is in state 0.
  Relates to TC-02 and TC-03.
- **Incomplete (no kit / zero duration) feedback is console-only.** `Debug.LogWarning` is invisible to the user.
  Relates to TC-03.
- **Incomplete reset.** `ResetSelection()` does not clear the review dropdowns or the Order Check text, so the
  next order may show stale values. Relates to TC-04.
- **Locker number offset.** The kit number shown is `kitSelected` as-is; if `KitSelected(n)` is 0-based and
  `kitNumber` in the JSON is 1-based, the locker number shown could be off by one. Relates to TC-01.

---

## 5. Unresolved issues

| ID | Description | Severity | Reason not fixed / planned action |
|---|---|---|---|
| | | | |

## 6. Conclusion

`<2-4 sentences: what passed, what was fixed, what remains, confidence in the build.>`