# Testing Report

## 1. Tested build

| Field | Value |
|---|---|
| Project | UnityQuest-Checkout (kit rental app for tablets) |
| Branch / commit | `soto` / [`18b461d`](https://github.com/JpSotoSanchez/UnityQuest-Checkout/commit/18b461d60a02eecd29a49634f95ec4bded6c7eba) ("Added images and started the desing document") |
| Unity version | 6000.5.7f1 |
| Device | Unity Editor, Play mode with the play scene (Game view resolution: `1536x2048`) |
| Test date(s) | Usability session: 11/10/2026, about 7:27 pm. Functional cases: `7:29pm` |
| Tester(s) | `José Pablo Soto Sánchez` |


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

### Scenario A: Complete Checkout 

| | |
|---|---|
| **Setup** | At least one kit in state 0. Example: edit `Kits.json` so kit 1 has `available: 0`. |
| **Steps** | 1. Open the app. 2. Tap an available kit. 3. Choose a duration (e.g. 0 d, 1 h, 30 min). 4. Tap Next. 5. Check all four items (headset, right controller, left controller, charger). 6. Tap Next to Order Check. 7. Confirm. |
| **Expected** | Order Check shows the chosen kit and duration. The confirmation screen shows the locker number and the return date/time (current time + duration) in separate fields. `Kits.json` has the kit at `available: 2`, the correct date and `complete: true`. The kit button shows "not available". |
| **Actual** | `<fill in>` |
| **Pass/Fail** | `<fill in>` |
| **Evidence** | `docs/evidence/tc01-*.png` |

### Scenario B:  No availability

| | |
|---|---|
| **Setup** | Default test data above, with no kit in state 0 (all kits in 1, 2 or 3). |
| **Steps** | 1. Open the app. 2. Try to tap a no available kit. 3. Try to continue to the duration panel. |
| **Expected** | To follow with the reservation that will statr when the other kit is available. |
| **Actual** | `<fill in>` |
| **Pass/Fail** | `<fill in>` |
| **Evidence** | `docs/evidence/tc02-*.png` |



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
| Scenario A: Complete Checkout  | `<Pass/Fail>` |
| Scenario B:  No availability | `<Pass/Fail>` |

---

## 3. Usability testing with users outside the team

**Method.** Three people who are not part of the team each attempt both scenarios with no click guidance.
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
- **Scenario A:** Reserve a Quest kit for 90 minutes, then check that the kit is complete.
- **Scenario B:** Select a kit and check what is available. `<fill in: exact wording of the task>`

**Results**

| Participant | Scenario | Completed (Y/N) | Time (mm:ss) | Errors | Help needed | Observations |
|---|---|---|---|---|---|---|
| P1 | A | Y | 01:10 | `<fill in>` | `<fill in>` | Hesitated a lot about what to do in each panel. Did not know how to move through the flow. Only simple task instructions were given. |
| P1 | B | Y | 00:34 | `<fill in>` | `<fill in>` | Faster than scenario A because the participant had already seen how to select a kit. |
| P2 | A | | | | | |
| P2 | B | | | | | |
| P3 | A | | | | | |
| P3 | B | | | | | |

**Totals:** completion rate `<x/6>`, average time A `<>`, average time B `<>`, total errors `<>`, help requests `<>`. (Only P1 recorded so far: A 01:10, B 00:34.)

**Patterns observed:** The participant got stuck when the panels did not give clear instructions about what to do. The proposed changes are in Problem 1 (section 4). `<fill in after P2 and P3>`

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