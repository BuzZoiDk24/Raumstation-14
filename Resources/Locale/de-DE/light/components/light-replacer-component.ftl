### Interaction Messages

# Shown when player tries to replace light, but there are no lights left
comp-light-replacer-missing-light = In { PROPER($light-replacer) ->
    [true] { $light-replacer }
   *[false] { DE-ARTICLE($light-replacer, "dative") } { DE-NAME($light-replacer, "dative", "weak") }
    } ist kein Ersatz für „{$light-name}“ mehr vorhanden.

# Shown when player tries to insert a broken light bulb into the light replacer.
comp-light-replacer-insert-broken-light = Kaputte Leuchtmittel kannst du nicht einsetzen!

# Shown when a player attempts to replace a light with the same color & type as the active light.
comp-light-replacer-same-light = In dieser Fassung steckt bereits { DE-ARTICLE($light, "nominative", article: "indefinite") } { DE-NAME($light, "nominative", "indefinite") }!

# Radial Menu messages

comp-light-replacer-open-empty = { PROPER($light-replacer) ->
    [true] { $light-replacer }
   *[false] { CAPITALIZE(DE-ARTICLE($light-replacer, "nominative")) } { DE-NAME($light-replacer, "nominative", "weak") }
    } ist völlig leer!

# Label
comp-light-replacer-label = Röhre: {$tube}
                            Glühbirne: {$bulb}

### Examine

### Status Control

# Bulbs
comp-light-bulb-incandescent = Glühlampe
comp-light-bulb-dim = gedämpft
comp-light-bulb-warm = warm
comp-light-bulb-service = Stationsservice

# Tubes
comp-light-bulb-fluorescent = Leuchtstoff
comp-light-bulb-exterior = Außenbeleuchtung
comp-light-bulb-sodium = Natriumdampf

# Both
comp-light-bulb-old = alt
comp-light-bulb-led = LED
comp-light-bulb-cyan = cyan
comp-light-bulb-blue = blau
comp-light-bulb-yellow = gelb
comp-light-bulb-pink = rosa
comp-light-bulb-orange = orange
comp-light-bulb-black = schwarz
comp-light-bulb-red = rot
comp-light-bulb-green = grün
