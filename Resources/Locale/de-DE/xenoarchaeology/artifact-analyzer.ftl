analysis-console-menu-title = Breitband-Analysekonsole Mark 3
analysis-console-server-list-button = Server
analysis-console-extract-button = Punkte gewinnen

analysis-console-info-no-scanner = Kein Analysator verbunden! Verbinde einen mit einem Multitool.
analysis-console-info-no-artifact = Kein Artefakt vorhanden! Lege eines auf die Plattform, um die Knoteninformationen anzuzeigen.
analysis-console-info-ready = Systeme betriebsbereit. Bereit zum Scannen.

analysis-console-no-node = Wähle einen Knoten aus
analysis-console-info-id = [font="Monospace" size=11]ID:[/font]
analysis-console-info-id-value = [font="Monospace" size=11][color=yellow]{$id}[/color][/font]
analysis-console-info-class = [font="Monospace" size=11]Klasse:[/font]
analysis-console-info-class-value = [font="Monospace" size=11]{$class}[/font]
analysis-console-info-locked = [font="Monospace" size=11]Status:[/font]
analysis-console-info-locked-value = [font="Monospace" size=11][color={ $state ->
    [0] red]Gesperrt
    [1] lime]Freigeschaltet
    *[2] plum]Aktiv
}[/color][/font]
analysis-console-info-durability = [font="Monospace" size=11]Haltbarkeit:[/font]
analysis-console-info-durability-value = [font="Monospace" size=11][color={$color}]{$current}/{$max}[/color][/font]
analysis-console-info-effect = [font="Monospace" size=11]Wirkung:[/font]
analysis-console-info-effect-value = [font="Monospace" size=11][color=gray]{ $state ->
    [true] {$info}
    *[false] Schalte Knoten frei, um Informationen zu erhalten
}[/color][/font]
analysis-console-info-trigger = [font="Monospace" size=11]Auslöser:[/font]
analysis-console-info-triggered-value = [font="Monospace" size=11][color=gray]{$triggers}[/color][/font]
analysis-console-info-scanner = Scan läuft...
analysis-console-info-scanner-paused = Pausiert.
analysis-console-progress-text = {$seconds ->
    [one] T-{$seconds} Sekunde
    *[other] T-{$seconds} Sekunden
}

analysis-console-extract-value = [font="Monospace" size=11][color=orange]Knoten {$id} (+{$value})[/color][/font]
analysis-console-extract-none = [font="Monospace" size=11][color=orange] Keine freigeschalteten Knoten mit verbleibenden Forschungspunkten [/color][/font]
analysis-console-extract-sum = [font="Monospace" size=11][color=orange]Forschungspunkte insgesamt: {$value}[/color][/font]

analyzer-artifact-extract-popup = Energie flimmert auf der Oberfläche des Artefakts!
