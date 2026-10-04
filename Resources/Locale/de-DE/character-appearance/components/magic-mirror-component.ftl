magic-mirror-component-activate-user-has-no-hair = Du kannst keine Haare bekommen!

magic-mirror-add-slot-self = Du lässt dir Haare wachsen.
magic-mirror-remove-slot-self = Du entfernst einen Teil deiner Haare.
magic-mirror-change-slot-self = Du veränderst deine Frisur.
magic-mirror-change-color-self = Du veränderst deine Haarfarbe.

magic-mirror-add-slot-target = { PROPER($user) ->
    [true] { $user }
   *[false] { CAPITALIZE(DE-ARTICLE($user, "nominative")) } { DE-NAME($user, "nominative", "weak") }
    } lässt dir Haare wachsen.
magic-mirror-remove-slot-target = { PROPER($user) ->
    [true] { $user }
   *[false] { CAPITALIZE(DE-ARTICLE($user, "nominative")) } { DE-NAME($user, "nominative", "weak") }
    } schneidet dir die Haare ab.
magic-mirror-change-slot-target = { PROPER($user) ->
    [true] { $user }
   *[false] { CAPITALIZE(DE-ARTICLE($user, "nominative")) } { DE-NAME($user, "nominative", "weak") }
    } verändert deine Frisur.
magic-mirror-change-color-target = { PROPER($user) ->
    [true] { $user }
   *[false] { CAPITALIZE(DE-ARTICLE($user, "nominative")) } { DE-NAME($user, "nominative", "weak") }
    } verändert deine Haarfarbe.

magic-mirror-blocked-by-hat-self = Du musst deine Kopfbedeckung abnehmen, bevor du deine Haare verändern kannst.
magic-mirror-blocked-by-hat-self-target = Du versuchst, die Haare von { PROPER($target) ->
    [true] { $target }
   *[false] { DE-ARTICLE($target, "dative") } { DE-NAME($target, "dative", "weak") }
    } zu verändern, aber die Kleidung ist im Weg.
