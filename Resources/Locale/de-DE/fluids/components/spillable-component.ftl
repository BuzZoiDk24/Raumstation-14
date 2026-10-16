## SpillTargetVerb

spill-target-verb-get-data-text = Flüssigkeit ausgießen
spill-target-verb-activate-cannot-drain-message = Aus „{$owner}“ kannst du nichts ausgießen!
spill-target-verb-activate-is-empty-message = „{$owner}“ ist leer!

spill-melee-hit-attacker = Du schüttest {$amount}u aus { PROPER($spillable) ->
    [true] { $spillable }
   *[false] { DE-ARTICLE($spillable, "dative") } { DE-NAME($spillable, "dative", "weak") }
    } auf { PROPER($target) ->
    [true] { $target }
   *[false] { DE-ARTICLE($target, "accusative") } { DE-NAME($target, "accusative", "weak") }
    }!
spill-melee-hit-others = { PROPER($attacker) ->
    [true] { $attacker }
   *[false] { CAPITALIZE(DE-ARTICLE($attacker, "nominative")) } { DE-NAME($attacker, "nominative", "weak") }
    } schüttet etwas aus { PROPER($spillable) ->
    [true] { $spillable }
   *[false] { DE-ARTICLE($spillable, "dative") } { DE-NAME($spillable, "dative", "weak") }
    } auf { PROPER($target) ->
    [true] { $target }
   *[false] { DE-ARTICLE($target, "accusative") } { DE-NAME($target, "accusative", "weak") }
    }!

spill-land-spilled-on-other = Aus { PROPER($spillable) ->
    [true] { $spillable }
   *[false] { DE-ARTICLE($spillable, "dative") } { DE-NAME($spillable, "dative", "weak") }
    } schwappt etwas Flüssigkeit auf { PROPER($target) ->
    [true] { $target }
   *[false] { DE-ARTICLE($target, "accusative") } { DE-NAME($target, "accusative", "weak") }
    }!

spill-examine-is-spillable = Aus diesem Behälter könnte etwas verschüttet werden.
spill-examine-spillable-weapon = Mit einem Angriff könntest du jemandem den Inhalt entgegenschütten.
