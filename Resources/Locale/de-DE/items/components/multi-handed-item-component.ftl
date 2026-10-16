multi-handed-item-pick-up-fail = { $number ->
    [one] Du brauchst noch eine freie Hand, um
   *[other] Du brauchst noch {$number} freie Hände, um
    } { PROPER($item) ->
    [true] { $item }
   *[false] { DE-ARTICLE($item, "accusative") } { DE-NAME($item, "accusative", "weak") }
    } aufzuheben.
