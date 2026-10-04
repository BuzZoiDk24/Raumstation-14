mixing-verb-default-mix = mischen
mixing-verb-default-grind = zerkleinern
mixing-verb-default-juice = entsaften
mixing-verb-default-condense = kondensieren
mixing-verb-centrifuge = zentrifugieren
mixing-verb-electrolysis = elektrolysieren
mixing-verb-holy = segnen
mixing-verb-stir = umrühren
mixing-verb-shake = schütteln
default-mixing-success = Du mischst den Inhalt von { PROPER($mixed) ->
    [true] { $mixed }
   *[false] { DE-ARTICLE($mixed, "dative") } { DE-NAME($mixed, "dative", "weak") }
    } mit { PROPER($mixer) ->
    [true] { $mixer }
   *[false] { DE-ARTICLE($mixer, "dative") } { DE-NAME($mixer, "dative", "weak") }
    }
bible-mixing-success = Du segnest { PROPER($mixed) ->
    [true] { $mixed }
   *[false] { DE-ARTICLE($mixed, "accusative") } { DE-NAME($mixed, "accusative", "weak") }
    } mit { PROPER($mixer) ->
    [true] { $mixer }
   *[false] { DE-ARTICLE($mixer, "dative") } { DE-NAME($mixer, "dative", "weak") }
    }
spoon-mixing-success = Du rührst den Inhalt von { PROPER($mixed) ->
    [true] { $mixed }
   *[false] { DE-ARTICLE($mixed, "dative") } { DE-NAME($mixed, "dative", "weak") }
    } mit { PROPER($mixer) ->
    [true] { $mixer }
   *[false] { DE-ARTICLE($mixer, "dative") } { DE-NAME($mixer, "dative", "weak") }
    } um
handheld-centrifuge-success = Du trennst die Chemikalien in { PROPER($mixed) ->
    [true] { $mixed }
   *[false] { DE-ARTICLE($mixed, "dative") } { DE-NAME($mixed, "dative", "weak") }
    }
