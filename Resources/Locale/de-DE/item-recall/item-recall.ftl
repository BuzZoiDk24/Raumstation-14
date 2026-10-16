item-recall-marked-name = { CAPITALIZE(DE-NAME($item, "nominative", "strong")) } zurückrufen
item-recall-marked-description = Rufe { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    } zurück in deine Hand.

item-recall-item-marked = Du zeichnest ein magisches Siegel auf { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    }.
item-recall-item-already-marked = { PROPER($item) ->
    [true] { $item }
   *[false] { CAPITALIZE(DE-ARTICLE($item, "nominative")) } { DE-NAME($item, "nominative", "weak") }
    } ist bereits markiert!
item-recall-item-mark-empty = Du musst einen Gegenstand in der Hand halten!
item-recall-item-summon-self = { PROPER($item) ->
    [true] { $item }
   *[false] { CAPITALIZE(DE-ARTICLE($item, "nominative")) } { DE-NAME($item, "nominative", "weak") }
    } erscheint in deiner Hand!
item-recall-item-summon-others = { PROPER($item) ->
    [true] { $item }
   *[false] { CAPITALIZE(DE-ARTICLE($item, "nominative")) } { DE-NAME($item, "nominative", "weak") }
    } erscheint in der Hand von { PROPER($name) ->
    [true] { $name }
   *[false] { DE-ARTICLE($name, "dative") } { DE-NAME($name, "dative", "weak") }
    }!
item-recall-item-disappear = { PROPER($item) ->
    [true] { $item }
   *[false] { CAPITALIZE(DE-ARTICLE($item, "nominative")) } { DE-NAME($item, "nominative", "weak") }
    } verschwindet!
item-recall-item-unmark = Du spürst, wie deine Verbindung zu { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "dative") } { DE-NAME($item, "dative", "weak") }
    } abreißt.

