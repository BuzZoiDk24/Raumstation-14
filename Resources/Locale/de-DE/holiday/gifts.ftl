gift-packin-contains = Dieses Geschenk scheint { $proper ->
    [true] { DE-NAME($prototype, "accusative", "indefinite") }
   *[false] { $number ->
        [plural] { DE-NAME($prototype, "accusative", "indefinite") }
       *[other] { DE-ARTICLE($gender, "accusative", $number, article: "indefinite") } { DE-NAME($prototype, "accusative", "indefinite") }
        }
    } zu enthalten.
christmas-tree-got-gift = Nach kurzem Suchen findest du ein Geschenk mit deinem Namen darauf!
christmas-tree-no-gift = Unter dem Baum liegt kein Geschenk für dich ...
