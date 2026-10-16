fleeting-clothing-component-default-popup = { PROPER($item) ->
    [true] { $item }
   *[false] { CAPITALIZE(DE-ARTICLE($item, "nominative")) } { DE-NAME($item, "nominative", "weak") }
    } zerfällt zu Staub.
fleeting-clothing-component-default-examine = Dieser Gegenstand ist vergänglich. Er verschwindet, sobald du ihn ablegst.
