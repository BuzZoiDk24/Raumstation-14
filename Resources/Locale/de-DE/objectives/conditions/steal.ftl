objective-condition-steal-title-no-owner =
    { $itemGender ->
        [proper] Stiehl {$itemName}.
       *[other] Stiehl { DE-ARTICLE($itemGender, "accusative", $itemNumber) } {$itemObject}.
    }
objective-condition-steal-title-alive-no-owner =
    { $itemGender ->
        [proper] Entführe {$itemName} lebend.
       *[other] Entführe { DE-ARTICLE($itemGender, "accusative", $itemNumber) } {$itemObject} lebend.
    }
objective-condition-steal-title =
    { $itemGender ->
        [proper] Stiehl {$itemName} ({$owner}).
       *[other] Stiehl { DE-ARTICLE($itemGender, "accusative", $itemNumber) } {$itemObject} ({$owner}).
    }
objective-condition-steal-description =
    { $itemGender ->
        [proper] Besorge {$itemName} für das Syndikat. Lass dich nicht erwischen.
       *[other] Besorge { DE-ARTICLE($itemGender, "accusative", $itemNumber) } {$itemObject} für das Syndikat. Lass dich nicht erwischen.
    }

objective-condition-steal-station = Station
objective-condition-steal-Ian = Ian, der Corgi der Personalleitung

objective-condition-thief-description =
    { $itemGender ->
        [proper] {$itemName} würde sich gut in meiner Sammlung machen!
       *[other] { CAPITALIZE(DE-ARTICLE($itemGender, "nominative", $itemNumber)) } {$itemSubject} würde sich gut in meiner Sammlung machen!
    }
objective-condition-thief-animal-description =
    { $itemGender ->
        [proper] {$itemName} würde sich gut in meiner Sammlung machen. Hauptsache lebendig!
       *[other] { CAPITALIZE(DE-ARTICLE($itemGender, "nominative", $itemNumber)) } {$itemSubject} würde sich gut in meiner Sammlung machen. Hauptsache lebendig!
    }
objective-condition-thief-multiply-description = Ich muss {$count} {$itemPlural} sammeln und mitnehmen.
