humanoid-appearance-component-unknown-species = Person
humanoid-appearance-component-examine = { GENDER($user) ->
    [male] Er
    [female] Sie
    [neuter] Es
   *[epicene] Die Person
} ist { $age } und gehört zur Spezies { CAPITALIZE($species) }.
