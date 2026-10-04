pressurized-solution-spray-holder-self = Der Inhalt von { PROPER($drink) ->
    [true] { $drink }
   *[false] { DE-ARTICLE($drink, "dative") } { DE-NAME($drink, "dative", "weak") }
    } spritzt dir entgegen!
pressurized-solution-spray-holder-others = Der Inhalt von { PROPER($drink) ->
    [true] { $drink }
   *[false] { DE-ARTICLE($drink, "dative") } { DE-NAME($drink, "dative", "weak") }
    } spritzt auf { PROPER($victim) ->
    [true] { $victim }
   *[false] { DE-ARTICLE($victim, "accusative") } { DE-NAME($victim, "accusative", "weak") }
    }!
pressurized-solution-spray-ground = Der Inhalt von { PROPER($drink) ->
    [true] { $drink }
   *[false] { DE-ARTICLE($drink, "dative") } { DE-NAME($drink, "dative", "weak") }
    } spritzt heraus!
