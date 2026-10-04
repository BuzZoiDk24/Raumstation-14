spike-solution-generic = Du versetzt { PROPER($spiked-entity) ->
    [true] { $spiked-entity }
   *[false] { DE-ARTICLE($spiked-entity, "accusative") } { DE-NAME($spiked-entity, "accusative", "weak") }
    } mit { PROPER($spike-entity) ->
    [true] { $spike-entity }
   *[false] { DE-ARTICLE($spike-entity, "dative") } { DE-NAME($spike-entity, "dative", "weak") }
    }.
spike-solution-empty-generic = { PROPER($spike-entity) ->
    [true] { $spike-entity }
   *[false] { CAPITALIZE(DE-ARTICLE($spike-entity, "nominative")) } { DE-NAME($spike-entity, "nominative", "weak") }
    } { ATTRIB($spike-entity, "number") ->
    [plural] lösen sich
   *[other] löst sich
    } nicht in { PROPER($spiked-entity) ->
    [true] { $spiked-entity }
   *[false] { DE-ARTICLE($spiked-entity, "dative") } { DE-NAME($spiked-entity, "dative", "weak") }
    } auf.
spike-solution-egg = Du schlägst { PROPER($spike-entity) ->
    [true] { $spike-entity }
   *[false] { DE-ARTICLE($spike-entity, "accusative") } { DE-NAME($spike-entity, "accusative", "weak") }
    } in { PROPER($spiked-entity) ->
    [true] { $spiked-entity }
   *[false] { DE-ARTICLE($spiked-entity, "accusative") } { DE-NAME($spiked-entity, "accusative", "weak") }
    } auf.
spike-solution-mix = Du mischst { PROPER($spike-entity) ->
    [true] { $spike-entity }
   *[false] { DE-ARTICLE($spike-entity, "accusative") } { DE-NAME($spike-entity, "accusative", "weak") }
    } in { PROPER($spiked-entity) ->
    [true] { $spiked-entity }
   *[false] { DE-ARTICLE($spiked-entity, "accusative") } { DE-NAME($spiked-entity, "accusative", "weak") }
    }.
