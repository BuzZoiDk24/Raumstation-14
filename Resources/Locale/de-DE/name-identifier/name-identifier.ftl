name-identifier-format-append = {$baseName} {$identifier}
name-identifier-format-prepend = { $capitalizePrefix ->
    [true] { CAPITALIZE(DE-ADJECTIVE($identifier, DE-GENDER($entity), $adjectiveDeclension, $nameCase, $number)) }
   *[false] { DE-ADJECTIVE($identifier, DE-GENDER($entity), $adjectiveDeclension, $nameCase, $number) }
    } {$baseName}
name-identifier-format-full = {$identifier}

name-identifier-test-1 = TestValue
