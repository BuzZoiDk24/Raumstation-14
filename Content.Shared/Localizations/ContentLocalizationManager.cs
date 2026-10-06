using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Robust.Shared.Utility;
using Robust.Shared.Prototypes;
using Content.Shared.Humanoid;
using Content.Shared.IdentityManagement;
using Content.Shared.NameModifier.EntitySystems;
using Robust.Shared.GameObjects.Components.Localization;

namespace Content.Shared.Localizations
{
    public sealed partial class ContentLocalizationManager
    {
        [Dependency] private ILocalizationManager _loc = default!;
        [Dependency] private IEntityManager _germanEntityManager = default!;
        [Dependency] private IPrototypeManager _germanPrototypes = default!;

        // If you want to change your codebase's language, do it here.
        private const string Culture = "de-DE";

        /// <summary>
        /// Custom format strings used for parsing and displaying minutes:seconds timespans.
        /// </summary>
        public static readonly string[] TimeSpanMinutesFormats = new[]
        {
            @"m\:ss",
            @"mm\:ss",
            @"%m",
            @"mm"
        };

        public void Initialize()
        {
            var culture = new CultureInfo(Culture);

            _loc.LoadCulture(culture);
            _loc.AddFunction(culture, "PRESSURE", FormatPressure);
            _loc.AddFunction(culture, "POWERWATTS", FormatPowerWatts);
            _loc.AddFunction(culture, "POWERJOULES", FormatPowerJoules);
            // NOTE: ENERGYWATTHOURS() still takes a value in joules, but formats as watt-hours.
            _loc.AddFunction(culture, "ENERGYWATTHOURS", FormatEnergyWattHours);
            _loc.AddFunction(culture, "UNITS", FormatUnits);
            _loc.AddFunction(culture, "TOSTRING", args => FormatToString(culture, args));
            _loc.AddFunction(culture, "LOC", FormatLoc);
            _loc.AddFunction(culture, "NATURALFIXED", FormatNaturalFixed);
            _loc.AddFunction(culture, "NATURALPERCENT", FormatNaturalPercent);
            _loc.AddFunction(culture, "PLAYTIME", FormatPlaytime);


            /*
             * The following language functions are specific to the english localization. When working on your own
             * localization you should NOT modify these, instead add new functions specific to your language/culture.
             * This ensures the english translations continue to work as expected when fallbacks are needed.
             */
            var cultureEn = new CultureInfo("en-US");
            _loc.LoadCulture(cultureEn);
            _loc.AddFunction(cultureEn, "MAKEPLURAL", FormatMakePlural);
            _loc.AddFunction(cultureEn, "MANY", FormatMany);

			var cultureDe = new CultureInfo("de-DE");
			_loc.AddFunction(cultureDe, "MAKEPLURAL", FormatMakePluralDe);
			_loc.AddFunction(cultureDe, "MANY", FormatManyDe);
            _loc.AddFunction(cultureDe, "DE-ARTICLE", FormatGermanArticle);
            _loc.AddFunction(cultureDe, "DE-GENDER", FormatGermanGender);
            _loc.AddFunction(cultureDe, "DE-POSS-ADJ", FormatGermanPossessiveAdjective);
            _loc.AddFunction(cultureDe, "DE-ADJECTIVE", FormatGermanAdjective);
            _loc.AddFunction(cultureDe, "DE-NAME", FormatGermanName);
        }

        private ILocValue FormatMany(LocArgs args)
        {
            var count = ((LocValueNumber) args.Args[1]).Value;

            if (Math.Abs(count - 1) < 0.0001f)
            {
                return (LocValueString) args.Args[0];
            }
            else
            {
                return (LocValueString) FormatMakePlural(args);
            }
        }

        private ILocValue FormatNaturalPercent(LocArgs args)
        {
            var number = ((LocValueNumber) args.Args[0]).Value * 100;
            var maxDecimals = (int)Math.Floor(((LocValueNumber) args.Args[1]).Value);
            var formatter = (NumberFormatInfo)NumberFormatInfo.GetInstance(CultureInfo.GetCultureInfo(Culture)).Clone();
            formatter.NumberDecimalDigits = maxDecimals;
            return new LocValueString(string.Format(formatter, "{0:N}", number).TrimEnd('0').TrimEnd(char.Parse(formatter.NumberDecimalSeparator)) + "%");
        }

        private ILocValue FormatNaturalFixed(LocArgs args)
        {
            var number = ((LocValueNumber) args.Args[0]).Value;
            var maxDecimals = (int)Math.Floor(((LocValueNumber) args.Args[1]).Value);
            var formatter = (NumberFormatInfo)NumberFormatInfo.GetInstance(CultureInfo.GetCultureInfo(Culture)).Clone();
            formatter.NumberDecimalDigits = maxDecimals;
            return new LocValueString(string.Format(formatter, "{0:N}", number).TrimEnd('0').TrimEnd(char.Parse(formatter.NumberDecimalSeparator)));
        }

        private static readonly Regex PluralEsRule = new("^.*(s|sh|ch|x|z)$");

        private ILocValue FormatMakePlural(LocArgs args)
        {
            var text = ((LocValueString) args.Args[0]).Value;
            var split = text.Split(" ", 1);
            var firstWord = split[0];
            if (PluralEsRule.IsMatch(firstWord))
            {
                if (split.Length == 1)
                    return new LocValueString($"{firstWord}es");
                else
                    return new LocValueString($"{firstWord}es {split[1]}");
            }
            else
            {
                if (split.Length == 1)
                    return new LocValueString($"{firstWord}s");
                else
                    return new LocValueString($"{firstWord}s {split[1]}");
            }
        }

		private ILocValue FormatManyDe(LocArgs args)
		{
			var text = ((LocValueString) args.Args[0]).Value;
			var count = ((LocValueNumber) args.Args[1]).Value;

			if (Math.Abs(count - 1) < 0.0001f)
				return new LocValueString(FormatSingularDe(text));

			return new LocValueString(FormatPluralDe(text));
		}

		private ILocValue FormatMakePluralDe(LocArgs args)
		{
			var text = ((LocValueString) args.Args[0]).Value;
			return new LocValueString(FormatPluralDe(text));
		}

		private static string FormatSingularDe(string text)
		{
			return text switch
			{
				"second" => "Sekunde",
				"minute" => "Minute",
				"hour" => "Stunde",
				"day" => "Tag",
				"mole" => "Mol",
				"unit" => "Einheit",
				_ => text
			};
		}

		private static string FormatPluralDe(string text)
		{
			return text switch
			{
				"second" => "Sekunden",
				"minute" => "Minuten",
				"hour" => "Stunden",
				"day" => "Tage",
				"mole" => "Mol",
				"unit" => "Einheiten",
				"Platte" => "Platten",
				"Barren" => "Barren",
				"Brett" => "Bretter",
				"Rolle" => "Rollen",
				"Stück" => "Stück",
				"Bündel" => "Bündel",
				"Scheibe" => "Scheiben",
				"Netz" => "Netze",
				"Brocken" => "Brocken",
				"Kapsel" => "Kapseln",
				"Geldschein" => "Geldscheine",
				_ => text
			};
		}

        // TODO: allow fluent to take in lists of strings so this can be a format function like it should be.
        /// <summary>
        /// Formats a list using the current localization's conjunction and punctuation.
        /// </summary>
        public static string FormatList(List<string> list)
        {
            return list.Count switch
            {
                <= 0 => string.Empty,
                1 => list[0],
                2 => Loc.GetString("zzzz-fmt-list-and-pair", ("first", list[0]), ("last", list[1])),
                _ => Loc.GetString("zzzz-fmt-list-and-many",
                    ("items", string.Join(", ", list.GetRange(0, list.Count - 1))), ("last", list[^1]))
            };
        }

        /// <summary>
        /// Formats alternatives using the current localization's conjunction and punctuation.
        /// </summary>
        public static string FormatListToOr(List<string> list)
        {
            return list.Count switch
            {
                <= 0 => string.Empty,
                1 => list[0],
                2 => Loc.GetString("zzzz-fmt-list-or-pair", ("first", list[0]), ("last", list[1])),
                _ => Loc.GetString("zzzz-fmt-list-or-many",
                    ("items", string.Join(", ", list.GetRange(0, list.Count - 1))), ("last", list[^1]))
            };
        }

        /// <summary>
        /// Formats a direction struct as a human-readable string.
        /// </summary>
        public static string FormatDirection(Direction dir)
        {
            return Loc.GetString($"zzzz-fmt-direction-{dir.ToString()}");
        }

        /// <summary>
        /// Formats playtime as hours and minutes.
        /// </summary>
        public static string FormatPlaytime(TimeSpan time)
        {
            time = TimeSpan.FromMinutes(Math.Ceiling(time.TotalMinutes));
            var hours = (int)time.TotalHours;
            var minutes = time.Minutes;
            return Loc.GetString($"zzzz-fmt-playtime", ("hours", hours), ("minutes", minutes));
        }

        private static ILocValue FormatLoc(LocArgs args)
        {
            var id = ((LocValueString) args.Args[0]).Value;

            return new LocValueString(Loc.GetString(id, args.Options.Select(x => (x.Key, x.Value.Value!)).ToArray()));
        }

        private static ILocValue FormatToString(CultureInfo culture, LocArgs args)
        {
            var arg = args.Args[0];
            var fmt = ((LocValueString) args.Args[1]).Value;

            var obj = arg.Value;
            if (obj is IFormattable formattable)
                return new LocValueString(formattable.ToString(fmt, culture));

            return new LocValueString(obj?.ToString() ?? "");
        }

        private static ILocValue FormatUnitsGeneric(
            LocArgs args,
            string mode,
            Func<double, double>? transformValue = null)
        {
            const int maxPlaces = 5; // Matches amount in _lib.ftl
            var pressure = ((LocValueNumber) args.Args[0]).Value;

            if (transformValue != null)
                pressure = transformValue(pressure);

            var places = 0;
            while (pressure > 1000 && places < maxPlaces)
            {
                pressure /= 1000;
                places += 1;
            }

            return new LocValueString(Loc.GetString(mode, ("divided", pressure), ("places", places)));
        }

        private static ILocValue FormatPressure(LocArgs args)
        {
            return FormatUnitsGeneric(args, "zzzz-fmt-pressure");
        }

        private static ILocValue FormatPowerWatts(LocArgs args)
        {
            return FormatUnitsGeneric(args, "zzzz-fmt-power-watts");
        }

        private static ILocValue FormatPowerJoules(LocArgs args)
        {
            return FormatUnitsGeneric(args, "zzzz-fmt-power-joules");
        }

        private static ILocValue FormatEnergyWattHours(LocArgs args)
        {
            const double joulesToWattHours = 1.0 / 3600;

            return FormatUnitsGeneric(args, "zzzz-fmt-energy-watt-hours", joules => joules * joulesToWattHours);
        }

        private static ILocValue FormatUnits(LocArgs args)
        {
            if (!Units.Types.TryGetValue(((LocValueString) args.Args[0]).Value, out var ut))
                throw new ArgumentException($"Unknown unit type {((LocValueString) args.Args[0]).Value}");

            var fmtstr = ((LocValueString) args.Args[1]).Value;

            double max = Double.NegativeInfinity;
            var iargs = new double[args.Args.Count - 1];
            for (var i = 2; i < args.Args.Count; i++)
            {
                var n = ((LocValueNumber) args.Args[i]).Value;
                if (n > max)
                    max = n;

                iargs[i - 2] = n;
            }

            if (!ut.TryGetUnit(max, out var mu))
                throw new ArgumentException("Unit out of range for type");

            var fargs = new object[iargs.Length];

            for (var i = 0; i < iargs.Length; i++)
                fargs[i] = iargs[i] * mu.Factor;

            fargs[^1] = Loc.GetString($"units-{mu.Unit.ToLower()}");

            // Before anyone complains about "{"+"${...}", at least it's better than MS's approach...
            // https://docs.microsoft.com/en-us/dotnet/standard/base-types/composite-formatting#escaping-braces
            //
            // Note that the closing brace isn't replaced so that format specifiers can be applied.
            var res = String.Format(
                fmtstr.Replace("{UNIT", "{" + $"{fargs.Length - 1}"),
                fargs
            );

            return new LocValueString(res);
        }

        private static ILocValue FormatPlaytime(LocArgs args)
        {
            var time = TimeSpan.Zero;
            if (args.Args is { Count: > 0 } && args.Args[0].Value is TimeSpan timeArg)
            {
                time = timeArg;
            }
            return new LocValueString(FormatPlaytime(time));
        }

        /// <summary>
        /// Uses the localized noun gender for objects and NPCs. Humanoids keep
        /// their chosen pronouns, while named pets can retain their own gender.
        /// </summary>
        private ILocValue FormatGermanGender(LocArgs args)
        {
            if (args.Args.Count < 1 || args.Args[0].Value is not EntityUid entity)
                return new LocValueString("neuter");

            var isHumanoid = _germanEntityManager.HasComponent<HumanoidProfileComponent>(entity);
            if (!isHumanoid &&
                _germanEntityManager.TryGetComponent<MetaDataComponent>(entity, out var metadata) &&
                metadata.EntityPrototype is { } prototype &&
                _loc.GetEntityData(prototype.ID).Attributes.TryGetValue("gender", out var nounGender))
            {
                var localizedGender = nounGender.ToLowerInvariant();
                // "proper" identifies a name, not a grammatical gender. Named
                // pets keep the gender explicitly set by their Grammar component.
                if (localizedGender is "male" or "female" or "masculine" or "feminine" or "neuter" or "epicene")
                    return new LocValueString(localizedGender);
            }

            if (_germanEntityManager.TryGetComponent<GrammarComponent>(entity, out var grammar) &&
                grammar.Gender is { } gender)
                return new LocValueString(gender.ToString().ToLowerInvariant());

            return new LocValueString("neuter");
        }

        /// <summary>
        /// Formats a definite article by default. Use article: "indefinite"
        /// for ein/eine; grammatical case and noun metadata are shared.
        /// Indefinite plurals have no article.
        /// </summary>
        private ILocValue FormatGermanArticle(LocArgs args)
        {
            if (args.Args.Count < 2)
                return new LocValueString("");

            // Entity arguments use localized noun metadata. String arguments
            // remain supported for objective groups and other explicit grammar.
            var genus = args.Args[0].Value is EntityUid
                ? ((LocValueString) FormatGermanGender(args)).Value.ToLowerInvariant()
                : ((LocValueString) args.Args[0]).Value.ToLowerInvariant();
            // Gender values use male/female or masculine/feminine.
            genus = genus switch
            {
                "male" => "masculine",
                "female" => "feminine",
                _ => genus
            };
            var grammaticalCase = ((LocValueString) args.Args[1]).Value.ToLowerInvariant();

            var number = args.Args.Count >= 3
                ? ((LocValueString) args.Args[2]).Value.ToLowerInvariant()
                : "singular";

            if (args.Args.Count < 3 && args.Args[0].Value is EntityUid entity &&
                _germanEntityManager.TryGetComponent<MetaDataComponent>(entity, out var metadata) &&
                metadata.EntityPrototype is { } prototype &&
                _loc.GetEntityData(prototype.ID).Attributes.TryGetValue("number", out var nounNumber))
                number = nounNumber.ToLowerInvariant();

            var indefinite = args.Options.TryGetValue("article", out var article) &&
                article.Value is string articleType &&
                articleType.Equals("indefinite", StringComparison.OrdinalIgnoreCase);

            if (indefinite)
            {
                if (number == "plural")
                    return new LocValueString("");

                return (genus, grammaticalCase) switch
                {
                    ("masculine", "nominative") => new LocValueString("ein"),
                    ("masculine", "accusative") => new LocValueString("einen"),
                    ("masculine", "dative") => new LocValueString("einem"),
                    ("masculine", "genitive") => new LocValueString("eines"),

                    ("feminine", "nominative" or "accusative") => new LocValueString("eine"),
                    ("feminine", "dative" or "genitive") => new LocValueString("einer"),

                    ("neuter", "nominative" or "accusative") => new LocValueString("ein"),
                    ("neuter", "dative") => new LocValueString("einem"),
                    ("neuter", "genitive") => new LocValueString("eines"),

                    _ => new LocValueString("")
                };
            }

            if (number == "plural")
            {
                return grammaticalCase switch
                {
                    "nominative" => new LocValueString("die"),
                    "accusative" => new LocValueString("die"),
                    "dative" => new LocValueString("den"),
                    "genitive" => new LocValueString("der"),
                    _ => new LocValueString("")
                };
            }

            return (genus, grammaticalCase) switch
            {
                ("masculine", "nominative") => new LocValueString("der"),
                ("masculine", "accusative") => new LocValueString("den"),
                ("masculine", "dative") => new LocValueString("dem"),
                ("masculine", "genitive") => new LocValueString("des"),

                ("feminine", "nominative") => new LocValueString("die"),
                ("feminine", "accusative") => new LocValueString("die"),
                ("feminine", "dative") => new LocValueString("der"),
                ("feminine", "genitive") => new LocValueString("der"),

                ("neuter", "nominative") => new LocValueString("das"),
                ("neuter", "accusative") => new LocValueString("das"),
                ("neuter", "dative") => new LocValueString("dem"),
                ("neuter", "genitive") => new LocValueString("des"),

                _ => new LocValueString("")
            };
        }

        /// <summary>
        /// Inflects sein/ihr for the noun. With es (or a legacy neutral profile),
        /// the selected body determines the possessive stem.
        /// </summary>
        private static ILocValue FormatGermanPossessiveAdjective(LocArgs args)
        {
            if (args.Args.Count < 4)
                return new LocValueString("");

            var pronouns = ((LocValueString) args.Args[0]).Value.ToLowerInvariant();
            var bodySex = ((LocValueString) args.Args[1]).Value.ToLowerInvariant();
            var grammaticalCase = ((LocValueString) args.Args[2]).Value.ToLowerInvariant();
            var nounGender = ((LocValueString) args.Args[3]).Value.ToLowerInvariant();

            var stem = (pronouns is "female" or "feminine") ||
                (pronouns is not ("male" or "masculine") && bodySex == "female")
                ? "ihr"
                : "sein";

            var ending = (grammaticalCase, nounGender) switch
            {
                ("nominative", "feminine" or "plural") => "e",
                ("accusative", "masculine") => "en",
                ("accusative", "feminine" or "plural") => "e",
                ("dative", "masculine" or "neuter") => "em",
                ("dative", "feminine") => "er",
                ("dative", "plural") => "en",
                ("genitive", "masculine" or "neuter") => "es",
                ("genitive", "feminine" or "plural") => "er",
                _ => ""
            };

            return new LocValueString(stem + ending);
        }

        /// <summary>
        /// Inflects an adjective stem. Existing three-argument calls default to
        /// singular nominative; optional arguments select case and number.
        /// </summary>
        private static ILocValue FormatGermanAdjective(LocArgs args)
        {
            if (args.Args.Count < 3)
                return new LocValueString("");

            var stem = ((LocValueString) args.Args[0]).Value;
            var gender = ((LocValueString) args.Args[1]).Value.ToLowerInvariant();
            var form = ((LocValueString) args.Args[2]).Value.ToLowerInvariant();
            var grammaticalCase = args.Args.Count > 3
                ? ((LocValueString) args.Args[3]).Value.ToLowerInvariant()
                : "nominative";
            var number = args.Args.Count > 4
                ? ((LocValueString) args.Args[4]).Value.ToLowerInvariant()
                : "singular";
            return new LocValueString(InflectGermanAdjective(stem, gender, form, grammaticalCase, number));
        }

        private static string InflectGermanAdjective(string stem, string gender, string form,
            string grammaticalCase, string number)
        {
            gender = gender switch
            {
                "male" => "masculine",
                "female" => "feminine",
                "masculine" or "feminine" or "neuter" or "plural" => gender,
                _ => "neuter"
            };
            if (number == "plural")
                gender = "plural";

            if (grammaticalCase is not ("nominative" or "accusative" or "dative" or "genitive") ||
                form is not ("strong" or "weak" or "mixed"))
                return stem;

            var ending = form switch
            {
                "weak" => (grammaticalCase, gender) switch
                {
                    ("nominative", "masculine" or "feminine" or "neuter") => "e",
                    ("accusative", "feminine" or "neuter") => "e",
                    _ => "en"
                },
                "mixed" => (grammaticalCase, gender) switch
                {
                    ("nominative", "masculine") => "er",
                    ("nominative" or "accusative", "neuter") => "es",
                    ("nominative" or "accusative", "feminine") => "e",
                    _ => "en"
                },
                _ => (grammaticalCase, gender) switch
                {
                    ("nominative", "masculine") => "er",
                    ("nominative" or "accusative", "feminine" or "plural") => "e",
                    ("nominative" or "accusative", "neuter") => "es",
                    ("accusative", "masculine") => "en",
                    ("dative", "masculine" or "neuter") => "em",
                    ("dative" or "genitive", "feminine") => "er",
                    ("dative", "plural") => "en",
                    ("genitive", "plural") => "er",
                    ("genitive", "masculine" or "neuter") => "en",
                    _ => ""
                }
            };
            // Comma-separated stems let every adjective in a name share the ending.
            // A stem such as "sehr alt" remains a single phrase.
            return string.Join(" ", stem.Split(',').Select(part => part.Trim() + ending));
        }

        /// <summary>
        /// Formats an entity or entity-prototype name for a sentence without
        /// changing its display name. A string first argument is a prototype ID,
        /// not an arbitrary name. The optional fourth argument for entities is
        /// an already escaped display name, as used by hands and chat.
        /// </summary>
        private ILocValue FormatGermanName(LocArgs args)
        {
            if (args.Args.Count < 3)
                return new LocValueString("");

            var grammaticalCase = ((LocValueString) args.Args[1]).Value.ToLowerInvariant();
            var declension = ((LocValueString) args.Args[2]).Value.ToLowerInvariant();
            if (args.Args[0].Value is string prototypeId)
            {
                if (!_germanPrototypes.TryIndex<EntityPrototype>(prototypeId, out var prototype))
                    return new LocValueString(prototypeId);
                var data = _loc.GetEntityData(prototype.ID);
                var gender = data.Attributes.GetValueOrDefault("gender", "neuter").ToLowerInvariant();
                var number = data.Attributes.GetValueOrDefault("number", "singular").ToLowerInvariant();
                declension = NormalizeGermanDeclension(declension, number);
                return new LocValueString(FormatGermanBaseName(prototype.ID, data.Name, data,
                    gender, grammaticalCase, declension, number));
            }

            if (args.Args[0].Value is not EntityUid entity ||
                !_germanEntityManager.TryGetComponent<MetaDataComponent>(entity, out var metadata))
                return new LocValueString("");

            var identityName = Identity.Name(entity, _germanEntityManager);
            var escaped = args.Args.Count > 3;
            var displayName = escaped ? ((LocValueString) args.Args[3]).Value : identityName;
            var expectedName = escaped ? FormattedMessage.EscapeText(metadata.EntityName) : metadata.EntityName;
            if (displayName != expectedName ||
                _germanEntityManager.HasComponent<HumanoidProfileComponent>(entity))
                return new LocValueString(displayName);

            var names = _germanEntityManager.System<NameModifierSystem>();
            var baseName = names.GetBaseName(entity);
            var nounNumber = "singular";
            if (metadata.EntityPrototype is { } entityPrototype)
            {
                var data = _loc.GetEntityData(entityPrototype.ID);
                if (IsGermanProperName(data))
                    return new LocValueString(displayName);
                nounNumber = data.Attributes.GetValueOrDefault("number", "singular").ToLowerInvariant();
                declension = NormalizeGermanDeclension(declension, nounNumber);
                var gender = ((LocValueString) FormatGermanGender(args)).Value.ToLowerInvariant();
                baseName = FormatGermanBaseName(entityPrototype.ID, baseName, data,
                    gender, grammaticalCase, declension, nounNumber);
            }
            else
                declension = NormalizeGermanDeclension(declension, nounNumber);

            var result = names.GetContextualName(entity, baseName, grammaticalCase, declension, nounNumber);
            return new LocValueString(escaped ? FormattedMessage.EscapeText(result) : result);
        }

        private static bool IsGermanProperName(EntityLocData data)
        {
            return data.Attributes.GetValueOrDefault("gender") == "proper" ||
                   data.Attributes.GetValueOrDefault("proper") == "true";
        }

        private static string NormalizeGermanDeclension(string declension, string number)
        {
            // There is no indefinite article in the plural.
            return declension == "indefinite"
                ? number == "plural" ? "strong" : "mixed"
                : declension;
        }

        private string FormatGermanBaseName(string prototypeId, string baseName, EntityLocData data,
            string gender, string grammaticalCase, string declension, string number)
        {
            if (baseName != data.Name || IsGermanProperName(data))
                return baseName;

            if (TryGetGermanNameAttribute(prototypeId, data.Name, "name-adjective", out var stem) &&
                TryGetGermanNameAttribute(prototypeId, data.Name, "name-noun", out var noun) &&
                string.Equals(baseName,
                    InflectGermanAdjective(stem, gender, "strong", "nominative", number) + " " + noun,
                    StringComparison.OrdinalIgnoreCase))
            {
                if (TryGetGermanNameAttribute(prototypeId, data.Name, $"name-noun-{grammaticalCase}", out var nounForm))
                    noun = nounForm;
                return InflectGermanAdjective(stem, gender, declension, grammaticalCase, number) + " " + noun;
            }

            return TryGetGermanNameAttribute(prototypeId, data.Name, $"name-{grammaticalCase}", out var fullForm)
                ? fullForm
                : baseName;
        }

        /// <summary>
        /// Case forms belong to the localized base name that defines them.
        /// Desc-only variants may inherit them; differently named variants must
        /// provide their own forms instead of inheriting a parent's noun ending.
        /// </summary>
        private bool TryGetGermanNameAttribute(string prototypeId, string baseName, string attribute,
            out string value)
        {
            foreach (var parent in _germanPrototypes.EnumerateParents<EntityPrototype>(prototypeId, true))
            {
                if (parent == null)
                    continue;
                // TryGetString logs an error when a message exists but its
                // requested optional attribute does not. Read the cached
                // attribute dictionary instead: missing forms are normal.
                var data = _loc.GetEntityData(parent.ID);
                if (data.Name != baseName)
                    break;
                if (data.Attributes.TryGetValue(attribute, out var form))
                {
                    value = form;
                    return true;
                }
            }
            value = "";
            return false;
        }
    }
}
