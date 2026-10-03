comp-storage-no-item-size = k. A.
comp-storage-cant-insert = Das kannst du nicht hineinlegen.
comp-storage-too-big = Zu groß!
comp-storage-insufficient-capacity = Kein Platz!
comp-storage-invalid-container = Das gehört nicht dort hinein!
comp-storage-anchored-failure = Fest verankerte Gegenstände kannst du nicht verstauen.
comp-storage-cant-drop = { PROPER($entity) ->
    [true] Du kannst {$entity} nicht loslassen!
   *[false] { DE-GENDER($entity) ->
        [masculine] Du kannst { DE-ARTICLE($entity, "accusative") } { DE-NAME($entity, "accusative", "weak") } nicht loslassen!
        [feminine] Du kannst { DE-ARTICLE($entity, "accusative") } { DE-NAME($entity, "accusative", "weak") } nicht loslassen!
        [neuter] Du kannst { DE-ARTICLE($entity, "accusative") } { DE-NAME($entity, "accusative", "weak") } nicht loslassen!
        [male] Du kannst { DE-ARTICLE($entity, "accusative") } { DE-NAME($entity, "accusative", "weak") } nicht loslassen!
        [female] Du kannst { DE-ARTICLE($entity, "accusative") } { DE-NAME($entity, "accusative", "weak") } nicht loslassen!
       *[other] Das kannst du nicht loslassen!
    }
}
comp-storage-window-title = Behälter
comp-storage-window-weight = {$weight}/{$maxWeight}, Maximalgröße: {$size}
comp-storage-window-slots = Plätze: {$itemCount}/{$maxCount}, Maximalgröße: {$size}
comp-storage-window-dummy = Platzhalter
comp-storage-verb-open-storage = Behälter öffnen
comp-storage-verb-close-storage = Behälter schließen
