buckle-component-no-hands-message = Du hast keine Hände!
buckle-component-already-buckled-message = Du bist bereits angeschnallt!
buckle-component-other-already-buckled-message = { PROPER($owner) ->
    [true] { $owner }
   *[false] { CAPITALIZE(DE-ARTICLE($owner, "nominative")) } { DE-NAME($owner, "nominative", "weak") }
    } ist bereits angeschnallt!
buckle-component-cannot-buckle-message = Hier kannst du dich nicht anschnallen!
buckle-component-other-cannot-buckle-message = Hier kannst du { PROPER($owner) ->
    [true] { $owner }
   *[false] { DE-ARTICLE($owner, "accusative") } { DE-NAME($owner, "accusative", "weak") }
    } nicht anschnallen!
buckle-component-cannot-fit-message = Du passt da nicht hinein!
buckle-component-other-cannot-fit-message = { PROPER($owner) ->
    [true] { $owner }
   *[false] { CAPITALIZE(DE-ARTICLE($owner, "nominative")) } { DE-NAME($owner, "nominative", "weak") }
    } passt da nicht hinein!
