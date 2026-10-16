### Locale for wielding items; i.e. two-handing them

wieldable-verb-text-wield = Beidhändig halten
wieldable-verb-text-unwield = Einhändig halten

wieldable-component-successful-wield = Du hältst { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    } jetzt mit beiden Händen.
wieldable-component-failed-wield = Du hältst { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    } wieder mit einer Hand.
wieldable-component-successful-wield-other = { PROPER($user) ->
    [true] { $user }
   *[false] { CAPITALIZE(DE-ARTICLE($user, "nominative")) } { DE-NAME($user, "nominative", "weak") }
    } hält { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    } jetzt mit beiden Händen.
wieldable-component-failed-wield-other = { PROPER($user) ->
    [true] { $user }
   *[false] { CAPITALIZE(DE-ARTICLE($user, "nominative")) } { DE-NAME($user, "nominative", "weak") }
    } hält { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    } wieder mit einer Hand.
wieldable-component-blocked-wield = { PROPER($blocker) ->
    [true] { $blocker }
   *[false] { CAPITALIZE(DE-ARTICLE($blocker, "nominative")) } { DE-NAME($blocker, "nominative", "weak") }
    } hindert dich daran, { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    } beidhändig zu halten.

wieldable-component-no-hands = Du hast nicht genug Hände!
wieldable-component-not-enough-free-hands = { $number ->
    [one] Du brauchst eine freie Hand, um
   *[other] Du brauchst {$number} freie Hände, um
    } { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    } beidhändig zu halten.
wieldable-component-not-in-hands = Du hältst { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    } nicht in der Hand!

wieldable-component-requires = Du musst { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    } beidhändig halten!

gunwieldbonus-component-examine = Diese Waffe ist genauer, wenn du sie beidhändig hältst.

gunrequireswield-component-examine = Diese Waffe lässt sich nur beidhändig abfeuern.
