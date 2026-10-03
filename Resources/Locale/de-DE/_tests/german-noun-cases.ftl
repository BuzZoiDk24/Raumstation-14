test-de-noun-case-prototype = { DE-NAME($prototype, $case, $form) }
test-de-noun-case-entity = { DE-ARTICLE($entity, "genitive") } { DE-NAME($entity, "genitive", "weak") }
test-de-noun-case-renamed = { DE-NAME($entity, "genitive", "weak") }
# Variants with the same base name can share the parent's case forms.
ent-GermanNounCaseTestVariant = Rucksack
    .desc = Testvariante mit unverändertem Grundnamen.
# A differently named variant must never pick up the parent's noun case forms.
ent-GermanNounCaseTestRenamedVariant = Spezialbeutel
    .desc = Testobjekt für deutsche Namensformen.
