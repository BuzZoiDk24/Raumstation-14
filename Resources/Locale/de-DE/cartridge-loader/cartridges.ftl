device-pda-slot-component-slot-name-cartridge = Programmmodul

default-program-name = Programm
notekeeper-program-name = Notizprogramm
nano-task-program-name = NanoTask
news-read-program-name = Stationsnachrichten

crew-manifest-program-name = Besatzungsverzeichnis
crew-manifest-cartridge-loading = Wird geladen ...
crew-manifest-cartridge-loading-failed = Besatzungsverzeichnis konnte nicht geladen werden!

net-probe-program-name = NetProbe
net-probe-scan = {$device} gescannt!
net-probe-label-name = Name
net-probe-label-address = Adresse
net-probe-label-frequency = Frequenz
net-probe-label-network = Netzwerk

log-probe-program-name = LogProbe
log-probe-scan = Protokolle von {$device} heruntergeladen!
log-probe-label-time = Zeit
log-probe-label-accessor = Zugriff durch
log-probe-label-number = #
log-probe-print-button = Protokolle drucken
log-probe-printout-device = Gescanntes Gerät: {$name}
log-probe-printout-header = Letzte Protokolle:
log-probe-printout-entry = #{$number} / {$time} / {$accessor}

astro-nav-program-name = AstroNav

med-tek-program-name = MedTek

# NanoTask cartridge

nano-task-ui-heading-high-priority-tasks =
    { $amount ->
        [0] Keine Aufgaben mit hoher Priorität
        [one] 1 Aufgabe mit hoher Priorität
       *[other] {$amount} Aufgaben mit hoher Priorität
    }
nano-task-ui-heading-medium-priority-tasks =
    { $amount ->
        [0] Keine Aufgaben mit mittlerer Priorität
        [one] 1 Aufgabe mit mittlerer Priorität
       *[other] {$amount} Aufgaben mit mittlerer Priorität
    }
nano-task-ui-heading-low-priority-tasks =
    { $amount ->
        [0] Keine Aufgaben mit niedriger Priorität
        [one] 1 Aufgabe mit niedriger Priorität
       *[other] {$amount} Aufgaben mit niedriger Priorität
    }
nano-task-ui-done = Erledigt
nano-task-ui-revert-done = Rückgängig
nano-task-ui-priority-low = Niedrig
nano-task-ui-priority-medium = Mittel
nano-task-ui-priority-high = Hoch
nano-task-ui-cancel = Abbrechen
nano-task-ui-print = Drucken
nano-task-ui-delete = Löschen
nano-task-ui-save = Speichern
nano-task-ui-new-task = Neue Aufgabe
nano-task-ui-description-label = Beschreibung:
nano-task-ui-description-placeholder = Besorge etwas Wichtiges
nano-task-ui-requester-label = Auftraggeber:
nano-task-ui-requester-placeholder = John NanoTrasen
nano-task-ui-item-title = Aufgabe bearbeiten
nano-task-printed-description = [bold]Beschreibung[/bold]: {$description}
nano-task-printed-requester = [bold]Auftraggeber[/bold]: {$requester}
nano-task-printed-high-priority = [bold]Priorität[/bold]: [color=red]Hoch[/color]
nano-task-printed-medium-priority = [bold]Priorität[/bold]: Mittel
nano-task-printed-low-priority = [bold]Priorität[/bold]: Niedrig

# Wanted list cartridge
wanted-list-program-name = Fahndungsliste
wanted-list-label-no-records = Alles in Ordnung, Cowboy
wanted-list-search-placeholder = Nach Name und Status suchen

wanted-list-age-label = [color=darkgray]Alter:[/color] [color=white]{$age}[/color]
wanted-list-job-label = [color=darkgray]Beruf:[/color] [color=white]{$job}[/color]
wanted-list-species-label = [color=darkgray]Spezies:[/color] [color=white]{$species}[/color]
wanted-list-gender-label = [color=darkgray]Geschlecht:[/color] [color=white]{$gender}[/color]

wanted-list-reason-label = [color=darkgray]Grund:[/color] [color=white]{$reason}[/color]
wanted-list-unknown-reason-label = Unbekannter Grund

wanted-list-initiator-label = [color=darkgray]Eingetragen von:[/color] [color=white]{$initiator}[/color]
wanted-list-unknown-initiator-label = Unbekannt

wanted-list-status-label = [color=darkgray]Status:[/color] {$status ->
        [suspected] [color=yellow]verdächtig[/color]
        [wanted] [color=red]gesucht[/color]
        [detained] [color=#b18644]inhaftiert[/color]
        [paroled] [color=green]auf Bewährung[/color]
        [discharged] [color=green]entlassen[/color]
        [hostile] [color=darkred]feindlich[/color]
        [eliminated] [color=gray]ausgeschaltet[/color]
        *[other] Kein Eintrag
    }

wanted-list-history-table-time-col = Zeit
wanted-list-history-table-reason-col = Vergehen
wanted-list-history-table-initiator-col = Eingetragen von
