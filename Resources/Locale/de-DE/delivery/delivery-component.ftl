delivery-recipient-examine = Diese Sendung ist für {$recipient}, {$job}, bestimmt.
delivery-already-opened-examine = Sie wurde bereits geöffnet.
delivery-earnings-examine = Die Zustellung bringt der Station [color=yellow]{$spesos}[/color] Spesos ein.
delivery-recipient-no-name = Namenlos
delivery-recipient-no-job = Unbekannt

delivery-unlocked-self = Du schließt { PROPER($deliveryEntity) ->
    [true] { $deliveryEntity }
   *[false] { DE-ARTICLE($deliveryEntity, "accusative") } { DE-NAME($deliveryEntity, "accusative", "weak") }
    } mit deinem Fingerabdruck auf.
delivery-opened-self = Du öffnest { PROPER($deliveryEntity) ->
    [true] { $deliveryEntity }
   *[false] { DE-ARTICLE($deliveryEntity, "accusative") } { DE-NAME($deliveryEntity, "accusative", "weak") }
    }.
delivery-unlocked-others = { PROPER($recipient) ->
    [true] { $recipient }
   *[false] { CAPITALIZE(DE-ARTICLE($recipient, "nominative")) } { DE-NAME($recipient, "nominative", "weak") }
    } schließt { PROPER($deliveryEntity) ->
    [true] { $deliveryEntity }
   *[false] { DE-ARTICLE($deliveryEntity, "accusative") } { DE-NAME($deliveryEntity, "accusative", "weak") }
    } { DE-GENDER($possadj) ->
    [epicene] per Fingerabdruck
   *[other] mit { DE-POSS-ADJ(DE-GENDER($possadj), $bodySex, "dative", "masculine") } Fingerabdruck
    } auf.
delivery-opened-others = { PROPER($recipient) ->
    [true] { $recipient }
   *[false] { CAPITALIZE(DE-ARTICLE($recipient, "nominative")) } { DE-NAME($recipient, "nominative", "weak") }
    } öffnet { PROPER($deliveryEntity) ->
    [true] { $deliveryEntity }
   *[false] { DE-ARTICLE($deliveryEntity, "accusative") } { DE-NAME($deliveryEntity, "accusative", "weak") }
    }.

delivery-unlock-verb = Aufschließen
delivery-open-verb = Öffnen
delivery-slice-verb = Aufschneiden

delivery-teleporter-amount-examine = { $amount ->
    [one] Enthält [color=yellow]{$amount}[/color] Sendung.
   *[other] Enthält [color=yellow]{$amount}[/color] Sendungen.
    }
delivery-teleporter-empty = { PROPER($entity) ->
    [true] { $entity }
   *[false] { CAPITALIZE(DE-ARTICLE($entity, "nominative")) } { DE-NAME($entity, "nominative", "weak") }
    } ist leer.
delivery-teleporter-empty-verb = Post entnehmen


# modifiers
delivery-priority-examine = { PROPER($deliveryEntity) ->
    [true] { $deliveryEntity }
   *[false] { CAPITALIZE(DE-ARTICLE($deliveryEntity, "nominative")) } { DE-NAME($deliveryEntity, "nominative", "weak") }
    } hat [color=orange]Vorrang[/color]. Du hast noch [color=orange]{$time}[/color] Zeit, um die Sendung für einen Bonus zuzustellen.
delivery-priority-delivered-examine = { PROPER($deliveryEntity) ->
    [true] { $deliveryEntity }
   *[false] { CAPITALIZE(DE-ARTICLE($deliveryEntity, "nominative")) } { DE-NAME($deliveryEntity, "nominative", "weak") }
    } hat [color=orange]Vorrang[/color] und wurde rechtzeitig zugestellt.
delivery-priority-expired-examine = { PROPER($deliveryEntity) ->
    [true] { $deliveryEntity }
   *[false] { CAPITALIZE(DE-ARTICLE($deliveryEntity, "nominative")) } { DE-NAME($deliveryEntity, "nominative", "weak") }
    } hat [color=orange]Vorrang[/color]. Die Zustellfrist ist abgelaufen.

delivery-fragile-examine = Der Inhalt von { PROPER($deliveryEntity) ->
    [true] { $deliveryEntity }
   *[false] { DE-ARTICLE($deliveryEntity, "dative") } { DE-NAME($deliveryEntity, "dative", "weak") }
    } ist [color=red]zerbrechlich[/color]. Stelle die Sendung unbeschädigt zu, um einen Bonus zu erhalten.
delivery-fragile-broken-examine = Der Inhalt von { PROPER($deliveryEntity) ->
    [true] { $deliveryEntity }
   *[false] { DE-ARTICLE($deliveryEntity, "dative") } { DE-NAME($deliveryEntity, "dative", "weak") }
    } ist [color=red]zerbrechlich[/color] und sieht stark beschädigt aus.

delivery-bomb-examine = In { PROPER($deliveryEntity) ->
    [true] { $deliveryEntity }
   *[false] { DE-ARTICLE($deliveryEntity, "dative") } { DE-NAME($deliveryEntity, "dative", "weak") }
    } steckt eine [color=purple]Bombe[/color]. Oh nein.
delivery-bomb-primed-examine = Die [color=purple]Bombe[/color] in { PROPER($deliveryEntity) ->
    [true] { $deliveryEntity }
   *[false] { DE-ARTICLE($deliveryEntity, "dative") } { DE-NAME($deliveryEntity, "dative", "weak") }
    } ist scharf. Das hier zu lesen ist gerade keine gute Idee.
