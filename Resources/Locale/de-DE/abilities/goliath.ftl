tentacle-ability-use-popup = { PROPER($entity) ->
    [true] { $entity }
   *[false] { CAPITALIZE(DE-ARTICLE($entity, "nominative")) } { DE-NAME($entity, "nominative", "weak") }
    } gräbt { DE-GENDER($entity) ->
    [epicene] die
   *[other] { DE-POSS-ADJ(DE-GENDER($entity), $bodySex, "accusative", "plural") }
    } Tentakel in den Boden!
