## PointingSystem

pointing-system-try-point-cannot-reach = Du kommst nicht heran!
pointing-system-point-at-self = Du zeigst auf dich selbst.
pointing-system-point-at-other = { PROPER($other) ->
    [true] Du zeigst auf {$other}.
   *[false] Du zeigst auf { DE-ARTICLE(GENDER($other), "accusative") } {$other}.
}
pointing-system-point-at-self-others = {$otherName} zeigt auf sich selbst.
pointing-system-point-at-other-others = { PROPER($other) ->
    [true] {$otherName} zeigt auf {$other}.
   *[false] {$otherName} zeigt auf { DE-ARTICLE(GENDER($other), "accusative") } {$other}.
}
pointing-system-point-at-you-other = {$otherName} zeigt auf dich.
pointing-system-point-at-tile = Du zeigst auf den Boden ({$tileName}).
pointing-system-point-in-own-inventory-self = Du zeigst auf { DE-ARTICLE(GENDER($item), "accusative") } {$item} in deiner Ausrüstung.
pointing-system-point-in-own-inventory-others = {$pointer} zeigt auf { DE-ARTICLE(GENDER($item), "accusative") } {$item} in der eigenen Ausrüstung.
pointing-system-point-in-other-inventory-self = Du zeigst auf { DE-ARTICLE(GENDER($item), "accusative") } {$item} in der Ausrüstung von {$wearer}.
pointing-system-point-in-other-inventory-target = {$pointer} zeigt auf { DE-ARTICLE(GENDER($item), "accusative") } {$item} in deiner Ausrüstung.
pointing-system-point-in-other-inventory-others = {$pointer} zeigt auf { DE-ARTICLE(GENDER($item), "accusative") } {$item} in der Ausrüstung von {$wearer}.
pointing-system-other-point-at-tile = {$otherName} zeigt auf den Boden ({$tileName}).
