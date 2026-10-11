# AI Use

**Authors:** Jaime Rincón Burboa and José Pablo Soto Sánchez
**Course:** Human Computer Interaction, Universidad Panamericana

## How the team uses AI

AI is allowed in this course for Git commands, Unity code, debugging, design support and testing support. The team uses it as a help, not as the one who builds the project. The team decides what the feature should do first, writes its own draft, and then asks AI to review it, explain an error or suggest options. After that, the team chooses what to keep, change or throw away, and tests the result itself. AI answers never replace the team's own testing observations.

For each relevant use, the team records the tool, date, teammate, purpose, prompt, response used, modifications and verification, and links the related work.

## Index of entries

| # | Date | Teammate | Use | Type |
|---|---|---|---|---|
| 1 | 2026-10-09 | José Pablo Soto Sánchez | Reading and updating `Kits.json` to drive the buttons | Unity code, debugging |
| 2 | 2026-10-09 | José Pablo Soto Sánchez | Simplifying `KitsManager` to plain `if`/`else` and `for` | Unity code |
| 3 | 2026-10-09 | José Pablo Soto Sánchez | Other ways to build the file path | Unity code |
| 4 | 2026-10-09 | José Pablo Soto Sánchez | Rental dates and kit states | Unity code, debugging |
| 5 | 2026-10-09 | José Pablo Soto Sánchez | Updating `Kits.json` with the new fields | Unity code, debugging |
| 6 | 2026-10-09 | José Pablo Soto Sánchez | Drafting this document | Documentation |
| 7 | 2026-10-10 | José Pablo Soto Sánchez | Testing report template | Testing support |
| 8 | 2026-10-10 | José Pablo Soto Sánchez | Design justification template | Design support |

---

## Entry 1: Reading and updating `Kits.json` to drive the buttons

- **Tool:** Claude (Anthropic)
- **Date:** 2026-10-09
- **Teammate:** José Pablo Soto Sánchez (UPSoto)
- **Purpose:** Unity code and debugging. The team wanted the Unity app to read a JSON file at startup and after certain changes, and use it to update the kit buttons (values and availability).
- **Related work:** `Assets/Scripts/KitManager.cs`, `Assets/Scripts/KitButton.cs`, `Assets/Scripts/Kits.cs`, `Kits.json`

**Prompt**

> Tengo duda de cómo deberían de funcionar estas cosas. Quiero que en ciertos momentos, al iniciar el programa de Unity y después de ciertos cambios, se lea/actualice el json y con eso los botones se actualicen con valores, disponibilidad.

*(Attached: the team's own `JSONReader.cs`, `Kit.cs`, `Kits.cs` and `Kits.json`.)*

**Response used**

The AI pointed out problems in the team's first attempt: `Kit` was a `MonoBehaviour` (so `JsonUtility` could not fill it), the JSON keys did not match the classes, the `ColorBlock` was read after it was used, and `WriteData` used an empty path. The team used its suggested structure: a plain data class, a `Kits` wrapper, one manager that owns reading and writing, and a button script that only paints the button. The team also used the idea of saving to `Application.persistentDataPath` instead of writing to the `TextAsset`.

**Modifications**

The team kept the structure but adapted it to the app: the manager was named `KitManager`, an original JSON (`ogJson`) and a `ResetKits()` method were added, and the buttons were connected to the team's `CanvasManager` so an available kit opens the menu.

**Verification**

- [ ] What the team tested: *(fill in)*
- [ ] What actually happened: *(fill in)*

**What the team can explain**

- The team used AI to: find why the JSON was not loading and to get a cleaner structure.
- The team decided to: keep one manager as the only script that touches the file, because then other scripts only call its methods.
- The team verified the result by: *(fill in)*
- One thing the team still needs to check: *(fill in)*

---

## Entry 2: Simplifying `KitsManager` to plain `if`/`else` and `for`

- **Tool:** Claude (Anthropic)
- **Date:** 2026-10-09
- **Teammate:** José Pablo Soto Sánchez (UPSoto)
- **Purpose:** Unity code. The team wanted code that is easy to read and explain, without LINQ, lambdas or shortcuts.
- **Related work:** `Assets/Scripts/KitManager.cs`

**Prompt**

> Simplifica este código en puros if/else, for etc:

*(Followed by the team's `KitsManager` class with `FirstOrDefault`, `foreach` and `=>`.)*

**Response used**

The AI rewrote the class with `for` loops and `if` statements, and moved the kit search into one `FindKit` method to avoid repeating it. It also moved the `path` calculation into `Start()`.

**Modifications**

*(Fill in what the team changed after reading it.)*

**Verification**

- [ ] What the team tested: *(fill in)*
- [ ] What actually happened: *(fill in)*

**What the team can explain**

- The team used AI to: rewrite its own code in a simpler style.
- The team decided to: use plain loops, because every line of them can be explained.
- The team verified the result by: *(fill in)*
- One thing the team still needs to check: *(fill in)*

---

## Entry 3: Other ways to build the file path

- **Tool:** Claude (Anthropic)
- **Date:** 2026-10-09
- **Teammate:** José Pablo Soto Sánchez (UPSoto)
- **Purpose:** Unity code support. The team wanted to compare different ways to write the `path` for the save file.
- **Related work:** `Assets/Scripts/KitManager.cs`

**Prompt**

> Y otra manera de poner path?

**Response used**

The AI listed several options: simple string concatenation, string interpolation, setting the path in `Awake()` instead of `Start()`, a file name editable from the Inspector, and a subfolder. It also explained why `Application.dataPath` and `streamingAssetsPath` are not good for writing in a build.

**Modifications**

*(Fill in which option the team kept and why.)*

**Verification**

- [ ] What the team tested: *(fill in)*
- [ ] What actually happened: *(fill in)*

**What the team can explain**

- The team used AI to: compare options it did not know.
- The team decided to: *(fill in)*, because *(fill in)*.
- The team verified the result by: *(fill in)*
- One thing the team still needs to check: *(fill in)*

---

## Entry 4: Rental dates and kit states

- **Tool:** Claude (Anthropic)
- **Date:** 2026-10-09
- **Teammate:** José Pablo Soto Sánchez (UPSoto)
- **Purpose:** Unity code and debugging. A kit is being rented, so the team wants to save the date until which it is occupied and check it in `KitManager`.
- **Related work:** `Assets/Scripts/KitData.cs`, `Assets/Scripts/KitManager.cs`, `Assets/Scripts/KitButton.cs`

**Prompt**

> Estoy agregando la fecha a los botones, porque quiero trabajar con que se actualicen las fechas, ya que se está rentando un kit. Mi idea es guardar la fecha hasta la que están ocupados, así que quiero que en el mismo kitmanager se revise eso. En otros momentos estaré trabajando con los kits, no solo en el panel de donde estan los botones. Por ejemplo, en otras cosas la idea es que se actualicen los datos de los kits. Para comenzar, primero se tiene que verificar que si la fecha está antes que la actual, el kit ya pueda estar en un estado diferente de 2, porque podría estar cargándose, disponible o incompleto. Quiero que se pueda modificar la fecha después (0 disponible, 1, cargandose, 2 no disponible, 3 incompleto). Estoy pensando en usar DateTime para después hacer operaciones con las fechas.

*(Attached: the team's own `KitData`, `KitButton` and `KitManager` drafts, with the date fields already added.)*

**Response used**

The AI reviewed the drafts and found four problems: `path` was assigned twice (so `ogpath` was `null`), `AddListener` was called inside `Apply` and stacked listeners on every refresh, `button` was assigned in `Start` and could be used too early, and state 3 had no color. It also suggested `GetDate()` and `SetDate()` in `KitData`, a `CheckExpired()` method in `KitManager`, and `SetRental`, `SetDate` and `SetComplete` for other scripts.

**Modifications**

The idea and the states (0 available, 1 charging, 2 unavailable, 3 incomplete) came from the team, as did the decision to store the date in separate fields and use `DateTime` for operations. *(Fill in what the team changed from the AI's version, for example which state an expired kit goes to.)*

**Verification**

- [ ] What the team tested: *(fill in, for example a kit with a past date)*
- [ ] What actually happened: *(fill in)*

**What the team can explain**

- The team used AI to: review its drafts and find bugs it had not noticed.
- The team decided to: store the rental end date and check it in `KitManager`, because other parts of the app will also change kit data.
- The team verified the result by: *(fill in)*
- One thing the team still needs to check: *(fill in)*

---

## Entry 5: Updating `Kits.json` with the new fields

- **Tool:** Claude (Anthropic)
- **Date:** 2026-10-09
- **Teammate:** José Pablo Soto Sánchez (UPSoto)
- **Purpose:** Unity code and debugging support. The JSON needed the new date and `complete` fields.
- **Related work:** `Kits.json`, `Assets/Scripts/KitData.cs`

**Prompt**

> Hay que actualiza tambien el kit.json:

*(Followed by the team's JSON with six kits, where kit 1 had `"year":2026, "month": ` unfinished.)*

**Response used**

The AI noticed that `"month":` had no value, which breaks `JsonUtility`, and completed every kit with all the fields of `KitData`. It put a real date only on the kit in state 2 and explained that a kit in state 2 with the date in zeros expires immediately. It also reminded the team that the saved copy in `persistentDataPath` must be reset to see the new JSON.

**Modifications**

*(Fill in the dates and `complete` values the team actually used.)*

**Verification**

- [ ] What the team tested: *(fill in)*
- [ ] What actually happened: *(fill in)*

**What the team can explain**

- The team used AI to: fix the broken JSON and fill in the missing fields.
- The team decided to: *(fill in)*, because *(fill in)*.
- The team verified the result by: *(fill in)*
- One thing the team still needs to check: *(fill in)*

---

## Entry 6: Drafting this document

- **Tool:** Claude (Anthropic)
- **Date:** 2026-10-09
- **Teammate:** José Pablo Soto Sánchez (UPSoto)
- **Purpose:** Design and documentation support. The team used the course slides (`UP_HCI_Session20_IA_pptx.pdf`) and the required-document description to build `docs/ai-use.md`.
- **Related work:** [README.md](../README.md)

**Prompt**

> Haz mi ai-use.md con base a las instrucciones. Haz que parezca y que se vea como tal cual solo esto que necesito. También tienes que incluir este prompt y respuesta en ese documento.

*(The first prompt included the instructions: "AI is allowed for Git commands, Unity code, debugging, design support and testing support. You must understand and verify what you use. For each relevant use, record the tool, date, teammate, purpose, prompt, response used, modifications and verification. Link the related work. AI assistance does not replace real testing observations.")*

**Response used**

The AI wrote the structure of this document, turned the prompts of this conversation into entries, and rewrote the text in third person. This file is that response.

**Modifications**

*(Fill in what the team changed, and which placeholders it replaced with real results.)*

**Verification**

- [ ] The team checked every field against the course instructions.
- [ ] The team replaced all the *(fill in)* parts with its real actions and results.
- [ ] The related-work links point to real files in the repository.

**What the team can explain**

- The team used AI to: get the structure and wording of this document from the course instructions.
- The team decided to: *(fill in)*, because *(fill in)*.
- The team verified the result by: *(fill in)*
- One thing the team still needs to check: *(fill in)*

---

## Entry 7: Testing report template

- **Tool:** Claude (Anthropic), in the same conversation used to develop `KitSelector` and `KitManager`, so it had the project code as context.
- **Date:** 2026-10-10
- **Teammate:** José Pablo Soto Sánchez (UPSoto)
- **Purpose:** Testing support. The team needed a structure for the testing report required by the course.
- **Related work:** [docs/testing.md](testing.md), `Assets/Scripts/KitSelector.cs`, `Assets/Scripts/KitManager.cs`, `Kits.json`

**Prompt**

> Ya que has visto el proyecto, ayúdame para mi .md de tsting report:
> TESTING REPORT / docs/testing.md
> - Six functional cases: checkout, no availability, invalid kit,
> editing choices, saved data after restart and scenario reset.
> - For each: setup, steps, expected result, actual result,
> pass/fail and evidence. Identify the tested build.
> - Three users outside your team attempt both scenarios
> without click guidance. Use anonymous participant IDs.
> - Record completion, time, errors, observations and help.
> Fix two problems and retest with evidence before and after.
> - If fewer than two problems appear, expand testing.
> Document real findings and any unresolved issues.

**Response used**

The AI produced an empty template for `docs/testing.md`: the six test cases with setup, steps and expected results based on the team's code, the test data for `Kits.json`, the usability table for participants P1 to P3, the problem/fix/retest sections, and a list of candidate problems found by reading the code. All result fields were left empty on purpose.

**Modifications**

*(Fill in: scenarios redefined, candidate problems that did not reproduce, wording edited, cases added or removed.)*

**Verification**

- [ ] What the team tested: *(fill in: the six cases, run by the team on the tested build)*
- [ ] What actually happened: *(fill in: real results, times and evidence saved in `docs/evidence/`)*
- [ ] The three outside users were tested by the team, not simulated by the AI.
- [ ] The two fixed problems have evidence before and after.

The AI did not run the app, so all results, times and evidence in `docs/testing.md` come from the team's own tests.

**What the team can explain**

- The team used AI to: get the structure of the report and a list of possible problems from the code.
- The team decided to: *(fill in which two problems to fix)*, because *(fill in)*.
- The team verified the result by: *(fill in)*
- One thing the team still needs to check: *(fill in, for example which candidate problems did not reproduce)*

---

## Entry 8: Design justification template

- **Tool:** Claude (Anthropic), in the same conversation used to develop `KitSelector` and `KitManager`, so it had the project code as context.
- **Date:** 2026-10-10
- **Teammate:** José Pablo Soto Sánchez (UPSoto)
- **Purpose:** Design support. The team needed a structure for the design justification required by the course.
- **Related work:** [docs/design.md](design.md), [docs/testing.md](testing.md)

**Prompt**

> Hazme una plantilla para mi .md design justification con los siguientes lineamientos:
> docs/design.md: justify three interaction decisions.
> For each, include the user problem, HCI principle and image.
> Compare Figma with Unity. Explain what changed and why,
> using implementation needs or testing evidence

**Response used**

The AI produced an empty template for `docs/design.md`: three decision tables (user problem, HCI principle, decision, image, why it works, evidence), a Figma vs Unity screen comparison, a table of what changed and why (marked as implementation need or testing evidence), and a limitations section. It also suggested candidate decisions and candidate Figma/Unity differences based on the team's code.

**Modifications**

*(Fill in: which three decisions the team chose, candidate decisions replaced, candidate differences that did not match the real Figma file, wording edited.)*

**Verification**

- [ ] The three decisions, user problems and HCI principles were chosen and checked by the team.
- [ ] The Figma and Unity screenshots are the team's own.
- [ ] Every candidate difference was checked against the real Figma file before being kept.
- [ ] Every "testing evidence" reason points to a real case or participant in `docs/testing.md`.

The AI did not have access to the team's Figma file or the running app.

**What the team can explain**

- The team used AI to: get the structure of the document and ideas for decisions to justify.
- The team decided to: *(fill in the three decisions)*, because *(fill in)*.
- The team verified the result by: *(fill in)*
- One thing the team still needs to check: *(fill in)*