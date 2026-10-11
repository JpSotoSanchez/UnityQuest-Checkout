# AI Use

**Authors:** José Pablo Soto Sánchez (UPSoto) and [second teammate's name]
**Course:** Human Computer Interaction, Universidad Panamericana

## How the team uses AI

AI is allowed in this course for Git commands, Unity code, debugging, design support and testing support. The team uses it as a help, not as the one who builds the project. The team decides what the feature should do first, writes its own draft, and then asks AI to review it, explain an error or suggest options. After that, the team chooses what to keep, change or throw away, and tests the result itself. AI answers never replace the team's own testing observations.

For each relevant use, the team records the tool, date, teammate, purpose, prompt, response used, modifications and verification, and links the related work.

---

## Entry 1: Reading and updating `Kits.json` to drive the buttons

- **Tool:** Claude (Anthropic)
- **Date:** 2026-10-09
- **Teammate:** José Pablo Soto Sánchez (UPSoto)
- **Purpose:** Unity code and debugging. The team wanted the Unity app to read a JSON file at startup and after certain changes, and use it to update the kit buttons (values and availability).
- **Related work:** `Assets/Scripts/KitManager.cs`, `Assets/Scripts/KitButton.cs`, `Assets/Scripts/Kits.cs`, `Kits.json`

**Prompt**

> Tengo duda de cómo deberían de funcionar estas cosas. Quiero que en ciertos momentos, al iniciar el programa de Unity y después de ciertos cambios, se lea/actualice el json y con eso los botones se actualicen con valores, disponibilidad y eso

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


## Entry 5: Choosing the rental duration with dropdowns

- **Tool:** Claude (Anthropic)
- **Date:** 2026-10-10
- **Teammate:** José Pablo Soto Sánchez (UPSoto)
- **Purpose:** Unity code. When a kit is selected, the user has to set a duration in days, hours and minutes using three dropdowns, and the kit's end date has to be the current date plus that duration.
- **Related work:** `Assets/Scripts/kitSelector.cs`, `Assets/Scripts/KitManager.cs`, `Assets/Scripts/KitButton.cs`, `Kits.json`

**Prompt**

> Al seleccionar un kit, estamos manejando que el usuario tiene que poner una duración  en Dias,horas,minutos
>
> Para seleccionar la duración habrá 3 dropdows, de día, hora y minuto
> El día es de 0 a 7, la hora es de 0-1-2-3-6-12 y los minutos solo opción de 0 a 30
>
> Los datos deberán ser recogidos de los dropdown que ya debe tener el código para seleccinar como serialized.
>
> Los kits tienen guardados una información de si están disponibles o no, según el estado en el que están. PAra esto, cuando se ponga la duración del kit, este deberá de poner que su duración hasta la que estará disponible sea la fecha actual + la que se ponga al seleccionar.
>
> Te paso el código del kit selector para que veas lo que ya tengo y lo acomodes a las instrucciones que te estoy dando.
> También más códigos, que son los que usamos para los json, el canvas manager, otras cosas de los kits.
>
> En el kit selector ve haciendo una funcion que, cuando esté completo el kit, actualice la información del kit correcto en el json

*(Attached: the team's own `kitSelector` with empty methods, `Kits.json` and `KitManager`.)*

**Response used**

The AI reorganized `kitSelector` with three serialized dropdowns, converted each dropdown index into its real value (days 0 to 7, hours 0/1/2/3/6/12, minutes 0 and 30), and calculated the end date as `DateTime.Now` plus the duration. It also added a method that updates the correct kit in the JSON through `KitManager`, rejects a duration of zero, and suggested that `KitButton` call `selector.KitSelected(kitNumber)`. It noticed that a stray `public` line in the team's code would not compile.

**Modifications**

The rules came from the team: the allowed values of each dropdown, the end date being now plus the selected duration, and the idea of updating the JSON only when the kit is complete. *(Fill in what the team changed from the AI's version.)*

**Verification**

- [ ] What the team tested: *(fill in, for example a short duration and checking the date saved in the JSON)*
- [ ] What actually happened: *(fill in)*

**What the team can explain**

- The team used AI to: organize the selector and calculate the end date from the dropdowns.
- The team decided to: save the end date as current date plus duration, because the kit state check in `KitManager` compares against the current time.
- The team verified the result by: *(fill in)*
- One thing the team still needs to check: *(fill in)*

---

## Entry 6: Reservation stages (kit, duration, checklist, review, confirm)

- **Tool:** Claude (Anthropic)
- **Date:** 2026-10-10
- **Teammate:** José Pablo Soto Sánchez (UPSoto)
- **Purpose:** Unity code and design support. The selector is used in several panels, so it has to keep all the selection variables and only write the JSON at the end.
- **Related work:** `Assets/Scripts/kitSelector.cs`, `Assets/Scripts/KitManager.cs`

**Prompt**

> Este kit selector lo estoy usando para más cosas, porque es también el que, en el botón, le pasaré la función que acutualice, por ejemplo slectedkit(int n){this.kitselected = n}, además de que para otro panel estoy ocupando que se verifique que está completo, así que necesito todas esas variables.
> Son diferentes stages, primero selecciona el kit, luego revisa la duración, luego verifica que esté completo con 4 checkboxes de headset, controlR, controlL y charger, finalmente una pestaña que te deja verificar todos los datos y finalmente es cuando se reserva y debe cambiar la información del json

**Response used**

The AI rewrote `kitSelector` around the team's stages: public variables for the kit, duration and the four checklist items, a `complete` value that is true only when all four toggles are checked, a method that fills a summary text for the review panel, and a confirmation method as the only place that writes the JSON. It also added `ReserveKit` to `KitManager`, which saves the state, end date and completeness in one write, and resets the selection after reserving.

**Modifications**

The stages and the four checklist items (headset, right controller, left controller, charger) were defined by the team. *(Fill in what the team changed, for example whether a kit with a missing item can still be reserved.)*

**Verification**

- [ ] What the team tested: *(fill in, for example a full reservation and a reservation with one item unchecked)*
- [ ] What actually happened: *(fill in)*

**What the team can explain**

- The team used AI to: structure the flow and avoid writing the JSON before the last stage.
- The team decided to: keep one script holding the whole selection, because several panels need the same data.
- The team verified the result by: *(fill in)*
- One thing the team still needs to check: *(fill in)*

---

## Entry 7: Reading the dropdowns that already exist in the scene

- **Tool:** Claude (Anthropic)
- **Date:** 2026-10-10
- **Teammate:** José Pablo Soto Sánchez (UPSoto)
- **Purpose:** Unity code. The dropdowns already exist in the scene with their options, so the script only has to read them when the confirmation button is pressed.
- **Related work:** `Assets/Scripts/kitSelector.cs`

**Prompt**

> El dropdown ya existe en escena con las opciones, es de CanvasUI que ya está en su panel correspondiente, solo quiero que jale los valores de ahí cuando se le pique al boton de confirmation de la escena.

**Response used**

The AI removed the code that filled the dropdowns and made the script read the text of the selected option and convert it to a number when the confirmation button is pressed. It also added checks for missing references and explained how to connect the dropdowns and the button in the Inspector.

**Modifications**

*(Fill in what the team changed, for example the text format of its dropdown options.)*

**Verification**

- [ ] What the team tested: *(fill in, for example each dropdown option and the saved end date)*
- [ ] What actually happened: *(fill in)*

**What the team can explain**

- The team used AI to: read the values from dropdowns that were already built.
- The team decided to: keep the options in the scene instead of in code, because they were already set up in the canvas.
- The team verified the result by: *(fill in)*
- One thing the team still needs to check: *(fill in)*

---

## Entry 8: Drafting this document

- **Tool:** Claude (Anthropic)
- **Date:** 2026-10-10
- **Teammate:** José Pablo Soto Sánchez (UPSoto)
- **Purpose:** Design/documentation support. The team used the course slides (`UP_HCI_Session20_IA_pptx.pdf`) and the required-document description to build `docs/ai-use.md`, and then asked the AI to keep it updated with each new use.
- **Related work:** [README.md](../README.md)

**Prompts**

> Haz mi ai-use.md con base a las instrucciones. Haz que parezca y que se vea como tal cual solo esto que necesito. También tienes que incluir este prompt y respuesta en ese documento.

> Modifica el documento para que se vea mucho más humano, que solo estoy usando la ia para ayuda y no que me haga todo y también incluye los otros prompts o puntos de esta conversación

> Cambia el documento a 3ra persona, porque somos 2 quienes modificamos esto

> Completa el ia report con esto ultimo que pedí

*(The first prompt included the instructions: "AI is allowed for Git commands, Unity code, debugging, design support and testing support. You must understand and verify what you use. For each relevant use, record the tool, date, teammate, purpose, prompt, response used, modifications and verification. Link the related work. AI assistance does not replace real testing observations.")*

**Response used**

The AI wrote the structure of this document, turned the prompts of this conversation into entries, rewrote the text in third person, and added the entries for the duration dropdowns, the reservation stages and the dropdown reading. This file is that response.

**Modifications**

*(Fill in what the team changed, and which placeholders it replaced with real results.)*

**Verification**

- [ ] The team checked every field against the course instructions.
- [ ] The team replaced all the "fill in" parts with its real actions and results.
- [ ] The related-work links point to real files in the repository.