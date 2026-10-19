## Survivor

roles-antag-survivor-name = Survivor
# It's a Halo reference
roles-antag-survivor-objective = Current Objective: Survive

survivor-role-greeting =
    You are a Survivor. Above all you need to make it back to Central Command alive.
    Collect as much firepower as needed to guarantee your survival.
    Trust no one.

survivor-round-end-dead-count =
{
    $deadCount ->
        [one] [color=red]{$deadCount}[/color] survivor died.
        *[other] [color=red]{$deadCount}[/color] survivors died.
}

survivor-round-end-alive-count =
{
    $aliveCount ->
        [one] [color=yellow]{$aliveCount}[/color] survivor was marooned on the station.
        *[other] [color=yellow]{$aliveCount}[/color] survivors were marooned on the station.
}

survivor-round-end-alive-on-shuttle-count =
{
    $aliveCount ->
        [one] [color=green]{$aliveCount}[/color] survivor made it out alive.
        *[other] [color=green]{$aliveCount}[/color] survivors made it out alive.
}

## Wizard

objective-issuer-swf = [color=turquoise]Die Space Wizards Federation[/color]

wizard-title = Magier
wizard-description = Ein Magier ist auf der Station! Man weiß nie, was er als Nächstes vorhat.

roles-antag-wizard-name = Magier
roles-antag-wizard-objective = Nutze deine Beherrschung der arkanen Künste, um Chaos und Zwietracht zu säen.

wizard-role-greeting =
    Jetzt wird gezaubert!
    Die Spannungen zwischen der Space Wizards Federation und NanoTrasen nehmen zu. Du wurdest ausgewählt, einer Raumstation von NanoTrasen einen Besuch abzustatten und sie daran zu „erinnern“, warum man sich mit Magiern besser nicht anlegt.
    Stifte Chaos und Verwüstung! Wie du dabei vorgehst, bleibt dir überlassen. Denk aber daran, dass deine Vorgesetzten wollen, dass du lebend zurückkehrst.

wizard-round-end-name = Magier

## TODO: Wizard Apprentice (Coming sometime post-wizard release)
