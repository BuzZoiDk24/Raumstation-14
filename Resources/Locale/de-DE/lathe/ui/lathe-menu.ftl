lathe-menu-title = Fertigungsmenü
lathe-menu-queue = Warteschlange
lathe-menu-server-list = Serverliste
lathe-menu-sync = Synchronisieren
lathe-menu-search-designs = Baupläne suchen
lathe-menu-category-all = Alle
lathe-menu-search-filter = Filter:
lathe-menu-amount = Anzahl:
lathe-menu-recipe-count = { $count ->
    [1] {$count} Bauplan
    *[other] {$count} Baupläne
}
lathe-menu-reagent-slot-examine = An der Seite befindet sich ein Schacht für einen Becher.
lathe-reagent-dispense-no-container = Flüssigkeit läuft aus { DE-ARTICLE($name, "dative") } { DE-NAME($name, "dative", "weak") } auf den Boden!
lathe-menu-result-reagent-display = {$reagent} ({$amount}u)
lathe-menu-material-display = {$material} ({$amount})
lathe-menu-tooltip-display = {$amount} {$material}
lathe-menu-description-display = [italic]{$description}[/italic]
lathe-menu-material-amount = { $amount ->
    [1] {NATURALFIXED($amount, 2)} {$unit}
    *[other] {NATURALFIXED($amount, 2)} {MAKEPLURAL($unit)}
}
lathe-menu-material-amount-missing = { $amount ->
    [1] {NATURALFIXED($amount, 2)} {$unit} {$material} ([color=red]noch {NATURALFIXED($missingAmount, 2)} {MANY($unit, $missingAmount)} benötigt[/color])
    *[other] {NATURALFIXED($amount, 2)} {MAKEPLURAL($unit)} {$material} ([color=red]noch {NATURALFIXED($missingAmount, 2)} {MANY($unit, $missingAmount)} benötigt[/color])
}
lathe-menu-no-materials-message = Keine Materialien geladen.
lathe-menu-silo-linked-message = Mit Materialsilo verbunden
lathe-menu-fabricating-message = Wird hergestellt...
lathe-menu-materials-title = Materialien
lathe-menu-queue-title = Fertigungswarteschlange
lathe-menu-delete-fabricating-tooltip = Herstellung des aktuellen Gegenstands abbrechen.
lathe-menu-delete-item-tooltip = Herstellung dieser Serie abbrechen.
lathe-menu-move-up-tooltip = Diese Serie in der Warteschlange nach vorne verschieben.
lathe-menu-move-down-tooltip = Diese Serie in der Warteschlange nach hinten verschieben.
lathe-menu-item-single = {$index}. {$name}
lathe-menu-item-batch = {$index}. {$name} ({$printed}/{$total})
