ammonia-smell = Hier riecht etwas beißend!

## Verderbliche Gegenstände

perishable-1 = [color=green]Die Leiche wirkt noch frisch.[/color]
perishable-2 = [color=orangered]Die Leiche wirkt noch einigermaßen frisch.[/color]
perishable-3 = [color=red]Die Leiche wirkt nicht mehr besonders frisch.[/color]

perishable-1-nonmob = [color=green]{ PROPER($target) ->
    [true] { $target }
   *[false] { CAPITALIZE(DE-ARTICLE($target, "nominative")) } { DE-NAME($target, "nominative", "weak") }
    } { ATTRIB($target, "number") ->
    [plural] sehen noch frisch aus.
   *[other] sieht noch frisch aus.
    }[/color]
perishable-2-nonmob = [color=orangered]{ PROPER($target) ->
    [true] { $target }
   *[false] { CAPITALIZE(DE-ARTICLE($target, "nominative")) } { DE-NAME($target, "nominative", "weak") }
    } { ATTRIB($target, "number") ->
    [plural] sehen noch einigermaßen frisch aus.
   *[other] sieht noch einigermaßen frisch aus.
    }[/color]
perishable-3-nonmob = [color=red]{ PROPER($target) ->
    [true] { $target }
   *[false] { CAPITALIZE(DE-ARTICLE($target, "nominative")) } { DE-NAME($target, "nominative", "weak") }
    } { ATTRIB($target, "number") ->
    [plural] sehen nicht mehr besonders frisch aus.
   *[other] sieht nicht mehr besonders frisch aus.
    }[/color]

## Verwesung

rotting-rotting = [color=orange]Die Leiche verwest![/color]
rotting-bloated = [color=orangered]Die Leiche ist aufgebläht![/color]
rotting-extremely-bloated = [color=red]Die Leiche ist stark aufgebläht![/color]

rotting-rotting-nonmob = [color=orange]{ PROPER($target) ->
    [true] { $target }
   *[false] { CAPITALIZE(DE-ARTICLE($target, "nominative")) } { DE-NAME($target, "nominative", "weak") }
    } { ATTRIB($target, "number") ->
    [plural] verfaulen!
   *[other] verfault!
    }[/color]
rotting-bloated-nonmob = [color=orangered]{ PROPER($target) ->
    [true] { $target }
   *[false] { CAPITALIZE(DE-ARTICLE($target, "nominative")) } { DE-NAME($target, "nominative", "weak") }
    } { ATTRIB($target, "number") ->
    [plural] sind aufgebläht!
   *[other] ist aufgebläht!
    }[/color]
rotting-extremely-bloated-nonmob = [color=red]{ PROPER($target) ->
    [true] { $target }
   *[false] { CAPITALIZE(DE-ARTICLE($target, "nominative")) } { DE-NAME($target, "nominative", "weak") }
    } { ATTRIB($target, "number") ->
    [plural] sind stark aufgebläht!
   *[other] ist stark aufgebläht!
    }[/color]
