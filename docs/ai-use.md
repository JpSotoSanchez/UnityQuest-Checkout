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
| 9 | 2026-10-10 | José Pablo Soto Sánchez | Reserving an occupied kit (date + duration) and NOT AVAILABLE flow | Unity code, debugging |
| 10 | 2026-10-10 | Jaime Rincón Burboa | Rounded buttons with 9-Slice | Design support |
| 11 | 2026-10-10 | Jaime Rincón Burboa | Larger TMP Dropdown options and faster scroll | Design support |
| 12 | 2026-10-10 | Jaime Rincón Burboa | Checkboxes (Toggle) for the Kit Check scene | Design support |
| 13 | 2026-10-10 | Jaime Rincón Burboa | Converting OTF fonts for TextMeshPro | Design support |
| 14 | 2026-10-12 to 2026-10-13 | José Pablo Soto Sánchez | Putting the team's real results into the testing report | Testing support | 
| 15 | 2026-10-12 to 2026-10-13 | José Pablo Soto Sánchez | Putting the team's decisions and screenshots into the design justification | Design support |

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

- [x] What the team tested: that the JSON files were read and that this was reflected in `KitManager` without anything crashing.
- [x] What actually happened: the code did what was expected once the pieces were put together.

**What the team can explain**

- The team used AI to: find why the JSON was not loading and to get a cleaner structure.
- The team decided to: keep one manager as the only script that touches the file, because then other scripts only call its methods.
- The team verified the result by: reading the JSON files and checking that `KitManager` reflected them without errors.
- One thing the team still needs to check: that this way of saving the data can be read correctly when the app is actually used, and that it is scalable if it is modified later.

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

The team read all of the code to make sure it used the syntax the programmer wanted.

**Verification**

- [ ] What the team tested: no formal test was run; the team read all of the code to check the syntax.
- [x] What actually happened: everything kept working as before, only now with a syntax that is easier to understand.

**What the team can explain**

- The team used AI to: rewrite its own code in a simpler style.
- The team decided to: use plain loops, because every line of them can be explained.
- The team verified the result by: reading all of the code and checking that everything kept working as before.
- One thing the team still needs to check: nothing else. The code was seen to work exactly the same as before.

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

None. The team kept the path option that was already in the code.

**Verification**

- [x] What the team tested: another way to build the path, using the AI's options as a source of ideas.
- [x] What actually happened: the team kept what it already had.

**What the team can explain**

- The team used AI to: compare options it did not know.
- The team decided to: keep the path option that was already in the code, because it was easier to implement.
- The team verified the result by: trying another way to build the path and comparing it with the one the code already had.
- One thing the team still needs to check: that if the path is modified for any reason, how the code should be adjusted, or whether it needs to be modified at all.

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

The idea, the decision to store the date in separate fields and the use of `DateTime` for operations came from the team. Originally the team only had states 0, 1 and 2; state 3 (incomplete) was added afterwards.

**Verification**

- [x] What the team tested: that the different functions and the research on `DateTime` fit the project, that the data was saved correctly, and that the colors adjusted as the dates were modified.
- [x] What actually happened: the AI helped build the code so that everything fit with the rest of the project and with the idea of how the app should work.

**What the team can explain**

- The team used AI to: review its drafts and find bugs it had not noticed.
- The team decided to: store the rental end date and check it in `KitManager`, because other parts of the app will also change kit data.
- The team verified the result by: checking that the functions fit the project, that the data was saved correctly, and that the colors changed when the dates were modified.
- One thing the team still needs to check: whether the data will stay this way, whether more sections will eventually be added to the code, whether the data is kept across scenes, and whether it is overwritten correctly.

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
 
The team changed `Kits.json` so that it had all the fields complete with the information.
 
**Verification**
 
- [x] What the team tested: that the app was followed through and that the data was updated.
- [x] What actually happened: the AI updated the data.
**What the team can explain**
 
- The team used AI to: fix the broken JSON and fill in the missing fields.
- The team decided to: let the AI fill in the data, because it made testing more automated and saved time.
- The team verified the result by: following the app and checking that the data was updated.
- One thing the team still needs to check: that the dates are appropriate for the tests.
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

The team adjusted some things, for example an index was added at the start, which did not exist at first.

**Verification**

- [x] The team checked that the document included the prompts made so far, that the format was good, and that it works as a template to add more entries later.
- [x] The team replaced all the parts with its real actions and results.
- [x] The related-work links point to real files in the repository.

**What the team can explain**

- The team used AI to: get the structure and wording of this document from the course instructions.
- The team decided to: use AI to write the document, because all the prompts were already in that same conversation and the AI would also provide the template.
- The team verified the result by: checking that it included the prompts made so far and that the format could be reused for new entries, and by filling in the template with the team's real data.
- One thing the team still needs to check: that there are no errors in the wording the AI wrote.

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

No changes were made to the direct result of the AI. Afterwards, the team added all the required data to complete the report, using the real data it obtained.

**Verification**

- [x] What the team tested and what actually happened: the team added all the required data to `docs/testing.md`, using the real data it obtained.
- [ ] The three outside users were tested by the team, not simulated by the AI.
- [ ] The two fixed problems have evidence before and after.

The AI did not run the app, so all results, times and evidence in `docs/testing.md` come from the team's own tests.

**What the team can explain**

- The team used AI to: get the structure of the report and a list of possible problems from the code.
- The team decided to: use AI to get a template, because it would be much easier than starting from scratch.
- The team verified the result by: adding the real data it obtained to the template.
- One thing the team still needs to check: that, even though all the sections were seen to match the requirements, the report does not include more or less than required and that its format will not make it invalid later.

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

No changes have been made to the template yet. The team reviewed that it had everything needed, so the real data of the project can be added to it later.

**Verification**

- [x] The team reviewed that the template had everything required by the course parameters.
- [ ] The three decisions, user problems and HCI principles were chosen and checked by the team.
- [ ] The Figma and Unity screenshots are the team's own.
- [ ] Every candidate difference was checked against the real Figma file before being kept.
- [ ] Every "testing evidence" reason points to a real case or participant in `docs/testing.md`.

The AI did not have access to the team's Figma file or the running app.

**What the team can explain**

- The team used AI to: get the structure of the document and ideas for decisions to justify.
- The team decided to: use AI to get a template, because it would be much easier than starting from scratch.
- The team verified the result by: reviewing that the template had everything needed before filling it in with the real data of the project.
- One thing the team still needs to check: that, even though all the sections were seen to match the requirements, the document does not include more or less than required and that its format will not make it invalid later.

---

## Entry 9: Reserving an occupied kit (date + duration) and the NOT AVAILABLE flow

- **Tool:** Claude (Anthropic), in the same conversation used to develop `KitSelector` and `KitManager`, so it had the project code as context.
- **Date:** 2026-10-10
- **Teammate:** José Pablo Soto Sánchez (UPSoto)
- **Purpose:** Unity code and debugging. The team wanted a kit that is not available to still be reservable: the user sees a NOT AVAILABLE screen with the date when the kit is released, continues through the main path, and the end date becomes the kit's current date plus the chosen duration. A free kit keeps working as before (current time plus duration).
- **Related work:** `Assets/Scripts/KitManager.cs`, `Assets/Scripts/KitSelector.cs`, `Assets/Scripts/KitButton.cs`, `Assets/Scripts/CanvasManager.cs`, `Kits.json`, `KitsOg.json`

**Prompt**

The prompts were sent in sequence in one conversation. They are combined here:

> Quiero hacer varias cosas. Si el kit está libre en el momento, ya funciona el selector de fechas porque le añade la duración a la fecha actual, sin embargo, si está no disponible quiero que de igual manera pueda reservar que sería a partir de la hora a la que se descocupa + la duración que el usuario. Resuelve que se pueda hacer eso.
>
> En el panel uno, cuando se elige el kit, si se presiona uno que no está disponible, al usuario se le muestra una pantalla que le muestra la fecha en la que estará disponible, de ahí el usuario quiere igualmente hacer la reservación, se sigue con el main path. Y ahí es donde aplica que se sume la hora. Todavía no están esos labels, pero se incluirán. Nuestra opción es que ya con eso se haga la reserva, por lo que sí se debe obtener la fecha, ajustarla según lo necesario, ya sea que si está disponible ponga la hora actual + la duración y en caso de que no ponga la fecha que ya tiene + la duración.
>
> Estos son los códigos que tengo actualmente, qué me falta para la implementación?
>
> Follow-up questions about the Inspector: how much needs to change, what the 6 kit buttons must have in On Click, and what the Next button of the NOT AVAILABLE panel must have.
>
> Algo está raro, no me marca que se ponga verde. / No entiendo cuál fue el error.

*(Attached: the team's own `KitManager.cs`, `KitSelector.cs`, `KitButton.cs`, `CanvasManager.cs`, `KitData.cs`, `Kits.json`, `KitsOg.json`, and screenshots of the kit selection screen and the NOT AVAILABLE panel.)*

**Response used**

- **Rental logic:** The AI added `IsOccupied()` and `GetAvailableFrom()` to `KitManager`. `ConfirmReservation()` in `KitSelector` now calculates the end date as the kit's current date plus the duration if the kit is occupied, or the current time plus the duration if it is free. For an occupied kit only the date is changed (`SetDate`), so the state and the `complete` value of the person who has the kit are not overwritten.
- **NOT AVAILABLE screen:** The AI added `ShowAvailableFrom()`, which writes the release date in separate TMP texts (the team's labels were not created yet, so the fields are optional).
- **Bug in `KitButton`:** After reading the team's code, the AI found that `Apply()` called `AddListener` on every refresh, so listeners stacked and a button could open the wrong panel. It rewrote the script with one listener added in `Awake()` that checks the current state when pressed and calls `KitSelected(kitNumber)`.
- **Own correction:** In its first answer the AI suggested `UnityEvent`s and a new `KitPressed` method before seeing `KitButton` and `CanvasManager`. After the team shared those files, it dropped that idea because `KitButton` already routes each state to a panel.
- **Stale data:** When the team's kit 2 did not turn green after editing the JSON, the AI explained that `KitManager.Load()` copies the default JSON to `persistentDataPath` only the first time, so later edits to the project JSON are ignored. It suggested deleting the saved file (with two `ContextMenu` helper methods) or using the reset combo with `KitsOg.json`.
- **Suggestions not requested:** The AI also suggested skipping the checklist for occupied kits in `CanvasManager.toKitCheck()`, blocking `ReportIncomplete()` when the kit is occupied, and changing `ReadDuration()` to `ReadReviewDuration()` in `ConfirmReservation()`.

**Modifications**

The AI proposed a solution that the team had to correct so it fit the project and so the data was saved correctly. The team also modified the flow so that, if it is already known that the kit is rented, the user no longer has to check whether it is complete, which makes that part simpler for the user.

**Verification**

- [x] Before asking the AI, the team tested the whole app and noticed a few things that were missing to have a more complete project.
- [x] The team noticed that kit 2 stayed yellow after editing the JSON, and confirmed the cause was the old saved copy in `persistentDataPath`, not the code.
- [x] What the team tested after the changes: that, with the correct data, kit 1 turns green.
- [x] What actually happened: the error of the app reading the existing saved data was fixed, and kit 1 turned green with the correct data.
- [x] A free kit still gets current time plus duration: it adds the dates correctly.
- [x] After a reservation on an occupied kit, its `complete` value did not change.

**What the team can explain**

- The team used AI to: add a couple of missing functions and correct part of the logic the team had already planned.
- The team decided to: use AI to help think through the flow, because the team had doubts about how the parts connected.
- The team verified the result by: testing everything before asking the AI, and then correcting the AI's solution until it fit the project and the data was saved correctly.
- One thing the team still needs to check: that the dates match and that the data is saved correspondingly.

---

## Entry 10: Rounded buttons with 9-Slice

- **Tool:** Gemini 3.5 Thinking (Google)
- **Date:** 2026-10-10
- **Teammate:** Jaime Rincón Burboa
- **Purpose:** Design support. The team wanted the Unity buttons to have rounded corners instead of square ones.
- **Related work:** Button prefabs

**Prompt**

> En Unity puedo hacer que los botones tengan bordes redondos en vez de ser cuadrados?

**(Attached: nothing.)**

**Response used**

The AI explained two options: a 9-Slice sprite in uGUI (the traditional Canvas) and the Border Radius property in UI Toolkit. For uGUI it gave four steps: set the sprite's Texture Type to Sprite (2D and UI), adjust the four guides in the Sprite Editor so the curves stay inside the corners, assign the sprite to the button's Source Image, and change Image Type to Sliced. The team used the uGUI 9-Slice option. The UI Toolkit option was not used.

**Modifications**

The team did not reject or change anything in the AI's answer. The sprite was not made by the AI: the team downloaded it. The team applied the result to its buttons and then turned them into prefabs.

**Verification**

- [x] What the team tested: following the AI's steps on the team's buttons with the downloaded sprite and Image Type set to Sliced.
- [x] What actually happened: the verification checks listed in each step worked correctly. *(fill in: what was seen, e.g. corners stay rounded when the button is resized)*

**What the team can explain**

- The team used AI to: learn how to make rounded buttons in Unity.
- The team decided to: use uGUI with 9-Slice and a downloaded sprite, because the default button sprite looks dull.
- The team verified the result by: Applying the 9-Slice png to the project.
- One thing the team still needs to check: That the button scaled correctly while changing height and width.

---

## Entry 11: Larger TMP Dropdown options and faster scroll

- **Tool:** Gemini 3.5 Thinking (Google)
- **Date:** 2026-10-10
- **Teammate:** Jaime Rincón Burboa
- **Purpose:** Design support. The team wanted the options of a TextMeshPro Dropdown to be bigger and the list to scroll faster.
- **Related work:** App scene, duration panel dropdown. The same dropdown composition was reused in other panels with some adjustments.

**Prompt**

The prompts were sent in sequence in one conversation:

> En el Unity text Mesh Pro Dropdown como puedo hacer que las opciones que aparecen se hagan más grandes? Y como puedo hacer que vaya más rápido el scroll del dropdown

**(Attached: nothing.)**

**Response used**

For the size, the AI explained that the Template object has to be made visible temporarily, that the Height of Content and Item must have the same value, and that the font size is changed in Item Label. For the scroll speed, it explained that the Scroll Sensitivity of the Scroll Rect on the Template controls it, and it suggested an optional inertia and Deceleration Rate adjustment. The team used the steps for Content, Item, Item Label and Scroll Sensitivity. The team did not use the optional inertia adjustment.

**Modifications**

The team followed the steps but changed the final values. It used 280x200 *(fill in: which object this size belongs to)* and a Template Height of 500, so that several options are visible at first in the dropdown. After testing in Play, it set Scroll Sensitivity to 20. The inertia was not added.

**Verification**

- [x] What the team tested: the dropdown in Play, adjusting the scroll speed until it felt right.
- [x] What actually happened: with Scroll Sensitivity at 10 the scroll speed was good for the team and that value was kept.
- [x] Several options are visible at first with the Template Height of 500.
- [x] The reused dropdowns in the other panels work and look correct.

**What the team can explain**

- The team used AI to: learn how to enlarge the dropdown options and speed up their scroll.
- The team decided to: use a Template Height of 500 and a Scroll Sensitivity of 20, because several options should be visible at first and the scroll should feel good in Play.
- The team verified the result by: testing the dropdown in Play and adjusting the scroll speed until it was comfortable.
- One thing the team still needs to check: In the other scened we reused the dropdown if it worked correclty.

---

## Entry 12: Checkboxes (Toggle) for the Kit Check scene

- **Tool:** Gemini 3.5 Thinking (Google)
- **Date:** 2026-10-10
- **Teammate:** Jaime Rincón Burboa
- **Purpose:** Design support. The team wanted to create checkboxes in Unity to verify that a kit is complete, and to understand the difference between the Interactable and Is On properties of a Toggle.
- **Related work:** Kit Check scene.

**Prompt**

The prompts were sent in sequence in one conversation:

> Como puedo hacer unos checkboxes en Unity
>
> Whats the difference betwen Interactable and Is on?

**(Attached: nothing.)**

**Response used**

For the first prompt, the AI explained that the standard checkbox in Unity is the Toggle from the uGUI system. It gave four steps: create the Toggle from the Hierarchy, customize the Background and Checkmark images, create a script with a public function that receives a boolean (CheckboxHandler.cs with OnToggleValueChanged), and link that function to the On Value Changed event. For the second prompt, it explained that Is On is the checked or unchecked value, while Interactable controls whether the user can change it, and gave a table with the four combinations. The team followed the steps to create the Toggles. The script with OnToggleValueChanged was not modified by the teammate who wrote this entry; it was used by the teammate in charge of the code and functionality. 

**Modifications**

The team did not reject anything in the AI's answer. After testing, it changed the Toggles so they start unchecked (Is On turned off), because they were starting checked. 

**Verification**

- [x] What the team tested: the checkboxes of the Kit Check scene when the scene starts.
- [x] What actually happened: the checkboxes started with Is On checked, so the team changed them to start unchecked.
- [x] After the change, the checkboxes start unchecked and can be checked by the user.
- [x] The state of the checkboxes is read correctly by the code to confirm that the kit is complete.

**What the team can explain**

- The team used AI to: learn how to create checkboxes with the Toggle component and to understand the difference between Is On and Interactable.
- The team decided to: start the checkboxes unchecked, because they were starting checked and the user has to verify each item of the kit.
- The team verified the result by: testing the Kit Check scene and noticing the initial state of the checkboxes.
- One thing the team still needs to check: If the dependant logic of the toggle worked correctly when assigning the code to the toggle objects.

---

## Entry 13: Converting OTF fonts for TextMeshPro

- **Tool:** Gemini 3.5 Thinking (Google)
- **Date:** 2026-10-10
- **Teammate:** Jaime Rincón Burboa
- **Purpose:** Design support. The team wanted to use its own .otf fonts (CreatoDisplayRegular and CreatoDisplayMedium) in the Unity app.
- **Related work:** CreatoDisplayRegular and CreatoDisplayMedium font files and their TMP Font Assets. The fonts are applied to the text fields, buttons and labels of the app.

**Prompt**

> How to transform an otf font into something unity can use?

**(Attached: nothing.)**

**Response used**

The AI explained that Unity supports .otf files and gave two methods: TextMeshPro (converting the font into a Font Asset with the Font Asset Creator) and Legacy UI Text (dragging the .otf file directly into the Font slot). For TextMeshPro it gave four steps: import the .otf file into the Project window, open Window > TextMeshPro > Font Asset Creator, generate the font atlas (Sampling Point Size on Auto Sizing) and save it, and assign the Font Asset to the text object. The team used the TextMeshPro method with the Font Asset Creator. The Legacy UI Text method was not used.

**Modifications**

The team did not change any of the steps. It created Font Assets for CreatoDisplayRegular and CreatoDisplayMedium and applied them to all text fields, buttons and labels. The AI's answer did not mention the conflicts with TMP fonts described in Verification; the team solved them on its own.

**Verification**

- [x] What the team tested: the app with the new fonts applied to the text fields, buttons and labels.
- [x] What actually happened: there were conflicts with the TMP fonts. The team solved them by reimporting the library *(fill in: which library, e.g. TMP Essential Resources)* and moving the imported fonts to a different location *(fill in: from where to where)*.
- [x] All text fields, buttons and labels show the correct font (Regular or Medium) in Play.
- [x] Characters such as accents or ñ display correctly.

**What the team can explain**

- The team used AI to: learn how to convert .otf fonts into something TextMeshPro can use.
- The team decided to: use the Font Asset Creator (TextMeshPro method) with CreatoDisplayRegular and CreatoDisplayMedium, because.
- The team verified the result by: testing the app and solving the TMP font conflicts that appeared.
- One thing the team still needs to check: An error we had with the Unity editor that was not detecting the font correctly.

---

## Entry 14: Putting the team's real results into the testing report
 
- **Tool:** Claude (Anthropic), in the same conversation used to develop `KitSelector` and `KitManager`, so it had the project code as context.
- **Date:** 2026-10-12 to 2026-10-13
- **Teammate:** José Pablo Soto Sánchez (UPSoto)
- **Purpose:** Testing support. The team already had its own results (functional test cases, three usability sessions, problems and fixes) and needed them arranged in the format required for `docs/testing.md`. All the information is the team's own; the AI only helped put it in the requested format.
- **Related work:** [docs/testing.md](testing.md), `docs/images/` (evidence files), `Assets/Scripts/KitSelector.cs`, `Assets/Scripts/KitManager.cs`, `Kits.json`
**Prompt**
 
The team sent many short messages (dictated, in Spanish) over two days: the result of each test case, the data of each participant, the evidence file names and the commit hashes. They are summarized here with one general prompt. The first message started like this:
 
> Para llenar los test cases, vamos a ir poniendo lo siguiente. Yo, Soto, hice o realicé los test cases. [...]
 
*(Followed by the team's own result, pass/fail criterion and evidence for each case, and later by the times, errors, help, commits, screenshots and decisions about P1, P2 and P3.)*
 
**Response used**
 
The AI placed what the team said in the tables of `docs/testing.md`: the six functional cases, the session log, the results table, the problems with before and after evidence, and the unresolved issues. It also did the following with the team's data:
 
- It calculated the totals and averages of the usability sessions from the times the team gave (completion 6/6, average time of scenario A and of scenario B).
- It wrote the conclusion using the results of the three participants.
- It rewrote the expected result of some cases to match what the team had decided (for example, that a kit that is not available can still be reserved, and that the reset is done with a key combo and not with a button).
- It listed what was still missing at each step, and it moved the candidate problems found by reading the code to the unresolved issues table, as limitations, because the team decided they would not be fixed for lack of time.
- It proposed the severity of the unresolved issues, and it suggested some evidence images from their file names (for TC-03 and TC-04).
**Modifications**
 
The team corrected the AI's drafts every time something did not match what really happened: the pass criteria of TC-01 and TC-02, the results of TC-03, the real commits and times of the sessions, where the sessions took place, which problem was fixed in which commit, and the names of the evidence images. The team also decided which problems count as fixed (Problem 1 and Problem 2) and which remain unresolved (Problem 3 and the limitations).
 
**Verification**
 
- [x] All the results, times, errors and observations come from the functional tests and the three usability sessions that the team ran itself. The AI did not run the app.
- [x] The team gave the pass criterion of every case and the participants' data, and checked the totals against its own times.
- [x] The evidence files were named by the team and saved in `docs/images/`.
- [x] The team checked that every evidence file name written in `docs/testing.md` matches a real file in `docs/images/`.

**What the team can explain**
 
- The team used AI to: arrange its own test results, times and evidence in the format required for the testing report, and to calculate the totals and write a first version of the conclusion.
- The team decided to: use AI only for arranging the information, because all the data came from the team's own tests and the team wanted to avoid mistakes in such a long document.
- The team verified the result by: correcting the AI's drafts against what really happened in each test and session, and checking the commits, times and files it wrote.
- One thing the team still needs to check: that the evidence images and commit links in `docs/testing.md` open correctly in the repository.
---
 
## Entry 15: Putting the team's decisions and screenshots into the design justification
 
- **Tool:** Claude (Anthropic), in the same conversation used to develop `KitSelector` and `KitManager`, so it had the project code as context.
- **Date:** 2026-10-12 to 2026-10-13
- **Teammate:** José Pablo Soto Sánchez (UPSoto)
- **Purpose:** Design support. The team had already decided its three interaction decisions and had its own Figma and Unity screenshots, and needed them arranged in the format required for `docs/design.md`. The information (decisions, reasons and changes) is the team's own; the AI only helped put it in the requested format.
- **Related work:** [docs/design.md](design.md), [docs/testing.md](testing.md), `docs/images/` (Figma and Unity screenshots)
**Prompt**
 
The team sent several messages (dictated, in Spanish). They are summarized here with one general prompt. The first message started like this:
 
> Vamos a trabajar en mi desing.md. Si me falta algo, no lo llenes ni lo inventes, solo pon en mayúsculas FILL IN, como en el ia use report. [...]
 
*(Followed by the course requirements, the list of the team's own Figma and Unity screenshots, how the data and managers of the app work, the three decisions, the HCI principle of Decision 3, and the changes from Figma to Unity with their reasons. The team also attached the Figma and Unity PDFs.)*
 
**Response used**
 
The AI arranged the team's information in `docs/design.md`: the three decisions with user problem, how the design addresses it and images; the screenshots grouped by screen to compare Figma and Unity; the implementation context (three JSON files and the managers of the scene); the table of what changed and why; and what stayed the same. It followed the team's instruction of not inventing anything: everything the team did not say was marked FILL IN. It also did the following:
 
- It looked at the Figma and Unity screenshots and noticed differences the team had not listed (the kit selection title and pictures, the Next button in the kit check, the Day / Month / Year labels and the "Home" button on the confirmation screen). It added them to the table with the reason left as FILL IN, so the team could confirm or delete them.
- **Own correction:** the team said that Decision 1 worked the same as in Figma, but in the Figma screenshot the NOT AVAILABLE panel did not show the release time. The AI changed the text to say that the flow is the same and that showing the release time is new in Unity, and asked the team to correct it if that was wrong.
- It pointed out a typo in the Unity Incomplete panel ("ast" instead of "as").
- It suggested possible HCI principles for Decisions 1 and 2, but did not write them in the document because the team had not chosen them.
**Modifications**
 
The decisions, the user problems, the HCI principle of Decision 3 (affordance and consistency) and the reasons for the changes between Figma and Unity (duration and Order Check without the start date, a larger Back button, Charging without the Next button) came from the team. The team also chose to keep the images in `docs/images/`.
 
**Verification**
 
- [x] The decisions, reasons and changes in `docs/design.md` come from the team's own app and Figma design. The AI did not have access to the Figma file or to the running app, only to the screenshots and PDFs the team attached.
- [x] The team's instruction to mark missing information as FILL IN, and not to invent it, was followed.
- [x] The extra differences that the AI found in the screenshots were checked by the team against the real Figma file and Unity scene before being kept.
- [x] All the FILL IN fields (HCI principles of Decisions 1 and 2, user problem of Decision 3, testing evidence, reasons of the last changes and the limitations table) were completed by the team.

**What the team can explain**
 
- The team used AI to: arrange its own decisions, reasons and screenshots in the format required for the design justification, and to compare the Figma and Unity screenshots.
- The team decided to: use AI only for arranging the information and to mark with FILL IN everything that was not known, because the justification has to be based on the team's real decisions and not on invented text.
- The team verified the result by: reading each decision and change against its own Figma design and Unity app, and correcting what the AI wrote when it did not match (for example Decision 1).
- One thing the team still needs to check: that every HCI principle and testing evidence in `docs/design.md` is the team's own reasoning and points to a real test case or participant in `docs/testing.md`.