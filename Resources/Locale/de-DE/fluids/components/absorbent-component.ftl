mopping-system-target-container-empty = { PROPER($target) ->
    [true] { $target }
   *[false] { CAPITALIZE(DE-ARTICLE($target, "nominative")) } { DE-NAME($target, "nominative", "weak") }
    } ist leer!
mopping-system-target-container-empty-water = { PROPER($target) ->
    [true] { $target }
   *[false] { CAPITALIZE(DE-ARTICLE($target, "nominative")) } { DE-NAME($target, "nominative", "weak") }
    } enthält kein Wasser!
mopping-system-puddle-space = { PROPER($used) ->
    [true] { $used }
   *[false] { CAPITALIZE(DE-ARTICLE($used, "nominative")) } { DE-NAME($used, "nominative", "weak") }
    } ist mit Wasser vollgesogen.
mopping-system-puddle-already-mopped = { PROPER($target) ->
    [true] { $target }
   *[false] { CAPITALIZE(DE-ARTICLE($target, "nominative")) } { DE-NAME($target, "nominative", "weak") }
    } wurde bereits gewischt.
mopping-system-no-water = { PROPER($used) ->
    [true] { $used }
   *[false] { CAPITALIZE(DE-ARTICLE($used, "nominative")) } { DE-NAME($used, "nominative", "weak") }
    } enthält kein Wasser!
mopping-system-no-hands = Du hast keine Hände!

mopping-system-full = { PROPER($used) ->
    [true] { $used }
   *[false] { CAPITALIZE(DE-ARTICLE($used, "nominative")) } { DE-NAME($used, "nominative", "weak") }
    } ist voll!
mopping-system-empty = { PROPER($used) ->
    [true] { $used }
   *[false] { CAPITALIZE(DE-ARTICLE($used, "nominative")) } { DE-NAME($used, "nominative", "weak") }
    } ist leer!
