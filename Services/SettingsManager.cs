using MathTrainer.Models.Settings;

namespace MathTrainer.Services
{
    class SettingsManager : JsonManager<Setting>
    {
        private Setting _settings;
        public SettingsManager()
            : base(Path.Combine(DataFolder, "settings.json"))
        {
            _settings = Load();

            AppDomain.CurrentDomain.ProcessExit += (_, _) => Save(_settings);
            Console.CancelKeyPress += (_, _) => Save(_settings);
        }

        public override Setting CreateDefault() => new();
        

        public void SetMaxDigits(int value)
        {
            _settings.MaxDigits = EnsurePositive(value);
            Save(_settings);
        }


        public void SetMaxOperations(int value)
        {
            _settings.MaxOperations = EnsurePositive(value);
            Save(_settings);
        }

        public void ToggleFixedDigits()
        {
            _settings.FixedDigits = !_settings.FixedDigits;
            Save(_settings);
        }

        public void ToggleFixedOperations()
        {
            _settings.FixedOperations = !_settings.FixedOperations;
            Save(_settings);
        }
        public Setting Current => new()
        {
            MaxDigits = _settings.MaxDigits,
            MaxOperations = _settings.MaxOperations,
            FixedDigits = _settings.FixedDigits,
            FixedOperations = _settings.FixedOperations
        };

        public void ResetSettings()
        {
            _settings = CreateDefault();
            Save(_settings);
        }

        private static int EnsurePositive(int value)
        {
            return value <= 0 ? 1 : value;
        }
    }
}
