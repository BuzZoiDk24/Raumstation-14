-entity-heater-setting-name = { $setting ->
    [off] aus
    [low] niedrig
    [medium] mittel
    [high] hoch
   *[other] unbekannt
}

entity-heater-examined = Es ist auf { $setting ->
    [off] [color=gray]{ -entity-heater-setting-name(setting: "off") }[/color]
    [low] [color=yellow]{ -entity-heater-setting-name(setting: "low") }[/color]
    [medium] [color=orange]{ -entity-heater-setting-name(setting: "medium") }[/color]
    [high] [color=red]{ -entity-heater-setting-name(setting: "high") }[/color]
   *[other] [color=purple]{ -entity-heater-setting-name(setting: "other") }[/color]
} eingestellt.
entity-heater-switch-setting = Auf { -entity-heater-setting-name(setting: $setting) } umschalten
entity-heater-switched-setting = Auf { -entity-heater-setting-name(setting: $setting) } umgestellt.
