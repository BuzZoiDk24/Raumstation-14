scoopable-component-popup = Du schöpfst den Inhalt von { PROPER($scooped) ->
    [true] { $scooped }
   *[false] { DE-ARTICLE($scooped, "dative") } { DE-NAME($scooped, "dative", "weak") }
    } in { PROPER($beaker) ->
    [true] { $beaker }
   *[false] { DE-ARTICLE($beaker, "accusative") } { DE-NAME($beaker, "accusative", "weak") }
    }.
