contraband-examine-text-Minor =
    { $type ->
        *[item] [color={$color}]Für diesen Gegenstand gilt eine leichte Besitzbeschränkung.[/color]
        [reagent] [color={$color}]Für dieses Reagenz gilt eine leichte Besitzbeschränkung.[/color]
    }

contraband-examine-text-Restricted =
    { $type ->
        *[item] [color={$color}]Dieser Gegenstand ist bestimmten Abteilungen vorbehalten.[/color]
        [reagent] [color={$color}]Dieses Reagenz ist bestimmten Abteilungen vorbehalten.[/color]
    }

contraband-examine-text-Restricted-department =
    { $type ->
        *[item] [color={$color}]Dieser Gegenstand ist nur für {$departments} zugelassen und kann ansonsten als verbotene Ausrüstung gelten.[/color]
        [reagent] [color={$color}]Dieses Reagenz ist nur für {$departments} zugelassen und kann ansonsten als verboten gelten.[/color]
    }

contraband-examine-text-Major =
    { $type ->
        *[item] [color={$color}]Für diesen Gegenstand gilt eine strenge Besitzbeschränkung.[/color]
        [reagent] [color={$color}]Für dieses Reagenz gilt eine strenge Besitzbeschränkung.[/color]
    }

contraband-examine-text-GrandTheft =
    { $type ->
        *[item] [color={$color}]Dieser Gegenstand ist ein besonders begehrtes Ziel für Syndikatsagenten![/color]
        [reagent] [color={$color}]Dieses Reagenz ist ein besonders begehrtes Ziel für Syndikatsagenten![/color]
    }

contraband-examine-text-Highly-Illegal =
    { $type ->
        *[item] [color={$color}]Dieser Gegenstand ist streng verboten![/color]
        [reagent] [color={$color}]Dieses Reagenz ist streng verboten![/color]
    }

contraband-examine-text-Syndicate =
    { $type ->
        *[item] [color={$color}]Dieser Gegenstand ist streng verbotene Syndikatsausrüstung![/color]
        [reagent] [color={$color}]Dieses Reagenz ist streng verbotenes Syndikatsmaterial![/color]
    }

contraband-examine-text-Magical =
    { $type ->
        *[item] [color={$color}]Dieser magische Gegenstand ist streng verboten![/color]
        [reagent] [color={$color}]Dieses magische Reagenz ist streng verboten![/color]
    }

contraband-examine-text-avoid-carrying-around = [color=red][italic]Das solltest du ohne guten Grund besser nicht offen mit dir führen.[/italic][/color]
contraband-examine-text-in-the-clear = [color=green][italic]Es sollte kein Problem sein, das offen mit dir herumzutragen.[/italic][/color]

contraband-examinable-verb-text = Besitzberechtigung
contraband-examinable-verb-message = Prüfe, ob du diesen Gegenstand mitführen darfst.

contraband-department-plural = die Abteilung „{$department}“
contraband-job-plural = den Beruf „{$job}“
