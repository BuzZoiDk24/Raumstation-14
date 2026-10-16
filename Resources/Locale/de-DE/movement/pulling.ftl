getting-pulled-popup = { PROPER($puller) ->
    [true] { $puller }
   *[false] { CAPITALIZE(DE-ARTICLE($puller, "nominative")) } { DE-NAME($puller, "nominative", "weak") }
    } beginnt, dich zu ziehen.
