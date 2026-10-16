catchable-component-success-self = Du fängst { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    }!
catchable-component-success-others = { PROPER($catcher) ->
    [true] { $catcher }
   *[false] { CAPITALIZE(DE-ARTICLE($catcher, "nominative")) } { DE-NAME($catcher, "nominative", "weak") }
    } fängt { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    }!
catchable-component-fail-self = Du schaffst es nicht, { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    } zu fangen!
catchable-component-fail-others = { PROPER($catcher) ->
    [true] { $catcher }
   *[false] { CAPITALIZE(DE-ARTICLE($catcher, "nominative")) } { DE-NAME($catcher, "nominative", "weak") }
    } schafft es nicht, { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    } zu fangen!
