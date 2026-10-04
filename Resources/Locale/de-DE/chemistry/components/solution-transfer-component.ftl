comp-solution-transfer-fill-normal = Du füllst { PROPER($target) ->
    [true] { $target }
   *[false] { DE-ARTICLE($target, "accusative") } { DE-NAME($target, "accusative", "weak") }
    } mit {$amount}u aus { PROPER($owner) ->
    [true] { $owner }
   *[false] { DE-ARTICLE($owner, "dative") } { DE-NAME($owner, "dative", "weak") }
    }.
comp-solution-transfer-fill-fully = Du füllst { PROPER($target) ->
    [true] { $target }
   *[false] { DE-ARTICLE($target, "accusative") } { DE-NAME($target, "accusative", "weak") }
    } mit {$amount}u aus { PROPER($owner) ->
    [true] { $owner }
   *[false] { DE-ARTICLE($owner, "dative") } { DE-NAME($owner, "dative", "weak") }
    } bis zum Rand.
comp-solution-transfer-transfer-solution = Du überträgst {$amount}u in { PROPER($target) ->
    [true] { $target }
   *[false] { DE-ARTICLE($target, "accusative") } { DE-NAME($target, "accusative", "weak") }
    }.
comp-solution-transfer-is-empty = { PROPER($target) ->
    [true] { $target }
   *[false] { CAPITALIZE(DE-ARTICLE($target, "nominative")) } { DE-NAME($target, "nominative", "weak") }
    } { ATTRIB($target, "number") ->
    [plural] sind leer!
   *[other] ist leer!
    }
comp-solution-transfer-is-full = { PROPER($target) ->
    [true] { $target }
   *[false] { CAPITALIZE(DE-ARTICLE($target, "nominative")) } { DE-NAME($target, "nominative", "weak") }
    } { ATTRIB($target, "number") ->
    [plural] sind voll!
   *[other] ist voll!
    }
comp-solution-transfer-verb-custom-amount = Benutzerdefiniert
comp-solution-transfer-verb-amount = {$amount}u
comp-solution-transfer-verb-toggle = Auf {$amount}u umstellen
comp-solution-transfer-set-amount = Die Übertragungsmenge wurde auf {$amount}u eingestellt.
comp-solution-transfer-set-amount-max = Max: {$amount}u
comp-solution-transfer-set-amount-min = Min: {$amount}u
