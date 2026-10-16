spray-component-is-empty-message = { PROPER($entity) ->
    [true] { $entity }
   *[false] { CAPITALIZE(DE-ARTICLE($entity, "nominative")) } { DE-NAME($entity, "nominative", "weak") }
    } ist leer!

pin-spray-popup-empty = { PROPER($entity) ->
    [true] { $entity }
   *[false] { CAPITALIZE(DE-ARTICLE($entity, "nominative")) } { DE-NAME($entity, "nominative", "weak") }
    } welkt und muss gegossen werden!
