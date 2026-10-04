injector-volume-transfer-label = Volumen: [color=white]{$currentVolume}/{$totalVolume}u[/color]
    Modus: [color=white]{$modeString}[/color] ([color=white]{$transferVolume}u[/color])
injector-volume-label = Volumen: [color=white]{$currentVolume}/{$totalVolume}u[/color]
    Modus: [color=white]{$modeString}[/color]
injector-toggle-verb-text = Modus wechseln
injector-component-inject-mode-name = Injizieren
injector-component-draw-mode-name = Aufziehen
injector-component-dynamic-mode-name = Automatisch
injector-component-mode-changed-text = Jetzt im Modus „{$mode}“.
injector-component-transfer-success-message = Du überträgst {$amount}u in { PROPER($target) ->
    [true] { $target }
   *[false] { DE-ARTICLE($target, "accusative") } { DE-NAME($target, "accusative", "weak") }
    }.
injector-component-transfer-success-message-self = Du überträgst {$amount}u in deinen Körper.
injector-component-inject-success-message = Du injizierst {$amount}u in { PROPER($target) ->
    [true] { $target }
   *[false] { DE-ARTICLE($target, "accusative") } { DE-NAME($target, "accusative", "weak") }
    }!
injector-component-inject-success-message-self = Du injizierst dir {$amount}u!
injector-component-draw-success-message = Du entnimmst { PROPER($target) ->
    [true] { $target }
   *[false] { DE-ARTICLE($target, "dative") } { DE-NAME($target, "dative", "weak") }
    } {$amount}u.
injector-component-draw-success-message-self = Du entnimmst dir {$amount}u.
injector-component-target-already-full-message = { PROPER($target) ->
    [true] { $target }
   *[false] { CAPITALIZE(DE-ARTICLE($target, "nominative")) } { DE-NAME($target, "nominative", "weak") }
    } { ATTRIB($target, "number") ->
    [plural] sind bereits voll!
   *[other] ist bereits voll!
    }
injector-component-target-already-full-message-self = Du kannst keine weitere Flüssigkeit aufnehmen!
injector-component-target-is-empty-message = { PROPER($target) ->
    [true] { $target }
   *[false] { CAPITALIZE(DE-ARTICLE($target, "nominative")) } { DE-NAME($target, "nominative", "weak") }
    } { ATTRIB($target, "number") ->
    [plural] sind leer!
   *[other] ist leer!
    }
injector-component-target-is-empty-message-self = Dir lässt sich keine Flüssigkeit entnehmen!
injector-component-cannot-toggle-draw-message = Zu voll zum Aufziehen!
injector-component-cannot-toggle-inject-message = Nichts zum Injizieren!
injector-component-cannot-toggle-dynamic-message = Der automatische Modus lässt sich nicht aktivieren!
injector-component-empty-message = { PROPER($injector) ->
    [true] { $injector }
   *[false] { CAPITALIZE(DE-ARTICLE($injector, "nominative")) } { DE-NAME($injector, "nominative", "weak") }
    } { ATTRIB($injector, "number") ->
    [plural] sind leer!
   *[other] ist leer!
    }
injector-component-blocked-user = Die Schutzausrüstung hat deine Injektion verhindert!
injector-component-blocked-other = Die Schutzausrüstung von { PROPER($target) ->
    [true] { $target }
   *[false] { DE-ARTICLE($target, "dative") } { DE-NAME($target, "dative", "weak") }
    } hat die Injektion durch { PROPER($user) ->
    [true] { $user }
   *[false] { DE-ARTICLE($user, "accusative") } { DE-NAME($user, "accusative", "weak") }
    } verhindert!
injector-component-cannot-transfer-message = Du kannst keine Flüssigkeit in { PROPER($target) ->
    [true] { $target }
   *[false] { DE-ARTICLE($target, "accusative") } { DE-NAME($target, "accusative", "weak") }
    } übertragen!
injector-component-cannot-transfer-message-self = Du kannst keine Flüssigkeit in deinen Körper übertragen!
injector-component-cannot-inject-message = Du kannst { PROPER($target) ->
    [true] { $target }
   *[false] { DE-ARTICLE($target, "dative") } { DE-NAME($target, "dative", "weak") }
    } nichts injizieren!
injector-component-cannot-inject-message-self = Du kannst dir nichts injizieren!
injector-component-cannot-draw-message = Du kannst { PROPER($target) ->
    [true] { $target }
   *[false] { DE-ARTICLE($target, "dative") } { DE-NAME($target, "dative", "weak") }
    } keine Flüssigkeit entnehmen!
injector-component-cannot-draw-message-self = Du kannst dir keine Flüssigkeit entnehmen!
injector-component-ignore-mobs = Dieser Injektor kann nur mit Behältern verwendet werden!
injector-component-needle-injecting-user = Du beginnst, die Nadel einzuführen.
injector-component-needle-injecting-target = { PROPER($user) ->
    [true] { $user }
   *[false] { CAPITALIZE(DE-ARTICLE($user, "nominative")) } { DE-NAME($user, "nominative", "weak") }
    } { ATTRIB($user, "number") ->
    [plural] versuchen
   *[other] versucht
    }, dir eine Nadel einzuführen!
injector-component-needle-drawing-user = Du beginnst, mit der Nadel Flüssigkeit zu entnehmen.
injector-component-needle-drawing-target = { PROPER($user) ->
    [true] { $user }
   *[false] { CAPITALIZE(DE-ARTICLE($user, "nominative")) } { DE-NAME($user, "nominative", "weak") }
    } { ATTRIB($user, "number") ->
    [plural] versuchen
   *[other] versucht
    }, dir mit einer Nadel Flüssigkeit zu entnehmen!
injector-component-spray-injecting-user = Du beginnst, die Sprühdüse vorzubereiten.
injector-component-spray-injecting-target = { PROPER($user) ->
    [true] { $user }
   *[false] { CAPITALIZE(DE-ARTICLE($user, "nominative")) } { DE-NAME($user, "nominative", "weak") }
    } { ATTRIB($user, "number") ->
    [plural] versuchen
   *[other] versucht
    }, eine Sprühdüse an deinen Körper zu halten!
injector-component-feel-prick-message = Du spürst einen kleinen Stich!
