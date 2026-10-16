### Secret stash component. Stuff like potted plants, comfy chair cushions, etc...

comp-secret-stash-action-hide-success = Du versteckst { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    } in { PROPER($stash) ->
    [true] { $stash }
   *[false] { DE-ARTICLE($stash, "dative") } { DE-NAME($stash, "dative", "weak") }
    }.
comp-secret-stash-action-hide-container-not-empty = Hier ist ja schon etwas drin!?
comp-secret-stash-action-hide-item-too-big = { PROPER($item) ->
    [true] { $item }
   *[false] { CAPITALIZE(DE-ARTICLE($item, "nominative")) } { DE-NAME($item, "nominative", "weak") }
    } passt nicht in { PROPER($stash) ->
    [true] { $stash }
   *[false] { DE-ARTICLE($stash, "accusative") } { DE-NAME($stash, "accusative", "weak") }
    }.
comp-secret-stash-action-get-item-found-something = In { PROPER($stash) ->
    [true] { $stash }
   *[false] { DE-ARTICLE($stash, "dative") } { DE-NAME($stash, "dative", "weak") }
    } war etwas versteckt!
comp-secret-stash-on-examine-found-hidden-item = In „{$stashname}“ ist etwas versteckt!
comp-secret-stash-on-destroyed-popup = Etwas fällt aus { PROPER($stash) ->
    [true] { $stash }
   *[false] { DE-ARTICLE($stash, "dative") } { DE-NAME($stash, "dative", "weak") }
    }!

### Verbs
comp-secret-stash-verb-insert-into-stash = Gegenstand verstecken
comp-secret-stash-verb-insert-message-item-already-inside = In { PROPER($stash) ->
    [true] { $stash }
   *[false] { DE-ARTICLE($stash, "dative") } { DE-NAME($stash, "dative", "weak") }
    } liegt bereits ein Gegenstand.
comp-secret-stash-verb-insert-message-no-item = Verstecke { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    } in { PROPER($stash) ->
    [true] { $stash }
   *[false] { DE-ARTICLE($stash, "dative") } { DE-NAME($stash, "dative", "weak") }
    }.
comp-secret-stash-verb-take-out-item = Gegenstand herausnehmen
comp-secret-stash-verb-take-out-message-something = Nimm den Inhalt aus { PROPER($stash) ->
    [true] { $stash }
   *[false] { DE-ARTICLE($stash, "dative") } { DE-NAME($stash, "dative", "weak") }
    }.
comp-secret-stash-verb-take-out-message-nothing = In { PROPER($stash) ->
    [true] { $stash }
   *[false] { DE-ARTICLE($stash, "dative") } { DE-NAME($stash, "dative", "weak") }
    } befindet sich nichts.

comp-secret-stash-verb-close = Schließen
comp-secret-stash-verb-cant-close = Damit kannst du „{$stashname}“ nicht schließen.
comp-secret-stash-verb-open = Öffnen

### Stash names
secret-stash-plant = Pflanze
secret-stash-toilet = Spülkasten
secret-stash-plushie = Plüschtier
secret-stash-cake = Kuchen
