## UI

cargo-console-menu-title = Versorgungskonsole
cargo-console-menu-flavor-left = Bestelle noch mehr Pizzakartons als sonst!
cargo-console-menu-flavor-right = v2.1
cargo-console-menu-account-name-label = Konto:{" "}
cargo-console-menu-account-name-none-text = Keines
cargo-console-menu-account-name-format = [bold][color={$color}]{$name}[/color][/bold] [font="Monospace"]\[{$code}\][/font]
cargo-console-menu-shuttle-name-label = Shuttlename:{" "}
cargo-console-menu-shuttle-name-none-text = Keines
cargo-console-menu-points-label = Guthaben:{" "}
cargo-console-menu-points-amount = ${$amount}
cargo-console-menu-shuttle-status-label = Shuttlestatus:{" "}
cargo-console-menu-shuttle-status-away-text = Unterwegs
cargo-console-menu-order-capacity-label = Bestellkapazität:{" "}
cargo-console-menu-order-capacity-number = {$count}/{$capacity}
cargo-console-menu-call-shuttle-button = Teleportplattform aktivieren
cargo-console-menu-permissions-button = Berechtigungen
cargo-console-menu-categories-label = Kategorien:{" "}
cargo-console-menu-search-bar-placeholder = Suchen
cargo-console-menu-requests-label = Anfragen
cargo-console-menu-orders-label = Bestellungen
cargo-console-menu-populate-categories-all-text = Alle
cargo-console-menu-order-row-title = {$productName} (x{$orderAmount} für {$orderPrice}$)
cargo-console-menu-populate-orders-cargo-order-row-product-name-text = Angefragt von: {$orderRequester} für [color={$accountColor}]{$account}[/color]
cargo-console-menu-order-row-product-description = Begründung: {$orderReason}
cargo-console-menu-order-row-button-approve = Genehmigen
cargo-console-menu-order-row-button-cancel = Stornieren
cargo-console-menu-order-row-alerts-reason-absent = Keine Begründung angegeben
cargo-console-menu-order-row-alerts-requester-unknown = Unbekannt
cargo-console-menu-tab-title-orders = Bestellungen
cargo-console-menu-tab-title-funds = Überweisungen
cargo-console-menu-account-action-transfer-limit = [bold]Überweisungslimit:[/bold] ${$limit}
cargo-console-menu-account-action-transfer-limit-unlimited-notifier = [color=gold](Unbegrenzt)[/color]
cargo-console-menu-account-action-select = [bold]Kontoaktion:[/bold]
cargo-console-menu-account-action-amount = [bold]Betrag:[/bold] $
cargo-console-menu-account-action-button = Überweisen
cargo-console-menu-toggle-account-lock-button = Überweisungslimit umschalten
cargo-console-menu-account-action-option-withdraw = Bargeld abheben
cargo-console-menu-account-action-option-transfer = Geld auf {$code} überweisen

# Orders
cargo-console-order-not-allowed = Keine Zugriffsberechtigung
cargo-console-station-not-found = Keine Station verfügbar
cargo-console-invalid-product = Ungültige Produkt-ID
cargo-console-too-many = Zu viele genehmigte Bestellungen
cargo-console-snip-snip = Bestellmenge an die Kapazität angepasst
cargo-console-insufficient-funds = Nicht genügend Guthaben (benötigt werden {$cost})
cargo-console-unfulfilled = Kein Platz für die Lieferung
cargo-console-trade-station = An {$destination} gesendet
cargo-console-unlock-approved-order-broadcast = [bold]{$approver}[/bold] hat [bold]{$productName} x{$orderAmount}[/bold] für [bold]{$cost}[/bold] genehmigt
cargo-console-fund-withdraw-broadcast = [bold]{$name} hat {$amount} Spesos vom Konto {$name1} \[{$code1}\] abgehoben[/bold]
cargo-console-fund-transfer-broadcast = [bold]{$name} hat {$amount} Spesos vom Konto {$name1} \[{$code1}\] auf das Konto {$name2} \[{$code2}\] überwiesen[/bold]
cargo-console-fund-transfer-user-unknown = Unbekannt

cargo-console-paper-reason-default = Keine
cargo-console-paper-approver-default = Unbekannt
cargo-console-paper-print-name = Bestellung #{$orderNumber}
cargo-console-paper-print-text = [head=2]Bestellung #{$orderNumber}[/head]
    {"[bold]Gegenstand:[/bold]"} {$itemName} (x{$orderQuantity})
    {"[bold]Angefragt von:[/bold]"} {$requester}

    {"[head=3]Bestellinformationen[/head]"}
    {"[bold]Zahlendes Konto[/bold]:"} {$account} [font="Monospace"]\[{$accountcode}\][/font]
    {"[bold]Genehmigt von:[/bold]"} {$approver}
    {"[bold]Begründung:[/bold]"} {$reason}

# Cargo shuttle console
cargo-shuttle-console-menu-title = Frachtshuttle-Konsole
cargo-shuttle-console-station-unknown = Unbekannt
cargo-shuttle-console-shuttle-not-found = Nicht gefunden
cargo-shuttle-console-organics = Organische Lebensformen an Bord des Shuttles erkannt
cargo-no-shuttle = Kein Frachtshuttle gefunden!

# Funding allocation console
cargo-funding-alloc-console-menu-title = Budgetverteilungskonsole
cargo-funding-alloc-console-label-account = [bold]Konto[/bold]
cargo-funding-alloc-console-label-code = [bold] Kennung [/bold]
cargo-funding-alloc-console-label-balance = [bold] Guthaben [/bold]
cargo-funding-alloc-console-label-cut = [bold] Einnahmenanteil (%) [/bold]

cargo-funding-alloc-console-label-primary-cut = Anteil der Versorgung an Einnahmen außerhalb von Tresorkistenverkäufen (%):
cargo-funding-alloc-console-label-lockbox-cut = Anteil der Versorgung an Tresorkistenverkäufen (%):

cargo-funding-alloc-console-label-help-non-adjustible = Die Versorgung erhält {$percent}% der Einnahmen außerhalb von Tresorkistenverkäufen. Der Rest wird wie folgt aufgeteilt:
cargo-funding-alloc-console-label-help-adjustible = Die verbleibenden Einnahmen außerhalb von Tresorkistenverkäufen werden wie folgt aufgeteilt:
cargo-funding-alloc-console-button-save = Änderungen speichern
cargo-funding-alloc-console-label-save-fail = [bold]Ungültige Einnahmenverteilung![/bold] [color=red]({$pos ->
    [1] +
    *[-1] -
}{$val}%)[/color]

# Slip template
cargo-acquisition-slip-body = [head=3]Produktinformationen[/head]
    {"[bold]Produkt:[/bold]"} {$product}
    {"[bold]Beschreibung:[/bold]"} {$description}
    {"[bold]Stückpreis:[/bold]"} ${$unit}
    {"[bold]Menge:[/bold]"} {$amount}
    {"[bold]Kosten:[/bold]"} ${$cost}

    {"[head=3]Kaufinformationen[/head]"}
    {"[bold]Bestellt von:[/bold]"} {$orderer}
    {"[bold]Begründung:[/bold]"} {$reason}
