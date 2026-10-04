# UI

## Window

air-alarm-ui-title = Luftalarm

air-alarm-ui-access-denied = Keine ausreichende Zugriffsberechtigung!

air-alarm-ui-window-pressure-label = Druck
air-alarm-ui-window-temperature-label = Temperatur
air-alarm-ui-window-alarm-state-label = Status

air-alarm-ui-window-address-label = Adresse
air-alarm-ui-window-device-count-label = Geräte insgesamt
air-alarm-ui-window-resync-devices-label = Neu synchronisieren

air-alarm-ui-window-mode-label = Modus
air-alarm-ui-window-mode-select-locked-label = [bold][color=red] Fehler bei der Modusauswahl! [/color][/bold]
air-alarm-ui-window-auto-mode-label = Automatikmodus

-air-alarm-state-name = { $state ->
    [normal] Normal
    [warning] Warnung
    [danger] Gefahr
    [emagged] Manipuliert
   *[invalid] Ungültig
}

air-alarm-ui-window-listing-title = {$address} : {-air-alarm-state-name(state:$state)}
air-alarm-ui-window-pressure = {$pressure} kPa
air-alarm-ui-window-pressure-indicator = Druck: [color={$color}]{$pressure} kPa[/color]
air-alarm-ui-window-temperature = {$tempC} °C ({$temperature} K)
air-alarm-ui-window-temperature-indicator = Temperatur: [color={$color}]{$tempC} °C ({$temperature} K)[/color]
air-alarm-ui-window-alarm-state = [color={$color}]{-air-alarm-state-name(state:$state)}[/color]
air-alarm-ui-window-alarm-state-indicator = Status: [color={$color}]{-air-alarm-state-name(state:$state)}[/color]

air-alarm-ui-window-tab-vents = Luftauslässe
air-alarm-ui-window-tab-scrubbers = Luftfilter
air-alarm-ui-window-tab-sensors = Sensoren

air-alarm-ui-gases = {$gas}: {$amount} mol ({$percentage}%)
air-alarm-ui-gases-indicator = {$gas}: [color={$color}]{$amount} mol ({$percentage}%)[/color]

air-alarm-ui-mode-filtering = Filtern
air-alarm-ui-mode-wide-filtering = Filtern (großflächig)
air-alarm-ui-mode-fill = Füllen
air-alarm-ui-mode-panic = Panik
air-alarm-ui-mode-none = Keiner


air-alarm-ui-pump-direction-siphoning = Absaugen
air-alarm-ui-pump-direction-scrubbing = Filtern
air-alarm-ui-pump-direction-releasing = Einleiten

air-alarm-ui-pressure-bound-nobound = Keine Grenze
air-alarm-ui-pressure-bound-internalbound = Innere Grenze
air-alarm-ui-pressure-bound-externalbound = Äußere Grenze
air-alarm-ui-pressure-bound-both = Beide

air-alarm-ui-widget-gas-filters = Gasfilter

## Widgets

### General

air-alarm-ui-widget-enable = Aktiviert
air-alarm-ui-widget-copy = Einstellungen auf ähnliche Geräte übertragen
air-alarm-ui-widget-copy-tooltip = Überträgt die Einstellungen dieses Geräts auf alle Geräte in diesem Reiter des Luftalarms.
air-alarm-ui-widget-ignore = Ignorieren
air-alarm-ui-atmos-net-device-label = Adresse: {$address}

### Vent pumps

air-alarm-ui-vent-pump-label = Förderrichtung
air-alarm-ui-vent-pressure-label = Druckgrenze
air-alarm-ui-vent-external-bound-label = Äußere Druckgrenze
air-alarm-ui-vent-internal-bound-label = Innere Druckgrenze

### Scrubbers

air-alarm-ui-scrubber-pump-direction-label = Förderrichtung
air-alarm-ui-scrubber-volume-rate-label = Fördermenge (L)
air-alarm-ui-scrubber-wide-net-label = Großflächig filtern
air-alarm-ui-scrubber-select-all-gases-label = Alle auswählen
air-alarm-ui-scrubber-deselect-all-gases-label = Alle abwählen

### Thresholds

air-alarm-ui-sensor-gases = Gase
air-alarm-ui-sensor-thresholds = Grenzwerte
air-alarm-ui-thresholds-pressure-title = Grenzwerte (kPa)
air-alarm-ui-thresholds-temperature-title = Grenzwerte (K)
air-alarm-ui-thresholds-gas-title = Grenzwerte (%)
air-alarm-ui-thresholds-upper-bound = Gefahr oberhalb
air-alarm-ui-thresholds-lower-bound = Gefahr unterhalb
air-alarm-ui-thresholds-upper-warning-bound = Warnung oberhalb
air-alarm-ui-thresholds-lower-warning-bound = Warnung unterhalb
air-alarm-ui-thresholds-copy = Grenzwerte auf alle Geräte übertragen
air-alarm-ui-thresholds-copy-tooltip = Überträgt die Sensorgrenzwerte dieses Geräts auf alle Geräte in diesem Reiter des Luftalarms.
