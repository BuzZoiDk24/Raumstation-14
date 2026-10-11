test-de-counted-name = { DE-NAME($prototype, "nominative", "strong", count: $count) }
test-de-plural-name = { DE-NAME($prototype, $case, $form, number: "plural") }
test-de-plural-entity = { DE-ARTICLE($entity, "dative", "plural") } { DE-NAME($entity, "dative", "weak", number: "plural") }
ent-GermanPluralInheritedTest = { ent-ClothingBackpack }
ent-GermanPluralDifferentNameTest = Spezialbeutel
    .desc = Ein Beutel für den Grammatiktest.
    .gender = masculine
test-de-plural-entity-name = { DE-NAME($entity, "dative", "weak", number: "plural") }
