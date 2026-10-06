machine-insert-item = { DE-ARTICLE($user, "nominative") ->
    [der] Der { DE-NAME($user, "nominative", "weak") }
    [die] Die { DE-NAME($user, "nominative", "weak") }
    [das] Das { DE-NAME($user, "nominative", "weak") }
   *[other] {$user}
} setzt { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") } in { DE-ARTICLE($machine, "accusative") } { DE-NAME($machine, "accusative", "weak") } ein.

machine-upgrade-examinable-verb-text = Aufrüstungen
machine-upgrade-examinable-verb-message = Maschinenaufrüstungen untersuchen.
machine-upgrade-increased-by-percentage = [color=yellow]{CAPITALIZE($upgraded)}[/color] um {$percent}% erhöht.
machine-upgrade-decreased-by-percentage = [color=yellow]{CAPITALIZE($upgraded)}[/color] um {$percent}% verringert.
machine-upgrade-increased-by-amount = [color=yellow]{CAPITALIZE($upgraded)}[/color] um {$difference} erhöht.
machine-upgrade-decreased-by-amount = [color=yellow]{CAPITALIZE($upgraded)}[/color] um {$difference} verringert.
machine-upgrade-not-upgraded = [color=yellow]{CAPITALIZE($upgraded)}[/color] nicht aufgerüstet.

machine-part-name-capacitor = Kondensator
machine-part-name-manipulator = Manipulator
machine-part-name-matter-bin = Materialbehälter
machine-part-name-power-cell = Energiezelle

two-way-lever-left = Nach links drücken
two-way-lever-right = Nach rechts drücken
two-way-lever-cant = Der Hebel lässt sich nicht in diese Richtung drücken!

recycler-count-items = {$items ->
    [one] {$items} Gegenstand recycelt.
   *[other] {$items} Gegenstände recycelt.
}

machine-already-in-use = {CAPITALIZE(DE-ARTICLE($machine, "nominative"))} { DE-NAME($machine, "nominative", "weak") } wird bereits benutzt.
