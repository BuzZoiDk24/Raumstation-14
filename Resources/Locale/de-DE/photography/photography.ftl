# TODO: Make this a fluent function in RT
photograph-name-text = Dies ist ein Foto von { PROPER($entity) ->
    [true] { $entity }
   *[false] { DE-ARTICLE($entity, "dative", article: "indefinite") } { DE-NAME($entity, "dative", "indefinite") }
    }.
photograph-name-text-empty = Dies ist ein Foto.
photograph-name-text-photograph = Dies ist ein Foto von einem anderen Foto.
