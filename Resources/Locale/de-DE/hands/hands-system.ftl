# Examine text after when they're holding something (in-hand)
comp-hands-examine = { GENDER($user) ->
    [male] Er
    [female] Sie
    [neuter] Es
   *[other] { $user }
    } hält { $items } in den Händen.
comp-hands-examine-empty = { GENDER($user) ->
    [male] Er
    [female] Sie
    [neuter] Es
   *[other] { $user }
    } hält nichts in den Händen.
comp-hands-examine-wrapper = { DE-ARTICLE($itemEntity, "accusative", article: "indefinite") } [color=paleturquoise]{ $item }[/color]

hands-system-blocked-by = Blockiert durch
