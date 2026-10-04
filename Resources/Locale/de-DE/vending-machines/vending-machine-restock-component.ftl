vending-machine-restock-invalid-inventory = Mit { PROPER($this) ->
    [true] { DE-NAME($this, "dative", "weak") }
   *[false] { DE-ARTICLE($this, "dative") } { DE-NAME($this, "dative", "weak") }
    } kannst du { PROPER($target) ->
    [true] { DE-NAME($target, "accusative", "weak") }
   *[false] { DE-ARTICLE($target, "accusative") } { DE-NAME($target, "accusative", "weak") }
    } nicht auffüllen.
vending-machine-restock-needs-panel-open = Du musst zuerst die Wartungsklappe öffnen, bevor du { PROPER($target) ->
    [true] { DE-NAME($target, "accusative", "weak") }
   *[false] { DE-ARTICLE($target, "accusative") } { DE-NAME($target, "accusative", "weak") }
    } auffüllen kannst.
vending-machine-restock-start-self = Du beginnst, { PROPER($target) ->
    [true] { DE-NAME($target, "accusative", "weak") }
   *[false] { DE-ARTICLE($target, "accusative") } { DE-NAME($target, "accusative", "weak") }
    } aufzufüllen.
vending-machine-restock-start-others = { CAPITALIZE($user) } beginnt, { PROPER($target) ->
    [true] { DE-NAME($target, "accusative", "weak") }
   *[false] { DE-ARTICLE($target, "accusative") } { DE-NAME($target, "accusative", "weak") }
    } aufzufüllen.
vending-machine-restock-done-self = Du hast { PROPER($target) ->
    [true] { DE-NAME($target, "accusative", "weak") }
   *[false] { DE-ARTICLE($target, "accusative") } { DE-NAME($target, "accusative", "weak") }
    } fertig aufgefüllt.
vending-machine-restock-done-others = { CAPITALIZE($user) } hat { PROPER($target) ->
    [true] { DE-NAME($target, "accusative", "weak") }
   *[false] { DE-ARTICLE($target, "accusative") } { DE-NAME($target, "accusative", "weak") }
    } fertig aufgefüllt.
