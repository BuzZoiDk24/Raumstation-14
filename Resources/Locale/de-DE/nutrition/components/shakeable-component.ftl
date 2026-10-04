shakeable-verb = Schütteln
shakeable-popup-message-others = { PROPER($user) ->
    [true] { $user }
   *[false] { CAPITALIZE(DE-ARTICLE($user, "nominative")) } { DE-NAME($user, "nominative", "weak") }
    } { ATTRIB($user, "number") ->
    [plural] schütteln
   *[other] schüttelt
    } { PROPER($shakeable) ->
    [true] { $shakeable }
   *[false] { DE-ARTICLE($shakeable, "accusative") } { DE-NAME($shakeable, "accusative", "weak") }
    }
shakeable-popup-message-self = Du schüttelst { PROPER($shakeable) ->
    [true] { $shakeable }
   *[false] { DE-ARTICLE($shakeable, "accusative") } { DE-NAME($shakeable, "accusative", "weak") }
    }
