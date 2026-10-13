# Design Justification

This document justifies three interaction decisions of the kit checkout app and compares the Figma
design with the final Unity implementation.

**Build / commit:** `<git rev-parse --short HEAD>`
**Related documents:** [testing report](testing.md), [AI use](ai-use.md)

---

## 1. Interaction decisions

Each decision states the user problem, the HCI principle that supports it, an image and the result.
Replace the candidate decisions with the three you actually want to defend.

### Decision 1: `<title, e.g. "Checkout split into short steps (kit, duration, checklist, review, confirmation)">`

| | |
|---|---|
| **User problem** | `<What the user struggled with or needed. E.g. "Reserving a kit requires several choices; showing them all at once would be confusing and easy to get wrong.">` |
| **HCI principle** | `<Name + one sentence. E.g. Progressive disclosure / Hick's law: fewer choices per screen make each decision faster.>` |
| **Decision** | `<What the interface does. E.g. one panel per step, with a Next button that saves the values of that step.>` |
| **Image** | ![Decision 1](evidence/design-d1.png) |
| **Why it works** | `<Link the principle to the image. Point at the specific element.>` |
| **Evidence** | `<Testing evidence, e.g. "P1 and P3 completed Scenario A without help (see testing.md, section 3).">` |

### Decision 2: `<title, e.g. "Review panel with editable duration before confirming">`

| | |
|---|---|
| **User problem** | `<E.g. "Users may notice a wrong duration only at the end and would have to restart.">` |
| **HCI principle** | `<E.g. User control and freedom (Nielsen) / Error prevention.>` |
| **Decision** | `<E.g. Order Check repeats the chosen duration in dropdowns that can still be changed, and the summary updates.>` |
| **Image** | ![Decision 2](evidence/design-d2.png) |
| **Why it works** | `<fill in>` |
| **Evidence** | `<E.g. TC-04 Editing choices.>` |

### Decision 3: `<title, e.g. "Visible kit state and checklist feedback (available, charging, not available, incomplete)">`

| | |
|---|---|
| **User problem** | `<E.g. "Users need to know which kits they can take and whether a kit is missing something.">` |
| **HCI principle** | `<E.g. Visibility of system status / Recognition over recall / Feedback.>` |
| **Decision** | `<E.g. each kit button shows its state; the checklist shows a different button when an item is missing.>` |
| **Image** | ![Decision 3](evidence/design-d3.png) |
| **Why it works** | `<fill in>` |
| **Evidence** | `<E.g. TC-02 No availability, usability errors observed.>` |

---

## 2. Figma vs Unity

For each screen, show both versions side by side, then explain what changed and why.
The reason must be an **implementation need** (technical constraint) or **testing evidence** (a test case or participant ID).

### Screen comparison

| Screen | Figma | Unity |
|---|---|---|
| Kit selection | ![](evidence/figma-kits.png) | ![](evidence/unity-kits.png) |
| Duration | ![](evidence/figma-duration.png) | ![](evidence/unity-duration.png) |
| Checklist | ![](evidence/figma-checklist.png) | ![](evidence/unity-checklist.png) |
| Order Check | ![](evidence/figma-review.png) | ![](evidence/unity-review.png) |
| Reservation completed | ![](evidence/figma-confirmed.png) | ![](evidence/unity-confirmed.png) |

### What changed and why

| # | Screen | Figma | Unity | Reason | Type |
|---|---|---|---|---|---|
| 1 | `<screen>` | `<what the design had>` | `<what was built>` | `<why>` | Implementation / Testing |
| 2 | | | | | |
| 3 | | | | | |
| 4 | | | | | |

**Type** is *Implementation* (needed by Unity, data or time) or *Testing* (changed after a test; cite the case or participant, e.g. TC-04, P2).

**Candidate differences to check against your Figma file** (keep only the ones that are true):

- **Duration input:** dropdowns (days 0-7, hours 0/1/2/3/6/12, minutes 0-30) instead of a free time picker, because they keep the value valid and are easy to read from `TMP_Dropdown` in code.
- **Confirmation screen:** day, month, year, hour and minute are separate text fields, so each one is filled independently by `KitSelector`.
- **Kit state on buttons:** four states come from `Kits.json` and are applied in `KitManager.RefreshAll()`, so the button appearance depends on saved data and not only on the design.
- **Expired rentals:** kits whose time has passed change state automatically (`CheckExpired()`), which the static Figma frames cannot show.
- **Changes after testing:** `<e.g. review duration became editable after P2 wanted to change it>`.