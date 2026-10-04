microwave-component-interact-using-no-power = Die Mikrowelle hat keinen Strom!
microwave-component-interact-using-broken = Die Mikrowelle ist kaputt!
microwave-component-interact-using-container-full = Der Behälter ist voll
microwave-component-interact-using-transfer-success = {$amount}u übertragen
microwave-component-interact-using-transfer-fail = Das geht nicht!
microwave-component-suicide-others-message = {$victim} versucht, den eigenen Kopf zu garen!
microwave-component-suicide-message = Du garst deinen Kopf!
microwave-component-interact-full = Die Mikrowelle ist voll.
microwave-component-interact-item-too-big = { PROPER($item) ->
    [true] { $item }
   *[false] { CAPITALIZE(DE-ARTICLE($item, "nominative")) } { DE-NAME($item, "nominative", "weak") }
    } { ATTRIB($item, "number") ->
    [plural] sind zu groß, um in die Mikrowelle zu passen!
   *[other] ist zu groß, um in die Mikrowelle zu passen!
    }
microwave-bound-user-interface-instant-button = SOFORT
microwave-bound-user-interface-cook-time-label = GARZEIT: {$time}
microwave-menu-title = Mikrowelle
microwave-menu-start-button = Start
microwave-menu-eject-all-text = Alles auswerfen
microwave-menu-eject-all-tooltip = Dabei werden alle Flüssigkeiten verdampft und alle festen Gegenstände ausgeworfen.
microwave-menu-instant-button = SOFORT
microwave-menu-footer-flavor-left = Keine elektronischen, metallischen oder lebenden Gegenstände hineinlegen.
microwave-menu-footer-flavor-right = v1.5
