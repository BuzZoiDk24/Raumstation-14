comp-pick-random-empty = { PROPER($storage) ->
    [true] { $storage }
   *[false] { CAPITALIZE(DE-ARTICLE($storage, "nominative")) } { DE-NAME($storage, "nominative", "weak") }
    } ist leer!
comp-pick-random-verb-text = Gegenstand nehmen
