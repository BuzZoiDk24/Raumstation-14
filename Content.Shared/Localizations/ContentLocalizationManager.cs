using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Robust.Shared.Utility;
using Content.Shared.Humanoid;
using Robust.Shared.GameObjects.Components.Localization;

namespace Content.Shared.Localizations
{
    public sealed partial class ContentLocalizationManager
    {
        [Dependency] private ILocalizationManager _loc = default!;
        [Dependency] private IEntityManager _germanEntityManager = default!;

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
        /// Inflects a German adjective stem for a singular nominative noun.
        /// Strong forms stand alone in a name; weak forms follow a definite article.
        /// </summary>
        private static ILocValue FormatGermanAdjective(LocArgs args)
        {
            if (args.Args.Count < 3)
                return new LocValueString("");

            var stem = ((LocValueString) args.Args[0]).Value;
            var gender = ((LocValueString) args.Args[1]).Value.ToLowerInvariant();
            var form = ((LocValueString) args.Args[2]).Value.ToLowerInvariant();

            var ending = form == "weak"
                ? "e"
                : gender switch
                {
                    "male" or "masculine" => "er",
                    "female" or "feminine" => "e",
                    _ => "es"
                };

            return new LocValueString(stem + ending);
        }
    }
}
